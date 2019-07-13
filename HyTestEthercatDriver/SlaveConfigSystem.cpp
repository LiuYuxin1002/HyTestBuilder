#pragma once

#include "SlaveConfigSystem.h"

//��������
char IOmap[MAP_SIZE];

struct SLAVET_ARR slave_arr[MAX_SLAVE];
slave_di dis = (slave_di)new SLAVE_DI();	//������ͷ���
slave_do dos = (slave_do)new SLAVE_DO();
slave_ai ais = (slave_ai)new SLAVE_AI();
slave_ao aos = (slave_ao)new SLAVE_AO();

//�ֲ�ȫ�ֱ���
const int SLAVE_TYPE_ID = 2;		//type����λ
const int SLAVE_CHANNEL_ID = 5;		//channel����λ

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
			printf("����workcounter %d\n", expectedWKC);
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
			return ec_slavecount;
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

int getSlaveIdFromName(char* name) {
	int slaveId = 0;
	for (int i = 2; i < 6; i++) {//��ʾ2,3,4,5λ
		slaveId = (name[i] - '0') + slaveId * 10;
	}
	return slaveId;
}

void initLocalSlaveInfo() {
	if (ec_slavecount == 0) {
		return;
	}
	//for (int i = 0; i < ec_slavecount+1; i++) {
	//	cout << ec_slave[i].name << endl;
	//	cout << "�˿ں�λ��"<<ec_slave[i].name[5] << endl;
	//}

	for (int i = 0; i < ec_slavecount+1; i++) {
		if (i == 0 || i==1) {									//�������һ��
			continue;
		}

		char* name = ec_slave[i].name;					//��վ�ɶ�����
		int type = name[SLAVE_TYPE_ID] - '0';			//��ȡ����
		int channel = name[SLAVE_CHANNEL_ID] - '0';		//��ȡ��վchannel����

		slave_arr[i].name = getSlaveIdFromName(name);						//��ֵ
		slave_arr[i].type = type;
		slave_arr[i].channelNum = channel;

		//cout << "ID: " << slave_arr[i].id << endl;
		//cout << slave_arr[i].name << endl;
		//cout << slave_arr[i].type << endl;
		//cout << slave_arr[i].channelNum << endl;

		//�жϴ�վ����
		switch (type)
		{
		case 1:				//DI
		{
			slave_di tmpslave = (slave_di)malloc(sizeof(SLAVE_DI));
			tmpslave = (slave_di)ec_slave[i].inputs;				//����ǰ��ȡֵд��di�ṹ��
			
			slave_arr[i].ptrToSlave = tmpslave;						//slave_arr[i]ָ��ǰ�ṹ��
			tmpslave->slaveinfo = &slave_arr[i];					//ָ��ȥ��˫������

			slave_di ptr = dis->next;								//����ͷ���
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
			printf("��վ����ɨ����������%d��slave\n", i);		//˵��Slave�Ŀɶ�����name����
			break;
		}
	}
}

int getSlaveInfoImpl(SLAVET_ARR *slave, int id) {
	if (slave_arr == NULL) {
		printf("û�м�鵽��վ��Ϣ��");
		return -1;
	}
	
	//slave->id = 1806;
	//slave->name = 1234;
	//slave->type = 3;
	//slave->ptrToSlave = NULL;
	//slave->channelNum = 2;

	slave->id = slave_arr[id].id;
	slave->name = slave_arr[id].name;
	slave->type = slave_arr[id].type;
	slave->ptrToSlave = NULL;
	slave->channelNum = slave_arr[id].channelNum;
	
	return 0;
}