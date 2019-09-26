#pragma once

#include "SlaveWirteManager.h"

//write analog
int writeSlave(int slaveId, int channelId, int value) {
	SLAVET_ARR slave = slave_arr[slaveId];
	int type = slave.type;
	int chNum = slave.channelNum;

	if (type != TYPE_AO && type != TYPE_SERVO) return -1;
	if (channelId > chNum) return -2;
	
	if (type == TYPE_AO) {
		slave_ao tmp = (slave_ao)slave.ptrToSlave1;
		WaitForSingleObject(g_hMutex, INFINITE);		//lock
		while (tmp->values[channelId] != value) {
			tmp->values[channelId] = value;
			osal_usleep(100);
		}
		ReleaseMutex(g_hMutex);							//unlock
	}
	if (type == TYPE_SERVO) {	//TODO: 根据伺服驱动器的不同需要调整
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
	}
	
	return type;
}
//write bool
int writeSlave(int slaveId, int channelId, bool value) {
	SLAVET_ARR slave = slave_arr[slaveId];
	int type = slave.type;

	if (channelId > slave.channelNum) {		//if id > channel num
		printf("没那么多端口，检查channel的值\n");
		return -1;
	}
	else if (type != TYPE_DO) {				//if type dismatch DO
		printf("设置数字量只能是数字量输出，检查slaveId的值\n");
		return -2;
	}

	slave_do tmp = (slave_do)slave.ptrToSlave1;
	WaitForSingleObject(g_hMutex, INFINITE);		//lock
	while (tmp->values[channelId] != value) {
		tmp->values[channelId] = value;
		osal_usleep(100);
	}
	ReleaseMutex(g_hMutex);		//unlock

	return TYPE_DO;
}

int slaveWriteSigleDigital(int slaveid, int channelid, bool value) {
	return writeSlave(slaveid, channelid, value);
}

//TODO
int slaveWriteBatchDigital(int *slaveid, int *channelid, bool *value) {
	int n = sizeof(&slaveid) / sizeof(bool);
	for (int i = 0; i < n; i++) {
		writeSlave(slaveid[i], channelid[i], value[i]);
	}
	return TYPE_DO;
}

int slaveWriteSingleAnalog(int slaveId, int channelId, int value) {
	return writeSlave(slaveId, channelId, value);
}

//TODO
int slaveWriteBatchAnalog(int* slaveId, int* channelId, int *value) {
	int n = sizeof(&slaveId) / sizeof(int);
	for (int i = 0; i < n; i++) {
		writeSlave(slaveId[i], channelId[i], value[i]);
	}
	return TYPE_AO;
}