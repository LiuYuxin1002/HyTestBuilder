#pragma once

#include <stdio.h>
#include <vector>
#include <iostream>
#include <string>
#include <bitset>
#include "ethercat.h"

using namespace std;

#ifdef __cplusplus
extern "C" {
#endif 

/************************************************************************/
/*Ԥ������                                                             */
/************************************************************************/
#define INF				0x3fffffff
#define EC_TIMEOUT		500
#define EC_STACK_SIZE	128000
#define MAX_SLAVE		1000
#define MAX_CHANNEL		16
#define MAP_SIZE		4096

#define TYPE_COUPLER	10
#define TYPE_SERVO		20
#define TYPE_DI			1
#define TYPE_DO			2
#define TYPE_AI			3
#define TYPE_AO			4

/************************************************************************/
/*ȫ�ֱ���                                                               */
/************************************************************************/
extern HANDLE		g_hMutex;				//thread read-write lock.
extern int			DEFAULT_SLEEP_TIME;		//default time of every wait.
extern int			DEFINE_WAIT_TIME;
extern int			DEFINE_REFRENSE_CLOCK;	//the time between every writes
extern int			READING_FREQUENCY;		//Hz of reading.
extern int			REDIS_BUFFER_SIZE;
extern char			IOmap[MAP_SIZE];

typedef void(__stdcall * ProcessCallBack)(int, int, int);//��ȡ�̺߳����ص�
extern ProcessCallBack readCallBack;

typedef struct operationResult {
	int16 error;
	char* error_msg;
	operationResult(int16 _error, char* _error_msg) : 
	error(_error), error_msg(_error_msg) {}
} operationResult;

/************************************************************************/
/*����������                                                             */
/************************************************************************/
extern ec_adaptert* adapter;
extern char			ifbuf[1024];		//�������ƻ���

typedef struct adaptert *ptrToAdapter;
struct adaptert {
	string			name;
	vector<string>	nicDesc;
	vector<string>	nicName;
	int				adapterNum;
};
extern struct adaptert myadapter;

/************************************************************************/
/*��վ�ṹ������                                                          */
/************************************************************************/
struct SLAVET_ARR
{
	int		id;				//ID
	int		type;			//0:empty, 1:di, 2:do, 3:ai, 4:ao
	int		channelNum;		//1,2,4,8,16...
	void*	ptrToSlave1;	//ָ���Ӧ�Ĵ�վ�˿�
	void*	ptrToSlave2;	//ָ�����
	char*	name;			//�ɶ�����
};
extern struct SLAVET_ARR slave_arr[MAX_SLAVE];

typedef struct SLAVE_DI *slave_di;
typedef struct SLAVE_DO *slave_do;
typedef struct SLAVE_AI *slave_ai;
typedef struct SLAVE_AO *slave_ao;

PACKED_BEGIN
struct SLAVE_AI_CHANNEL {
	boolean		Underrange;
	boolean		Overrange;
	bitset<2>	Limit1;
	bitset<2>	Limit2;
	boolean		Error;
	bitset<1>	Empty1;
	bitset<1>	Empty2;
	boolean		PDO_State;
	boolean		PDO_Toggle;
	int16		value;
};
PACKED_END

PACKED_BEGIN
struct SLAVE_DI {
	bitset<16>		values;
	slave_di		next = NULL;
	SLAVET_ARR*		slaveinfo = NULL;//ָ��ȥ��ָ���վ�����б�
};
PACKED_END

PACKED_BEGIN
struct SLAVE_DO {
	bitset<16>		values;
	slave_do		next = NULL;
	SLAVET_ARR*		slaveinfo = NULL;
};
PACKED_END
	
PACKED_BEGIN
struct SLAVE_AI {
	//SLAVE_AI_CHANNEL values[MAX_CHANNEL];
	int16			values[MAX_CHANNEL*11];	//TODO: cause there is always sth useless in map, 10 if for EL3154 only.
	slave_ai		next = NULL;
	SLAVET_ARR*		slaveinfo = NULL;
};
PACKED_END

PACKED_BEGIN
struct SLAVE_AO {
	int16			values[MAX_CHANNEL];
	slave_ao		next = NULL;
	SLAVET_ARR*		slaveinfo = NULL;
};
PACKED_END

//�����ŷ�������ӳ����������
typedef struct SLAVE_SERVO_OUT *pservo_output;
typedef struct SLAVE_SERVO_IN  *pservo_input;

//TODO: If you change these words, remember to modify Read/WriteManager
PACKED_BEGIN
	struct SLAVE_SERVO_IN {
	int16 errorCode;
	int16 StatusWord;
	int8  OperationMode;
	int32 positionValue;		//position	4
	int32 velocityValue;		//velocity	5
	int16 torqueValue;			//torque	6
	int32 touchProbeStatus;
	int32 touchProbePoslPosValue;
	int32 digitalInput;
	void* slaveInfo;
};
PACKED_END
	
PACKED_BEGIN
struct SLAVE_SERVO_OUT {
	int16 controlWord;			
	int8  operationMode;		
	int16 maxTorque;			//Max Torque	13
	int32 targetPositon;		//position		14
	int16 touchProbeFunction;	
	int32 targetVelocity;		//speed			16	
	void* slaveInfo;
};
PACKED_END

PACKED_BEGIN
struct SLAVE_COUPLER {
	void* slaveInfo;
};
PACKED_END

//��������
extern slave_di dis;
extern slave_do dos;
extern slave_ai ais;
extern slave_ao aos;

//�ŷ�����������
extern pservo_input		sis;
extern pservo_output	sos;

#ifdef __cplusplus
}
#endif
