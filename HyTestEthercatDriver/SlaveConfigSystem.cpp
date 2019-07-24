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
const int COUPLER_TYPE = 10;		//���������
const int SERVO_TYPE = 20;			//�ŷ�������
const int SLAVE_TYPE_ID = 2;		//type����λ
const int SLAVE_CHANNEL_ID = 5;		//channel����λ

int initSlaveConfigInfo() {
	int i, expectedWKC;

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
		if (i == 0) {									//�������һ��
			continue;
		}
		else if (ec_slave[i].outputs == 0 && ec_slave[i].inputs == 0)	//�����
		{
			slave_arr[i].name = ec_slave[i].name;
			slave_arr[i].type = COUPLER_TYPE;
			slave_arr[i].id = ec_slave[i].eep_id;
			slave_arr[i].channelNum = -1;
			slave_arr[i].ptrToSlave1 = NULL;
			slave_arr[i].ptrToSlave2 = NULL;

			continue;
		}
		//TODO: �ŷ�������ʹ��ǰ��Ҫ�������ã��μ�"�����ŷ�EtherCATʹ��˵��.pdf"��5-4-4��.
		else if (ec_slave[i].inputs != 0 && ec_slave[i].outputs != 0)	//�ŷ�������
		{
			slave_arr[i].name = ec_slave[i].name;
			slave_arr[i].type = SERVO_TYPE;
			slave_arr[i].id = ec_slave[i].eep_id;
			slave_arr[i].channelNum = 16;		//TODO: �����ŷ��������Ĳ�ͬ����ͬ

			//�����ŷ��������ṹ��ӳ��
			pservo_input tmp_in = (pservo_input)malloc(sizeof(SLAVE_SERVO_IN));
			pservo_output tmp_out = (pservo_output)malloc(sizeof(SLAVE_SERVO_OUT));

			tmp_in = (pservo_input)ec_slave[i].inputs;	//��IOmapӳ��
			tmp_out = (pservo_output)ec_slave[i].outputs;

			slave_arr[i].ptrToSlave1 = tmp_in;	//slave array -> servo struct
			slave_arr[i].ptrToSlave2 = tmp_out;
			tmp_in->slaveInfo = &slave_arr[i];	//servo struct -> slave arr
			tmp_out->slaveInfo = &slave_arr[i];

			//TODO: �Ƿ���Ҫ�����ŷ�������������
			continue;
		}

		char* name = ec_slave[i].name;	//��վ�ɶ�����

		slave_arr[i].id = ec_slave[i].eep_id;
		slave_arr[i].name = name;
		slave_arr[i].type = name[SLAVE_TYPE_ID] - '0';			//��ȡ����
		slave_arr[i].channelNum = name[SLAVE_CHANNEL_ID] - '0';		//��ȡ��վchannel����		//TODO: channel��������8��ʧ��

																	//�жϴ�վ����
		switch (slave_arr[i].type)
		{
		case 1:				//DI
		{
			slave_di tmpslave = (slave_di)malloc(sizeof(SLAVE_DI));
			tmpslave = (slave_di)ec_slave[i].inputs;				//����ǰ��ȡֵд��di�ṹ��

			slave_arr[i].ptrToSlave1 = tmpslave;						//slave_arr[i]ָ��ǰ�ṹ��
			slave_arr[i].ptrToSlave2 = NULL;
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

			slave_arr[i].ptrToSlave1 = tmpslave;						//slave_arr[i]ָ��ǰ�ṹ��
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

			slave_arr[i].ptrToSlave1 = tmpslave;						//slave_arr[i]ָ��ǰ�ṹ��
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

			slave_arr[i].ptrToSlave1 = tmpslave;						//slave_arr[i]ָ��ǰ�ṹ��
			slave_arr[i].ptrToSlave2 = NULL;
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

int getSlaveInfoImpl(SLAVET_ARR *slave, char* slaveName, int id) {
	if (slave_arr == NULL) {
		printf("û�м�鵽��վ��Ϣ��");
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