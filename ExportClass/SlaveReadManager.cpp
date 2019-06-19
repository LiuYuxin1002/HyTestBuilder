#pragma once

#include "SlaveReadManager.h"
#include <stdio.h>
#include <process.h>
#include <windows.h>
#include <sys/timeb.h>
#include <iostream>
#include <string>
#include <map>
#include "HiredisHelper.h"

using namespace std;

boolean runningState;

long long getSystemTime() {
	struct timeb t;
	ftime(&t);
	return 1000 * t.time + t.millitm;
}

char* ltos(long long lld)
{
	char strbuf[1024];
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
		newSlave->name = 3234;
		newSlave->type = 3;

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
	runningState = true;
	return wkc;
}

#define DI 1
#define AI 3

void readSlave(void*) {
	while (runningState)
	{
		long long timestamp = getSystemTime();
		char* timeStampStr = ltos(timestamp);

		cout << "timestamp" << timeStampStr << endl;	//debug

		//生成map：slave_channel->value
		map<char*, char*>* tmpMap = new map<char *, char *>();
		int slaveid, channelid;
		char *key_slave, *key_channel;
		char *value;
		for (int i=0; i<ec_slavecount; i++)
		{
			if (slave_arr[i].type == DI) {
				slaveid = i;
				itoa(slaveid, key_slave, 0);
				key_slave = strncat(key_slave, "_", 0);		//right?
				slave_di tmpDI = (SLAVE_DI*)slave_arr[i].ptrToSlave;
				for (int j = 0; j < slave_arr[i].channelNum; j++) {
					channelid = j;
					itoa(channelid, key_channel, 0);
					key_channel = strncat(key_slave, key_channel, 0);
					value = tmpDI->values[j] ? "1" : "0";
					cout << value << endl;							//debug

					tmpMap->insert(pair<char*, char*>(key_channel, value));//right?
					cout << key_channel << "::" << value;
				}
			}
		}


		addKeyValues(timeStampStr,*tmpMap);//添加到redis，现在还不能用，因为不能引用RedisHelper

		
	}
}

//HANDLE __stdcall hthread;
//
//DWORD __stdcall ThreadProc(LPVOID lpParameter) {
//	while (runningState)
//	{
//		long long timestamp = getSystemTime();
//		char* timeStampStr = ltos(timestamp);
//
//		cout << "timestamp" << timeStampStr << endl;	//debug
//
//														//生成map：slave_channel->value
//		map<char*, char*>* tmpMap = new map<char *, char *>();
//		int slaveid, channelid;
//		char *key_slave, *key_channel;
//		char *value;
//		for (int i = 0; i < ec_slavecount; i++)
//		{
//			if (slave_arr[i].type == DI) {
//				slaveid = i;
//				itoa(slaveid, key_slave, 0);
//				key_slave = strncat(key_slave, "_", 0);		//right?
//				slave_di tmpDI = (SLAVE_DI*)slave_arr[i].ptrToSlave;
//				for (int j = 0; j < slave_arr[i].channelNum; j++) {
//					channelid = j;
//					itoa(channelid, key_channel, 0);
//					key_channel = strncat(key_slave, key_channel, 0);
//					value = tmpDI->values[j] ? "1" : "0";
//					cout << value << endl;							//debug
//
//					tmpMap->insert(pair<char*, char*>(key_channel, value));//right?
//					cout << key_channel << "::" << value;
//				}
//			}
//		}
//
//		addKeyValues(timeStampStr, *tmpMap);//添加到redis，现在还不能用，因为不能引用RedisHelper
//
//	}
//}

int slaveReadStart() {
	/*hthread = CreateThread(NULL,0,ThreadProc,NULL,NULL,NULL);
	StartThread(hthread);
	SuspendThread(hthread);*/
	_beginthread(readSlave, 0, NULL);
	return 0;
}

void slaveReadStop() {
	_endthread();
}


