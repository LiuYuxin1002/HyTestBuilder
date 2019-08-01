#pragma once

#include "SlaveWirteManager.h"


int writeSlave(int slaveId, int channelId, int value) {
	SLAVET_ARR slave = slave_arr[slaveId];
	int type = slave.type;
	int chNum = slave.channelNum;

	if (type != TYPE_AO && type != TYPE_SERVO) return -1;
	if (channelId > chNum) return -2;
	
	if (type == TYPE_AO) {
		slave_ao tmp = (slave_ao)slave.ptrToSlave1;
		tmp->values[channelId] = value;
		//return TYPE_AO;
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
		//return TYPE_SERVO;
	}
	
	return type;
}

int writeSlave(int slaveId, int channelId, bool value) {
	SLAVET_ARR slave = slave_arr[slaveId];
	int type = slave.type;

	if (channelId > slave.channelNum) {		//û��ô��˿ڣ�ǿ��д�������ڴ���Ⱦ
		printf("û��ô��˿ڣ����channel��ֵ\n");
		return -1;
	}
	else if (type != TYPE_DO) {				//����������ֻ�������������
		printf("����������ֻ������������������slaveId��ֵ\n");
		return -2;
	}

	slave_do tmp = (slave_do)slave.ptrToSlave1;
	tmp->values[channelId] = value;

	//if (wthread == NULL) wthread = CreateThread(NULL, 0, writeSlaveThread, NULL, 0, NULL);	//����д�߳�

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