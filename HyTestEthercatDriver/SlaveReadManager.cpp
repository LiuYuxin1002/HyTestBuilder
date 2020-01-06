#pragma once

#include "SlaveReadManager.h"
#include <stdio.h>
#include <windows.h>
#include <sys/timeb.h>
#include <iostream>
#include <string>
#include <map>
#include <AclAPI.h>
#pragma comment(lib, "winmm.lib")
#pragma comment(lib, "advapi32.lib")
using namespace std;

char strbuf[64] = "";
map<string, int> oldIOmap;
int DEFAULT_SLEEP_TIME;
bool readState = true;
HANDLE rthread;
ProcessCallback readCallBack;

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
int booleanCompareAndSwap(int target, int real) {
	if (target != real) {
		target = real;
	}
	return target;
}

/*抖动率设置为1%忽略，即+-32767/100=327.67*/
int ignoreThreshold = 327;

/*Integer CAS*/
int analogCompareAndSwap(int target, int real) {
	int sub = target - real;
	if (-ignoreThreshold < sub && sub < ignoreThreshold) {	//进入了忽略范围: -327~327
		return target;
	}
	else {
		target = real;
		return target;
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
string contact(int key1, int key2) {
	char itc[10];
	sprintf(itc, "%d_%d", key1, key2);
	return itc;
}

//TODO: 
operationResult* prepareCallBack() {
	/*iomap is empty, we should fill it with init value*/
	if (readCallBack && oldIOmap.empty()) {
		for (int slave = 0; slave < ec_slavecount; slave++)
		{
			SLAVET_ARR tmp = slave_arr[slave];

			for (int channel = 0; channel < tmp.channelNum; channel++) {
				/*get your key*/
				string key = contact(slave, channel);
				/*get you value*/
				int value = 0;
				if (tmp.type == TYPE_DI || tmp.type == TYPE_DO)		value = getDigitalValueImpl(slave, channel);
				else if (tmp.type == TYPE_AI || tmp.type == TYPE_AO)	value = getAnalogValueImpl(slave, channel);
				/*insert*/
				oldIOmap[key] = value;
			}
		}
		return new operationResult(0, "buffer map initializing success");
	}
	else {
		return new operationResult(1, "Error in checkRedisState, callBack method maybe empty.");
	}
}
#pragma region CTimer
	class CTimer
	{
		public:
			CTimer(void);
			~CTimer(void);

			int time_in();
			double time_out();

		private:
			LARGE_INTEGER litmp;
			LONGLONG qt1, qt2,qt_diff;
			double dft, dff, dfm;
	};

	CTimer::CTimer(void)
	{
	}


	CTimer::~CTimer(void)
	{
	}

	int CTimer::time_in()
	{
		QueryPerformanceCounter(&litmp);//获得初始值
		qt1 = litmp.QuadPart;
		
		return 1;
	}

	double CTimer::time_out()
	{
		QueryPerformanceCounter(&litmp);//获得终止值
		qt_diff = litmp.QuadPart - qt1;
		qt_diff *= 1000000;

		QueryPerformanceFrequency(&litmp);//获得时钟频率
		dft = qt_diff / litmp.QuadPart;//获得对应的时间值
		
		return dft;
	}
#pragma endregion

MMRESULT TimerId;
CTimer timer;
double* arr;
int ptr = 0;

int flag = 0;
/*redis timer_tick event*/
FILETIME ftKernelTimeStart, ftKernelTimeEnd;
FILETIME ftUserTimeStart, ftUserTimeEnd;
FILETIME ftDummy1, ftDummy2;
void CALLBACK readAndCallBack(UINT uID, UINT uMsg, DWORD dwUser, DWORD dw1, DWORD dw2) {
	for (int slave=0; slave<ec_slavecount; slave++)
	{
		SLAVET_ARR tmp = slave_arr[slave];
		if (tmp.type==TYPE_DI || tmp.type == TYPE_DO)
		{
			for (int channel = 0; channel < tmp.channelNum; channel++) {
				int value = getDigitalValueImpl(slave, channel);
				string key = contact(slave, channel);
				/*CAS*/
				int before = oldIOmap[key];
				int ans = booleanCompareAndSwap(oldIOmap[key], value);
				/*add to redis if cas*/
				if (ans != before)
				{
					oldIOmap[key] = value;
					readCallBack(slave, channel, value);
				}
			}
		}
		if (tmp.type == TYPE_AI || tmp.type == TYPE_AO)
		{
			for (int channel = 0; channel<tmp.channelNum; channel++)
			{
				int value = getAnalogValueImpl(slave, channel);
				string key = contact(slave, channel);
				/*CAS*/
				int before = oldIOmap[key];
				int ans = analogCompareAndSwap(oldIOmap[key], value);
				/*add to redis if cas*/
				if (ans != before)
				{
					oldIOmap[key] = value;
					readCallBack(slave, channel, value);
				}
			}
		}
	}
}

operationResult* slavePrepareToRead(ProcessCallback processCallBack) {
	readCallBack = processCallBack;
	operationResult* res = checkSlaveState();
	readState = true;
	return res;
}

operationResult* slaveReadStart() {
	if (TimerId == NULL) {
		prepareCallBack();
		TimerId = timeSetEvent(100, 0, readAndCallBack, NULL, TIME_PERIODIC);
	}
	return new operationResult(0, NULL);
}

operationResult* slaveReadStop() {
	if (TimerId != NULL) {
		timeKillEvent(TimerId);	//kill event
	}
	return new operationResult(0, NULL);
}

int getDigitalValueImpl(int deviceId, int channelId) {
	//sendAndReveive();

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
	}
	return INF;
	
}

void sendAndReveive() {
	ec_send_processdata();
	int wkc = ec_receive_processdata(EC_TIMEOUTRET);
}


///
///高频采样
///TODO:多组数据高频采样？？？

int* result = new int[1000];
int ptr = 0;
int crtDevice, crtChannel;
HighFreqCallback localCallback;
void CALLBACK record(UINT uID, UINT uMsg, DWORD dwUser, DWORD dw1, DWORD dw2) {
	//达到1000个数字返回
	if (ptr > 1000) {
		ptr = 0;
		localCallback(result);
	}
	//重新初始化数组
	if (ptr == 0) {
		result = new int[1000];
	}
	//读取并放入数组
	result[ptr] = getAnalogValueImpl(crtDevice, crtChannel);	//暂时只读取模拟量
}

MMRESULT HighTimer;
void HighFreqReadImpl(int deviceId, int channelId, int period, HighFreqCallback callback) {
	crtDevice = deviceId;
	crtChannel = channelId;
	localCallback = callback;
	if (HighTimer == NULL) {
		HighTimer = timeSetEvent(period, 0, record, NULL, TIME_PERIODIC);
	}
}

void HighFreqReadStopImpl(int deviceId, int channelId) {
	if (HighTimer != NULL) {
		localCallback(result);
		timeKillEvent(HighTimer);//停止计时器
	}
}