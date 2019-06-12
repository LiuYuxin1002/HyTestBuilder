#pragma once

#include "SlaveReadManager.h"
#include <stdio.h>
#include <process.h>
#include <windows.h>
#include <sys/timeb.h>
#include <iostream>
#include <string>
#include <map>
//#include "HiredisHelper.h"

using namespace std;

int slavePrepareToRead() {
	return 0;
}

int slaveReadStart() {
	_beginthread(readSlave, 0, NULL);
	return 0;
}

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
boolean runningState;

void readSlave(void*) {
	while (runningState)
	{
		long long timestamp = getSystemTime();
		char* timeStampStr = ltos(timestamp);
		
		//addKeyValues(timeStampStr,)
	}
}
//struct SLAVET_ARR slave_arr[MAX_SLAVE];
map<char*, char*> getDataMap() {
	for (int i=0; i<ec_slavecount; i++)
	{
		int type = slave_arr[i].type;
		
		slave_arr[i].name;
	}
	map<char*, char*>* dataMap = new map<char *, char *>();
	return *dataMap;
}