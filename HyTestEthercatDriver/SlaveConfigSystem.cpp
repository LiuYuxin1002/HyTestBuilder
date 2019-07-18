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
const int COUPLER_TYPE = 10;		//耦合器类型
const int SLAVE_TYPE_ID = 2;		//type所在位
const int SLAVE_CHANNEL_ID = 5;		//channel所在位

int initSlaveConfigInfo() {
	int cnt, i, j, nSM;
	uint16 ssigen;
	int expectedWKC;

	/* initialise SOEM, bind socket to ifname */
	if (ec_init(ifbuf))
	{
		/* find and auto-config slaves */
		if (ec_config(FALSE, &IOmap) > 0)
		{
			ec_configdc();
			while (EcatError) printf("%s", ec_elist2string());
			printf("%d slaves found and configured.\n", ec_slavecount);
			expectedWKC = (ec_group[0].outputsWKC * 2) + ec_group[0].inputsWKC;
			printf("计算workcounter %d\n", expectedWKC);
			/* wait for all slaves to reach SAFE_OP state */
			ec_statecheck(0, EC_STATE_SAFE_OP, EC_TIMEOUTSTATE * 3);
			if (ec_slave[0].state != EC_STATE_SAFE_OP)
			{
				printf("Not all slaves reached safe operational state.\n");
				ec_readstate();
				for (i = 1; i <= ec_slavecount; i++)
				{
					if (ec_slave[i].state != EC_STATE_SAFE_OP)
					{
						printf("Slave %d State=%2x StatusCode=%4x : %s\n",
							i, ec_slave[i].state, ec_slave[i].ALstatuscode, ec_ALstatuscode2string(ec_slave[i].ALstatuscode));
					}
				}
			}
			ec_readstate();

			/////////////////////////////////////OP STATE/////////////////////////////////////
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

			initLocalSlaveInfo();
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

	for (int i = 0; i < ec_slavecount+1; i++) {
		if (i == 0) {									//不处理第一个
			continue;
		}
		else if(ec_slave[i].outputs==0 && ec_slave[i].inputs==0)	//耦合器、伺服驱动器	TODO: 伺服驱动器是否这样的情况待定
		{
			slave_arr[i].name = ec_slave[i].name;
			slave_arr[i].type = COUPLER_TYPE;
			slave_arr[i].id = ec_slave[i].eep_id;
			slave_arr[i].channelNum = -1;
			slave_arr[i].ptrToSlave = NULL;
			
			continue;
		}

		char* name	= ec_slave[i].name;	//从站可读名称

		slave_arr[i].id		= ec_slave[i].eep_id;
		slave_arr[i].name	= name;
		slave_arr[i].type	= name[SLAVE_TYPE_ID] - '0';			//获取类型
		slave_arr[i].channelNum = name[SLAVE_CHANNEL_ID] - '0';		//获取从站channel数量		//TODO: channel数量大于8会失败

		//判断从站类型
		switch (slave_arr[i].type)
		{
			case 1:				//DI
			{
				slave_di tmpslave = (slave_di)malloc(sizeof(SLAVE_DI));
				tmpslave = (slave_di)ec_slave[i].inputs;				//将当前读取值写入di结构体
			
				slave_arr[i].ptrToSlave = tmpslave;						//slave_arr[i]指向当前结构体
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

				slave_arr[i].ptrToSlave = tmpslave;
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

				slave_arr[i].ptrToSlave = tmpslave;
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

				slave_arr[i].ptrToSlave = tmpslave;
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
	slave->ptrToSlave = NULL;	//useless.
	slave->channelNum = slave_arr[id].channelNum;
	if (slave_arr[id].name != NULL ) strcpy(slaveName, slave_arr[id].name);
	
	return id;
}