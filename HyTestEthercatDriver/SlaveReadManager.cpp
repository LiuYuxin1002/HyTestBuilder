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

//If success return slavecount, else return 0.
int checkSlaveState() {
	if (ec_slavecount != 0) {
		//The work for state check here.

		cout << "check slave finish" << endl;
	}
	else {
		//Doing sth. for this state
		cout << "No Slave Found" << endl;
	}
	return ec_slavecount;
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

	if (slave_arr[deviceId].type == TYPE_DI) {
		SLAVE_DI* tmpSlave = (SLAVE_DI*)slave_arr[deviceId].ptrToSlave1;
		return tmpSlave->values[channelId];
	}
	else if (slave_arr[deviceId].type == TYPE_DO) {
		SLAVE_DO* tmpSlave = (SLAVE_DO*)slave_arr[deviceId].ptrToSlave1;
		return tmpSlave->values[channelId];
	}
	else {
		return -1;
	}
}

int getAnalogValueImpl(int deviceId, int channelId) {
	ec_send_processdata();
	int wkc = ec_receive_processdata(EC_TIMEOUTRET);

	if (slave_arr[deviceId].type == TYPE_AI) {		//AI
		SLAVE_AI* tmpSlave = (SLAVE_AI*)slave_arr[deviceId].ptrToSlave1;
		int16 value = tmpSlave->values[channelId];
		return value;
	}
	else if (slave_arr[deviceId].type == TYPE_AO) {	//AO
		SLAVE_AO* tmpSlave = (SLAVE_AO*)slave_arr[deviceId].ptrToSlave1;
		int16 value = tmpSlave->values[channelId];
		return value;
	}
	//TODO: need to be more flexible
	else if (slave_arr[deviceId].type == TYPE_SERVO) {//Servo
		SLAVE_SERVO_IN* si = (SLAVE_SERVO_IN*)slave_arr[deviceId].ptrToSlave1;
		SLAVE_SERVO_OUT* so = (SLAVE_SERVO_OUT*)slave_arr[deviceId].ptrToSlave2;
		switch (channelId)
		{
		case 1:
			return si->errorCode;
		case 2:
			return si->StatusWord;
		case 3:
			return si->OperationMode;
		case 4:
			return si->positionValue;	//Position Input	4
		case 5:
			return si->velocityValue;	//Velocity Input	5
		case 6:
			return si->torqueValue;
		case 7:
			return si->touchProbeStatus;
		case 8:
			return si->touchProbePoslPosValue;
		case 9:
			return si->digitalInput;
		case 11:
			return so->controlWord;
		case 12:
			return so->operationMode;
		case 13:
			return so->maxTorque;
		case 14:
			return so->targetPositon;	//Position Output	14
		case 15:
			return so->touchProbeFunction;
		case 16:
			return so->targetVelocity;	//Velocity Output	16
		default:
			break;
		}
		return INF;
	}
	
}