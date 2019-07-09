#pragma once

#include "SlaveWirteManager.h"

#define DEFINE_SLEEP_TIME 300

bool runningState = true;
HANDLE wthread;

DWORD WINAPI writeSlaveThread(LPVOID lpParameter) {
	int wkc;
	while (runningState) {
		ec_send_processdata();
		wkc = ec_receive_processdata(300);

		int sleepTime = lpParameter == NULL ? DEFINE_SLEEP_TIME : *(int*)lpParameter;
		Sleep(sleepTime);
	}
	return 0;
}

int writeSlave(int salveId, int channelId, int value) {
	return 0;
}

int writeSlave(int salveId, int channelId, bool value) {
	
	return 0;
}
//extern struct SLAVET_ARR slave_arr[MAX_SLAVE];
int slaveWriteSigleDigital(int slaveid, int channelid, bool value) {
	SLAVET_ARR slave = slave_arr[slaveid];
	int type = slave.type;

	if (channelid > slave.channelNum) {//没那么多端口
		printf("没那么多端口，检查channel的值\n");
		return -1;
	}
	else if (type != 2) {				//设置数字量只能是数字量输出
		printf("设置数字量只能是数字量输出，检查slaveId的值\n");
		return -2;
	}
	
	slave_do tmp = (slave_do)slave.ptrToSlave;
	tmp->values[channelid] = value;
		
	if (wthread == NULL) wthread = CreateThread(NULL, 0, writeSlaveThread, NULL, 0, NULL);
	
	return 0;
}

int slaveWriteBatchDigital(int *slaveid, int *channelid, bool *value) {
	return 0;

}

int slaveWriteSingleAnalog(int slaveId, int channelId, int value) {
	return 0;

}

int slaveWriteBatchAnalog(int slaveId, int channelId, int *value) {
	return 0;

}