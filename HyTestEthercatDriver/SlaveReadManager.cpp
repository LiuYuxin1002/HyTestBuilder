#pragma once

#include "SlaveReadManager.h"
#include <stdio.h>
#include <windows.h>
#include <sys/timeb.h>
#include <iostream>
#include <string>
#include <map>
#include "HiredisHelper.h"

using namespace std;

HANDLE hthread;

char strbuf[64] = "";

#define DEFINE_SLEEP_TIME1 300
#define DI 1
#define AI 3

bool readState = true;
HANDLE rthread;

long long getSystemTime() {
	struct timeb t;
	ftime(&t);
	return 1000 * t.time + t.millitm;
}

char* ltos(long long lld)
{
	sprintf(strbuf, "%lld", lld);
	return strbuf;
}

map<char*, char*> getDataMap() {
	for (int i = 0; i < ec_slavecount; i++)
	{
		int type = slave_arr[i].type;

		slave_arr[i].name;
	}
	map<char*, char*>* dataMap = new map<char *, char *>();
	return *dataMap;
}

int checkSlaveState() {
	if (ec_slavecount == 0) {
		SLAVET_ARR *newSlave = new SLAVET_ARR();
		newSlave->channelNum = 4;
		newSlave->id = 0;
		newSlave->name = 1234;
		newSlave->type = 1;

		slave_ai newai = (slave_ai)malloc(sizeof(struct SLAVE_AI));
		for (int i = 0; i < newSlave->channelNum; i++) {
			newai->values[i] = i;
		}

		newSlave->ptrToSlave = newai;
		slave_arr[0] = *newSlave;

		cout << "check slave finish" << endl;
		return 1;
	}
	else {
		return 0;
	}
}

int checkRedisState() {
	getRedisClient();
	if (client == NULL) {
		cout << "redis client get failed" << endl;
		return -1;
	}
	cout << "redis client get success" << endl;
	return 0;
}

int slavePrepareToRead() {
	checkRedisState();
	int wkc = checkSlaveState();
	cout << "prepare finished" << endl;
	readState = true;
	return wkc;
}

int slaveReadStart() {
	//if(hthread==NULL) hthread = CreateThread(NULL,0,readSlaveThread,NULL,0,NULL);
	return 0;
}

int slaveReadSuspend() {
	if(hthread!=NULL) SuspendThread(hthread);
	return 0;
}

int slaveReadResume() {
	if (hthread != NULL) ResumeThread(hthread);
	return 1;
}

int slaveReadStop() {
	if(hthread!=NULL) CloseHandle(hthread);
	return 0;
}

int getDigitalValueImpl(int deviceId, int channelId) {
	ec_send_processdata();
	int wkc = ec_receive_processdata(EC_TIMEOUTRET);

	SLAVE_DI* tmpSlave = (SLAVE_DI*)slave_arr[deviceId].ptrToSlave;
	return tmpSlave->values[channelId];
}

int getAnalogValueImpl(int deviceId, int channelId) {
	ec_send_processdata();
	int wkc = ec_receive_processdata(EC_TIMEOUTRET);

	SLAVE_AI* tmpSlave = (SLAVE_AI*)slave_arr[deviceId].ptrToSlave;
	int16 value = tmpSlave->values[channelId];
	return tmpSlave->values[channelId];
}