#pragma once

#include "SlaveConfigSystem.h"

#define DEFINE_SLEEP_TIME 600		//Define sleep time(us).
//变量声明
char IOmap[MAP_SIZE];

struct SLAVET_ARR slave_arr[MAX_SLAVE];
//these are lists heads of 4 types of slave struct.
slave_di dis = (slave_di)new SLAVE_DI();	
slave_do dos = (slave_do)new SLAVE_DO();
slave_ai ais = (slave_ai)new SLAVE_AI();
slave_ao aos = (slave_ao)new SLAVE_AO();

//Define some global variables.
const int SLAVE_TYPE_ID = 2;		//Type bit
const int SLAVE_CHANNEL_ID = 5;		//Channel bit
bool runningState = true;
HANDLE wthread;
HANDLE g_hMutex = CreateMutex(NULL, FALSE, L"WRITE_LOCK");

//slave write thread.
DWORD WINAPI writeSlaveThread(LPVOID lpParameter) {
	int wkc;
	//如果没有设定值，就用默认值DEFINE_SLEEP_TIME
	int sleepTime = lpParameter == NULL ? DEFINE_SLEEP_TIME : *(int*)lpParameter;
	while (runningState) {
		WaitForSingleObject(g_hMutex, INFINITE);//lock
		ec_send_processdata();
		wkc = ec_receive_processdata(DEFINE_SLEEP_TIME/2);
		ReleaseMutex(g_hMutex);		//unlock

		osal_usleep(sleepTime);
	}
	return 0;
}
//common slave config process.
bool needlf;
bool inOP;
int expectedWKC;
int initSlaveConfigInfo() {
	int i, j, oloop, iloop, chk;
	needlf = FALSE;
	inOP = FALSE;

	/* initialise SOEM, bind socket to ifname */
	if (ec_init(ifbuf))
	{
		/* find and auto-config slaves */
		if (ec_config_init(FALSE) > 0)
		{
			ec_config_map(&IOmap);
			ec_configdc();

			while (EcatError) printf("%s", ec_elist2string());
			
			ec_statecheck(0, EC_STATE_SAFE_OP, EC_TIMEOUTSTATE * 4);

			//================OP STATE===================
			expectedWKC = (ec_group[0].outputsWKC * 2) + ec_group[0].inputsWKC;
			printf("Calculated workcounter %d\n", expectedWKC);
			ec_slave[0].state = EC_STATE_OPERATIONAL;
			/* send one valid process data to make outputs in slaves happy*/
			ec_send_processdata();
			ec_receive_processdata(EC_TIMEOUTRET);
			/* request OP state for all slaves */
			ec_writestate(0);
			int chk = 40;
			/* wait for all slaves to reach OP state */
			do
			{
				ec_send_processdata();
				ec_receive_processdata(EC_TIMEOUTRET);
				ec_statecheck(0, EC_STATE_OPERATIONAL, 50000);
			} while (chk-- && (ec_slave[0].state != EC_STATE_OPERATIONAL));

			if (ec_slave[0].state != EC_STATE_OPERATIONAL) {	//最终状态检查
				cout << "未能成功进入OP状态" << endl;
			}

			initLocalSlaveInfo();

			/*启动写循环，如果不写在此处，部分从站将无法正常启动*/
			if (wthread == NULL)
				wthread = CreateThread(NULL, 0, writeSlaveThread, NULL, 0, NULL);

			return ec_slavecount - 1;
		}
		else
		{
			printf("No slaves found!\n");
			return 0;
		}
		
	}
	else
	{
		printf("No socket connection on %s\nExcecute as root\n", ifbuf);
		return -1;
	}
}
//interface: stop slave before program will be closed.
void stopSlaveRunning() {
	runningState = false;
	ec_slave[0].state = EC_STATE_INIT;
	ec_writestate(0);
	ec_close();
}

