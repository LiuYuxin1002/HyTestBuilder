#pragma once

#include "SlaveConfigSystem.h"

//变量声明
//char* ifbuf;
char IOmap[MAP_SIZE];

struct SLAVET_ARR slave_arr[MAX_SLAVE];
slave_di dis = (slave_di)new SLAVE_DI();	//新申请头结点
slave_do dos = (slave_do)new SLAVE_DO();
slave_ai ais = (slave_ai)new SLAVE_AI();
slave_ao aos = (slave_ao)new SLAVE_AO();

//局部全局变量
const int SLAVE_TYPE_ID = 2;		//type所在位
const int SLAVE_CHANNEL_ID = 5;		//channel所在位

int initSlaveConfigInfo() {
	int cnt, i, j, nSM;
	uint16 ssigen;
	int expectedWKC;

	printf("Starting slaveinfo\n");

	/* initialise SOEM, bind socket to ifname */
	if (ec_init(ifbuf))
	{
		printf("ec_init on %s succeeded.\n", ifbuf);
		/* find and auto-config slaves */
		if (ec_config(FALSE, &IOmap) > 0)
		{
			ec_configdc();
			while (EcatError) printf("%s", ec_elist2string());
			printf("%d slaves found and configured.\n", ec_slavecount);
			expectedWKC = (ec_group[0].outputsWKC * 2) + ec_group[0].inputsWKC;
			printf("Calculated workcounter %d\n", expectedWKC);
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
			//打印从站具体信息
			//for (cnt = 1; cnt <= ec_slavecount; cnt++)
			//{
			//	printf("\nSlave:%d\n Name:%s\n Output size: %dbits\n Input size: %dbits\n State: %d\n Delay: %d[ns]\n Has DC: %d\n",
			//		cnt, ec_slave[cnt].name, ec_slave[cnt].Obits, ec_slave[cnt].Ibits,
			//		ec_slave[cnt].state, ec_slave[cnt].pdelay, ec_slave[cnt].hasdc);
			//	if (ec_slave[cnt].hasdc) printf(" DCParentport:%d\n", ec_slave[cnt].parentport);
			//	printf(" Activeports:%d.%d.%d.%d\n", (ec_slave[cnt].activeports & 0x01) > 0,
			//		(ec_slave[cnt].activeports & 0x02) > 0,
			//		(ec_slave[cnt].activeports & 0x04) > 0,
			//		(ec_slave[cnt].activeports & 0x08) > 0);
			//	printf(" Configured address: %4.4x\n", ec_slave[cnt].configadr);
			//	printf(" Man: %8.8x ID: %8.8x Rev: %8.8x\n", (int)ec_slave[cnt].eep_man, (int)ec_slave[cnt].eep_id, (int)ec_slave[cnt].eep_rev);
			//	for (nSM = 0; nSM < EC_MAXSM; nSM++)
			//	{
			//		if (ec_slave[cnt].SM[nSM].StartAddr > 0)
			//			printf(" SM%1d A:%4.4x L:%4d F:%8.8x Type:%d\n", nSM, ec_slave[cnt].SM[nSM].StartAddr, ec_slave[cnt].SM[nSM].SMlength,
			//			(int)ec_slave[cnt].SM[nSM].SMflags, ec_slave[cnt].SMtype[nSM]);
			//	}
			//	for (j = 0; j < ec_slave[cnt].FMMUunused; j++)
			//	{
			//		printf(" FMMU%1d Ls:%8.8x Ll:%4d Lsb:%d Leb:%d Ps:%4.4x Psb:%d Ty:%2.2x Act:%2.2x\n", j,
			//			(int)ec_slave[cnt].FMMU[j].LogStart, ec_slave[cnt].FMMU[j].LogLength, ec_slave[cnt].FMMU[j].LogStartbit,
			//			ec_slave[cnt].FMMU[j].LogEndbit, ec_slave[cnt].FMMU[j].PhysStart, ec_slave[cnt].FMMU[j].PhysStartBit,
			//			ec_slave[cnt].FMMU[j].FMMUtype, ec_slave[cnt].FMMU[j].FMMUactive);
			//	}
			//	printf(" FMMUfunc 0:%d 1:%d 2:%d 3:%d\n",
			//		ec_slave[cnt].FMMU0func, ec_slave[cnt].FMMU1func, ec_slave[cnt].FMMU2func, ec_slave[cnt].FMMU3func);
			//	printf(" MBX length wr: %d rd: %d MBX protocols : %2.2x\n", ec_slave[cnt].mbx_l, ec_slave[cnt].mbx_rl, ec_slave[cnt].mbx_proto);
			//	ssigen = ec_siifind(cnt, ECT_SII_GENERAL);
			//	/* SII general section */
			//	if (ssigen)
			//	{
			//		ec_slave[cnt].CoEdetails = ec_siigetbyte(cnt, ssigen + 0x07);
			//		ec_slave[cnt].FoEdetails = ec_siigetbyte(cnt, ssigen + 0x08);
			//		ec_slave[cnt].EoEdetails = ec_siigetbyte(cnt, ssigen + 0x09);
			//		ec_slave[cnt].SoEdetails = ec_siigetbyte(cnt, ssigen + 0x0a);
			//		if ((ec_siigetbyte(cnt, ssigen + 0x0d) & 0x02) > 0)
			//		{
			//			ec_slave[cnt].blockLRW = 1;
			//			ec_slave[0].blockLRW++;
			//		}
			//		ec_slave[cnt].Ebuscurrent = ec_siigetbyte(cnt, ssigen + 0x0e);
			//		ec_slave[cnt].Ebuscurrent += ec_siigetbyte(cnt, ssigen + 0x0f) << 8;
			//		ec_slave[0].Ebuscurrent += ec_slave[cnt].Ebuscurrent;
			//	}
			//	printf(" CoE details: %2.2x FoE details: %2.2x EoE details: %2.2x SoE details: %2.2x\n",
			//		ec_slave[cnt].CoEdetails, ec_slave[cnt].FoEdetails, ec_slave[cnt].EoEdetails, ec_slave[cnt].SoEdetails);
			//	printf(" Ebus current: %d[mA]\n only LRD/LWR:%d\n",
			//		ec_slave[cnt].Ebuscurrent, ec_slave[cnt].blockLRW);
			//	if ((ec_slave[cnt].mbx_proto & ECT_MBXPROT_COE) && printSDO)
			//		si_sdo(cnt);
			//	if (printMAP)
			//	{
			//		if (ec_slave[cnt].mbx_proto & ECT_MBXPROT_COE)
			//			si_map_sdo(cnt);
			//		else
			//			si_map_sii(cnt);
			//	}
			//}
			return ec_slavecount;
		}
		else
		{
			printf("No slaves found!\n");
			return 0;
		}
		//printf("End slaveinfo, close socket\n");
		/* stop SOEM, close socket */
		//ec_close();
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

	//if (slave_arr == NULL) {	//为slave_arr申请空间
	//	slave_arr = (SLAVET_ARR*)malloc(sizeof(struct SLAVET_ARR));
	//}

	for (int i = 0; i < ec_slavecount; i++) {
		if (i == 0) {									//不处理第一个
			continue;
		}

		char* name = ec_slave[i].name;					//从站可读名称
		int type = name[SLAVE_TYPE_ID] - '0';			//获取类型
		int channel = name[SLAVE_CHANNEL_ID] - '0';		//获取从站channel数量

		slave_arr[i].name = name;						//赋值
		slave_arr[i].type = type;
		slave_arr[i].channelNum = channel;

		//判断从站类型
		switch (type)
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
		}
		default:
			printf("从站类型扫描错误，请检查第%d个slave\n", i);		//说明Slave的可读名称name不对
			break;
		}
	}
}

int getSlaveInfoImpl(SLAVET_ARR *slave, int id) {
	if (slave_arr == NULL) {
		printf("没有检查到从站信息！");
		return -1;
	}
	//SLAVET_ARR* slaveinfo = &slave_arr[id];
	//SLAVET_ARR* slaveinfo = new SLAVET_ARR();
	slave->channelNum = 2;
	slave->id = 1;
	slave->name = "EL3002";
	slave->type = 3;
	slave->ptrToSlave = NULL;
	
	return 0;
}