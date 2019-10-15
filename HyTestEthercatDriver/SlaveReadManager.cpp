#pragma once

#include "SlaveReadManager.h"
#include <stdio.h>
#include <windows.h>
#include <sys/timeb.h>
#include <iostream>
#include <string>
#include <map>
#include "mmsystem.h"
#include "hiredisHelper.h"
#pragma comment(lib, "winmm.lib")
using namespace std;

char strbuf[64] = "";
map<char*, int> oldIOmap;
int DEFAULT_SLEEP_TIME;
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

/*Boolean CAS*/
int booleanCompareAndSwap(int target, int real, int* location) {
	if (target != real) {
		target = real;
		*location = target;
	}
	return *location;
}

/*抖动率设置为1%忽略，即+-32767/100=327.67*/
int ignoreThreshold = 327;

/*Integer CAS*/
int analogCompareAndSwap(int target, int real, int*location) {
	int sub = target - real;
	if (ignoreThreshold > sub > -1 * ignoreThreshold) {	//进入了忽略范围: -327~327
		return target;
	}
	else {
		target = real;
		*location = target;
		return *location;
	}
}

//TODO: If success return slavecount, else return 0.
operationResult* checkSlaveState() {
	operationResult* res;
	if (ec_slavecount != 0) {
		//The work for state check here.
		
		cout << "check slave finish" << endl;
		res = new operationResult(0, NULL);
	}
	else {
		//Doing sth. for this state
		cout << "No Slave Found" << endl;
		res = new operationResult(1, "No Slave Found");
	}
	return res;
}

//将两个数字连接起来
char* contact(int key1, int key2, char* buffer) {
	char* key = "";
	strcat(key, itoa(key1, buffer, 10));
	strcat(key, "_");
	strcat(key, itoa(key2, buffer, 10));
	return key;
}

//TODO: 
operationResult* checkRedisState() {
	redisContext* context = getRedisClient();
	/*client maybe empty*/
	if (client == NULL || context->err) {
		cout << "redis client get failed" << endl;
		cout << context->errstr << endl;
		return new operationResult(context->err, context->errstr);
	}

	/*iomap is empty, we should fill it with init value*/
	if (oldIOmap.empty())	
	{
		char* buffer = new char[256];
		for (int slave = 0; slave < ec_slavecount; slave++)
		{
			SLAVET_ARR tmp = slave_arr[slave];

			for (int channel = 0; channel < tmp.channelNum; channel++) {
				/*get your key*/
				char* key = contact(slave, channel, buffer);
				/*get you value*/
				int value;
				if(tmp.type==TYPE_DI)			value = getDigitalValueImpl(slave, channel);
				else if (tmp.type == TYPE_AI)	value = getAnalogValueImpl(slave, channel);
				/*insert*/
				//oldIOmap.insert(pair<char*, int>(key, value));
				oldIOmap[key] = value;
			}
		}
		delete(buffer);
	}
	cout << "redis client get success" << endl;
	return new operationResult(0, NULL);
}

MMRESULT TimerId;
/*redis 时钟触发事件*/
void CALLBACK readAndSaveRedis(UINT uID, UINT uMsg, DWORD dwUser, DWORD dw1, DWORD dw2) {
	if (client == NULL) return;
	char* buff = new char[128];
	char* time = ltos(getSystemTime());	//get time as main-key
	for (int slave=0; slave<ec_slavecount; slave++)
	{
		SLAVET_ARR tmp = slave_arr[slave];
		if (tmp.type==TYPE_DI)
		{
			for (int channel = 0; channel < tmp.channelNum; channel++) {
				int value = getDigitalValueImpl(slave, channel);
				char* key = contact(slave, channel, buff);
				/*CAS*/
				int ans = booleanCompareAndSwap(oldIOmap[key], value, &oldIOmap[key]);
				/*add to redis if cas*/
				if(ans==value) addKeyValue(time, key, itoa(value, buff, 10));
			}
		}
		if (tmp.type == TYPE_AI)
		{
			for (int channel = 0; channel<tmp.channelNum; channel++)
			{
				int value = getAnalogValueImpl(slave, channel);
				char* key = contact(slave, channel, buff);
				/*CAS*/
				int ans = analogCompareAndSwap(oldIOmap[key], value, &oldIOmap[key]);
				/*add to redis if cas*/
				if(ans==value) addKeyValue(time, key, itoa(value, buff, 10));
			}
		}
	}
	delete(buff);
}

operationResult* slavePrepareToRead() {
	checkRedisState();
	operationResult* res = checkSlaveState();
	if (!res) {
		cout << "prepare finished" << endl;
		readState = true;
	}
	return res;
}

operationResult* slaveReadStart() {
	if (TimerId == NULL) {
		TimerId = timeSetEvent(DEFAULT_SLEEP_TIME/1000, 0, readAndSaveRedis, NULL, TIME_PERIODIC);
	}
	return new operationResult(0, NULL);
}

operationResult* slaveReadStop() {
	if (TimerId != NULL) {
		timeKillEvent(TimerId);	//kill event
	}
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
		int16 value = tmpSlave->values[(channelId+1)*2-1];		//TODO: 仅针对EL3004
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
		//inputs
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
		//outputs
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