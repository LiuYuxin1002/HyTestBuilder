#pragma once

#include "SlaveWirteManager.h"

#define DEFINE_SLEEP_TIME 300
#define DO 2
#define AO 4

bool runningState = true;
HANDLE wthread;

DWORD WINAPI writeSlaveThread(LPVOID lpParameter) {
	int wkc;
	while (runningState) {
		ec_send_processdata();
		wkc = ec_receive_processdata(2*DEFINE_SLEEP_TIME);

		int sleepTime = lpParameter == NULL ? DEFINE_SLEEP_TIME : *(int*)lpParameter;//���û���趨ֵ������Ĭ��ֵ
		osal_usleep(sleepTime);
	}
	return 0;
}



int writeSlave(int slaveId, int channelId, int value) {
	SLAVET_ARR slave = slave_arr[slaveId];
	int type = slave.type;

	if (channelId > slave.channelNum) {
		printf("û��ô��˿ڣ����channel��ֵ\n");
		return -1;
	}

	else if (type != AO) {				//����������ֻ�������������
		printf("����������ֻ������������������slaveId��ֵ\n");
		return -2;
	}

	slave_ao tmp = (slave_ao)slave.ptrToSlave;
	tmp->values[channelId] = value;

	if (wthread == NULL) wthread = CreateThread(NULL, 0, writeSlaveThread, NULL, 0, NULL);

	return AO;
}

int writeSlave(int slaveId, int channelId, bool value) {
	SLAVET_ARR slave = slave_arr[slaveId];
	int type = slave.type;

	if (channelId > slave.channelNum) {//û��ô��˿�
		printf("û��ô��˿ڣ����channel��ֵ\n");
		return -1;
	}
	else if (type != DO) {				//����������ֻ�������������
		printf("����������ֻ������������������slaveId��ֵ\n");
		return -2;
	}

	slave_do tmp = (slave_do)slave.ptrToSlave;
	tmp->values[channelId] = value;

	if (wthread == NULL) wthread = CreateThread(NULL, 0, writeSlaveThread, NULL, 0, NULL);

	return DO;
}

int slaveWriteSigleDigital(int slaveid, int channelid, bool value) {
	return writeSlave(slaveid, channelid, value);
}

//TODO
int slaveWriteBatchDigital(int *slaveid, int *channelid, bool *value) {
	return 0;

}

int slaveWriteSingleAnalog(int slaveId, int channelId, int value) {
	return writeSlave(slaveId, channelId, value);
}

//TODO
int slaveWriteBatchAnalog(int slaveId, int channelId, int *value) {
	return 0;

}