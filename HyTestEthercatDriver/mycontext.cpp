#pragma once

#include "mycontext.h"
#include "ethercat.h"

ec_adaptert* adapter;

struct adaptert myadapter;
//struct SLAVET_ARR slave_arr[MAX_SLAVE];
//slave_di dis = (slave_di)new SLAVE_DI();	//������ͷ���
//slave_do dos = (slave_do)new SLAVE_DO();
//slave_ai ais = (slave_ai)new SLAVE_AI();
//slave_ao aos = (slave_ao)new SLAVE_AO();

const int SLAVE_TYPE_ID = 2;		//type����λ
const int SLAVE_CHANNEL_ID = 5;		//channel����λ

									//�ֲ�����
OSAL_THREAD_HANDLE thread1;
//char IOmap[MAP_SIZE];
boolean needlf;
boolean inOP;
volatile int wkc;
uint8 currentgroup = 0;

int setIntegerValueImpl(int slaveId, int channel, int value) {
	SLAVET_ARR* slave = &slave_arr[slaveId];
	int type = slave->type;

	if (channel > slave->channelNum) {	//û��ô��˿�
		printf("û��ô��˿ڣ����channel��ֵ\n");
		return -1;
	}
	else if (type != 4) {				//����ģ����ֻ����ģ�������
		printf("����ģ����ֻ����ģ������������slaveId��ֵ\n");
		return -2;
	}

	slave_ao tmp = (slave_ao)slave->ptrToSlave;
	tmp->values[channel] = value;
	ec_send_processdata();
	int wkc = ec_receive_processdata(3000);

	return wkc;
}

int setBoolValueImpl(int slaveId, int channel, boolean value) {
	SLAVET_ARR* slave = &slave_arr[slaveId];
	int type = slave->type;

	if (channel > slave->channelNum) {//û��ô��˿�
		printf("û��ô��˿ڣ����channel��ֵ\n");
		return -1;
	}
	else if (type != 2) {				//����������ֻ�������������
		printf("����������ֻ������������������slaveId��ֵ\n");
		return -2;
	}

	slave_do tmp = (slave_do)slave->ptrToSlave;
	tmp->values[channel] = value;

	ec_send_processdata();
	int wkc = ec_receive_processdata(3000);
	
	return wkc;
}