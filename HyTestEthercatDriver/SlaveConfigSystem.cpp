#pragma once

#include "SlaveConfigSystem.h"

//变量声明
char IOmap[MAP_SIZE];

struct SLAVET_ARR slave_arr[MAX_SLAVE];
slave_di dis = (slave_di)new SLAVE_DI();	//新申请头结点
slave_do dos = (slave_do)new SLAVE_DO();
slave_ai ais = (slave_ai)new SLAVE_AI();
slave_ao aos = (slave_ao)new SLAVE_AO();

//局部全局变量
const int SLAVE_TYPE_ID = 2;		//type所在位
const int SLAVE_CHANNEL_ID = 5;		//channel所在位

#define DEFINE_SLEEP_TIME 1000

bool runningState = true;
HANDLE wthread;

//循环写线程
DWORD WINAPI writeSlaveThread(LPVOID lpParameter) {
	int wkc;
	//如果没有设定值，就用默认值DEFINE_SLEEP_TIME
	int sleepTime = lpParameter == NULL ? DEFINE_SLEEP_TIME : *(int*)lpParameter;
	while (runningState) {
		ec_send_processdata();
		wkc = ec_receive_processdata(DEFINE_SLEEP_TIME/2);

		osal_usleep(sleepTime * 8);
	}
	return 0;
}


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

			/////////////////////////////////////OP STATE/////////////////////////////////////
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

			if (wthread == NULL)	//启动循环
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
		else if (ec_slave[i].inputs != 0 && ec_slave[i].outputs != 0)	//伺服驱动器
		{
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

			slave_arr[i].ptrToSlave1 = tmpslave;						//slave_arr[i]指向当前结构体
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