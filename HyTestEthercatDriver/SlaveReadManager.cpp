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

boolean runningState;
HANDLE hthread;

char strbuf[64] = "";

#define DI 1
#define AI 3

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
	if (ec_slavecount != -1) {
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

DWORD WINAPI readSlaveThread(LPVOID lpParameter) {
	while (runningState)
	{
		long long timestamp = getSystemTime();
		char *timeStampStr = ltos(timestamp);

		ec_slavecount = 1;

		map<char*, char*>* tmpMap = new map<char *, char *>();
		int slaveid, channelid;

		char *value;

		for (int i = 0; i < ec_slavecount; i++)
		{
			slaveid = i;

			if (slave_arr[i].type == DI) {
				slave_di tmpDI = (SLAVE_DI*)slave_arr[i].ptrToSlave;

				for (int j = 0; j < slave_arr[i].channelNum; j++) {
					channelid = j;
					char *keystr = new char[10];
					sprintf(keystr, "%d_%d", slaveid, channelid);	//channelID

					value = tmpDI->values[j] ? "1" : "0";

					tmpMap->insert(pair<char*, char*>(keystr, value));
				}
			}
			else if (slave_arr[i].type == AI) {
				slave_ai tmpAI = (SLAVE_AI*)slave_arr[i].ptrToSlave;

				for (int j = 0; j < slave_arr[i].channelNum; j++) {
					channelid = j;
					char *keystr = new char[10];

					sprintf(keystr, "%d_%d", slaveid, channelid);	//key
					sprintf(value, "%d", tmpAI->values[j]);			//value

					tmpMap->insert(pair<char*, char*>(keystr, value));
				}
			}
		}

		addKeyValues(timeStampStr, *tmpMap);//Ìí¼Óµ½redis
		Sleep(100);
	}
	return 0;
}

int slavePrepareToRead() {
	checkRedisState();
	int wkc = checkSlaveState();
	cout << "prepare finished" << endl;
	runningState = true;
	return wkc;
}

int slaveReadStart() {
	if(hthread==NULL) hthread = CreateThread(NULL,0,readSlaveThread,NULL,0,NULL);
	return 0;
}

int slaveReadSuspend() {
	if(hthread!=NULL) SuspendThread(hthread);
	return 0;
}

int slaveReadStop() {
	if(hthread!=NULL) CloseHandle(hthread);
	return 0;
}
