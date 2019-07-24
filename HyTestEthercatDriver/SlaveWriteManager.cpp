#pragma once

#include "SlaveWirteManager.h"

#define DEFINE_SLEEP_TIME 300000

bool runningState = true;
HANDLE wthread;

//ѭ��д�߳�
DWORD WINAPI writeSlaveThread(LPVOID lpParameter) {
	int wkc;
	while (runningState) {
		ec_send_processdata();
		wkc = ec_receive_processdata(2*DEFINE_SLEEP_TIME);
		
		//���û���趨ֵ������Ĭ��ֵDEFINE_SLEEP_TIME
		int sleepTime = lpParameter == NULL ? DEFINE_SLEEP_TIME : *(int*)lpParameter;
		osal_usleep(sleepTime);
	}
	return 0;
}

int writeSlave(int slaveId, int channelId, int value) {
	SLAVET_ARR slave = slave_arr[slaveId];
	int type = slave.type;
	int chNum = slave.channelNum;

	if (type != TYPE_AO && type != TYPE_SERVO) return -1;
	if (channelId > chNum) return -2;
	
	if (type == TYPE_AO) {
		slave_ao tmp = (slave_ao)slave.ptrToSlave1;
		tmp->values[channelId] = value;

		if (wthread == NULL)	//����ѭ��
			wthread = CreateThread(NULL, 0, writeSlaveThread, NULL, 0, NULL);

		return TYPE_AO;
	}
	if (type == TYPE_SERVO) {	//TODO: �����ŷ��������Ĳ�ͬ��Ҫ����
		pservo_output tmpSlave = (pservo_output)slave.ptrToSlave2;
		switch (channelId)
		{
		case 1:
			tmpSlave->controlWord = value;
			break;
		case 2:
			tmpSlave->operationMode = value;
			break;
		case 3:
			tmpSlave->maxTorque = value;
			break;
		case 4:
			tmpSlave->targetPositon = value;
			break;
		case 5:
			tmpSlave->touchProbeFunction = value;
			break;
		case 6:
			tmpSlave->targetVelocity = value;
			break;
		default:
			break;
		}
		return TYPE_SERVO;
	}
	
}

int writeSlave(int slaveId, int channelId, bool value) {
	SLAVET_ARR slave = slave_arr[slaveId];
	int type = slave.type;

	if (channelId > slave.channelNum) {//û��ô��˿�
		printf("û��ô��˿ڣ����channel��ֵ\n");
		return -1;
	}
	else if (type != TYPE_DO) {				//����������ֻ�������������
		printf("����������ֻ������������������slaveId��ֵ\n");
		return -2;
	}

	slave_do tmp = (slave_do)slave.ptrToSlave1;
	tmp->values[channelId] = value;

	if (wthread == NULL) wthread = CreateThread(NULL, 0, writeSlaveThread, NULL, 0, NULL);

	return TYPE_DO;
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