//slave local config process, after slave start up.
void initLocalSlaveInfo() {
	if (ec_slavecount == 0) {
		return;
	}

	for (int i = 0; i < ec_slavecount + 1; i++) {
		if (i == 0) {									//不处理第一个
			continue;
		}
		else if (ec_slave[i].outputs == 0 && ec_slave[i].inputs == 0)	//耦合器
		{
			slave_arr[i].name = ec_slave[i].name;
			slave_arr[i].type = TYPE_COUPLER;
			slave_arr[i].id = ec_slave[i].eep_id;
			slave_arr[i].channelNum = -1;
			slave_arr[i].ptrToSlave1 = NULL;
			slave_arr[i].ptrToSlave2 = NULL;

			continue;
		}
		//TODO: 伺服驱动器使用前需要进行配置，参见"松下伺服EtherCAT使用说明.pdf"的5-4-4节.
		else if (ec_slave[i].inputs != 0 && ec_slave[i].outputs != 0)	//伺服驱动器或位移传感器
		{
			if (strstr(ec_slave[i].name, "EL5") == NULL) {	//伺服驱动器
				slave_arr[i].name = ec_slave[i].name;
				slave_arr[i].type = TYPE_SERVO;
				slave_arr[i].id = ec_slave[i].eep_id;
				slave_arr[i].channelNum = 16;		//TODO: 根据伺服驱动器的不同而不同

													//建立伺服驱动器结构体映射
				pservo_input tmp_in = (pservo_input)malloc(sizeof(SLAVE_SERVO_IN));
				pservo_output tmp_out = (pservo_output)malloc(sizeof(SLAVE_SERVO_OUT));

				tmp_in = (pservo_input)ec_slave[i].inputs;	//和IOmap映射
				tmp_out = (pservo_output)ec_slave[i].outputs;

				slave_arr[i].ptrToSlave1 = tmp_in;	//slave array -> servo struct
				slave_arr[i].ptrToSlave2 = tmp_out;
				tmp_in->slaveInfo = &slave_arr[i];	//servo struct -> slave arr
				tmp_out->slaveInfo = &slave_arr[i];

				//TODO: 是否需要建立伺服驱动器的链表
				continue;
			}
			else if (strstr(ec_slave[i].name, "EL5") != NULL) {	//位移传感器
				slave_arr[i].name = ec_slave[i].name;
				slave_arr[i].type = TYPE_DSENSOR;
				slave_arr[i].id = ec_slave[i].eep_id;
				slave_arr[i].channelNum = ec_slave[i].name[SLAVE_CHANNEL_ID] - '0';

				dservor_input tmp_in = (dservor_input)malloc(sizeof(SLAVE_DSERVOR_IN));
				dservor_output tmp_out = (dservor_output)malloc(sizeof(SLAVE_DSERVOR_OUT));

				tmp_in = (dservor_input)ec_slave[i].inputs;
				tmp_out = (dservor_output)ec_slave[i].outputs;

				slave_arr[i].ptrToSlave1 = tmp_in;
				slave_arr[i].ptrToSlave2 = tmp_out;

				tmp_in->slaveInfo = &slave_arr[i];
				tmp_out->slaveInfo = &slave_arr[i];
				continue;
			}
		}
		else {												//普通IO板
			char* name = ec_slave[i].name;	//从站可读名称

											//处理大部分基础信息
			slave_arr[i].id = ec_slave[i].eep_id;
			slave_arr[i].name = name;
			slave_arr[i].type = name[SLAVE_TYPE_ID] - '0';			//获取类型

																	//单独处理channelNum
			int tmpChannelNum = name[SLAVE_CHANNEL_ID] - '0';
			if (tmpChannelNum == 9) tmpChannelNum = 16;		//末位为9则为16通道，针对EL1809和EL2809
			slave_arr[i].channelNum = tmpChannelNum;

			switch (slave_arr[i].type)
			{
				case 1:				//DI
				{
					slave_di tmpslave = (slave_di)malloc(sizeof(SLAVE_DI));
					tmpslave = (slave_di)ec_slave[i].inputs;				//将当前读取值写入di结构体

					slave_arr[i].ptrToSlave1 = tmpslave;					//slave_arr[i]指向当前结构体
					slave_arr[i].ptrToSlave2 = NULL;
					tmpslave->slaveinfo = &slave_arr[i];					//指回去，双向链表

					slave_di ptr = dis->next;								//插入头结点
					dis->next = tmpslave;
					tmpslave->next = ptr;
					break;
				}
				case 2:				//DO
				{
					slave_do tmpslave = (slave_do)malloc(sizeof(SLAVE_DO));
					tmpslave = (slave_do)ec_slave[i].outputs;

					slave_arr[i].ptrToSlave1 = tmpslave;						//slave_arr[i]指向当前结构体
					slave_arr[i].ptrToSlave2 = NULL;
					tmpslave->slaveinfo = &slave_arr[i];

					slave_do ptr = dos->next;
					dos->next = tmpslave;
					tmpslave->next = ptr;
					break;
				}
				case 3:				//AI
				{
					slave_ai tmpslave = (slave_ai)malloc(sizeof(SLAVE_AI));
					tmpslave = (slave_ai)ec_slave[i].inputs;

					slave_arr[i].ptrToSlave1 = tmpslave;						//slave_arr[i]指向当前结构体
					slave_arr[i].ptrToSlave2 = NULL;
					tmpslave->slaveinfo = &slave_arr[i];

					slave_ai ptr = ais->next;
					ais->next = tmpslave;
					tmpslave->next = ptr;
					break;
				}
				case 4:				//AO
				{
					slave_ao tmpslave = (slave_ao)malloc(sizeof(SLAVE_AO));
					tmpslave = (slave_ao)ec_slave[i].outputs;

					slave_arr[i].ptrToSlave1 = tmpslave;						//slave_arr[i]指向当前结构体
					slave_arr[i].ptrToSlave2 = NULL;
					tmpslave->slaveinfo = &slave_arr[i];

					slave_ao ptr = aos->next;
					aos->next = tmpslave;
					tmpslave->next = ptr;
					break;
				}
				default:
					printf("从站类型扫描错误，请检查第%d个slave\n", i);		//说明Slave的可读名称name不对
					break;
			}
		}
	}
}
//slave struct requirement.
int getSlaveInfoImpl(SLAVET_ARR *slave, char* slaveName, int id) {
	if (slave_arr == NULL) {
		printf("没有检查到从站信息！");
		return 0;
	}

	slave->id = slave_arr[id].id;
	slave->name = NULL;
	//slaveName = slave_arr[id].name;
	slave->type = slave_arr[id].type;
	slave->ptrToSlave1 = NULL;	//useless.
	slave->channelNum = slave_arr[id].channelNum;
	if (slave_arr[id].name != NULL) strcpy(slaveName, slave_arr[id].name);

	return id;
}