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
/*预处理常量                                                             */
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
#define TYPE_DSENSOR	5

/************************************************************************/
/*全局变量                                                               */
/************************************************************************/
extern HANDLE		g_hMutex;				//thread read-write lock.
extern int			DEFAULT_SLEEP_TIME;		//default time of every wait.
extern int			DEFINE_WAIT_TIME;
extern int			DEFINE_REFRENSE_CLOCK;	//the time between every writes
extern int			READING_FREQUENCY;		//Hz of reading.
extern int			REDIS_BUFFER_SIZE;
extern char			IOmap[MAP_SIZE];

typedef void(__stdcall * ProcessCallback)(int, int, int);//读取线程函数回调
typedef void(__stdcall * HighFreqCallback)(int*);//高频采样回调

typedef struct operationResult {
	int16 error;
	char* error_msg;
	operationResult(int16 _error, char* _error_msg) : 
	error(_error), error_msg(_error_msg) {}
} operationResult;

/************************************************************************/
/*网络适配器                                                             */
/************************************************************************/
extern ec_adaptert* adapter;
extern char			ifbuf[1024];		//网卡名称缓存

typedef struct adaptert *ptrToAdapter;
struct adaptert {
	string			name;
	vector<string>	nicDesc;
	vector<string>	nicName;
	int				adapterNum;
};
extern struct adaptert myadapter;

/************************************************************************/
/*从站结构体申明                                                          */
/************************************************************************/
struct SLAVET_ARR
{
	int		id;				//ID
	int		type;			//0:empty, 1:di, 2:do, 3:ai, 4:ao 5:displacement sensor, 10:coupler, 20:servo
	int		channelNum;		//1,2,4,8,16...
	void*	ptrToSlave1;	//指向对应的从站端口
	void*	ptrToSlave2;	//指向输出
	char*	name;			//可读名称
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
	SLAVET_ARR*		slaveinfo = NULL;//指回去，指向从站整体列表
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

//松下伺服驱动器映射声明部分
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

//耦合器结构体声明
PACKED_BEGIN
struct SLAVE_COUPLER {
	void* slaveInfo;
};
PACKED_END

//位移传感器声明
typedef struct SLAVE_DSERVOR_IN		*dservor_input;
typedef struct SLAVE_DSERVOR_OUT	*dservor_output;
/*********SM3 inputs*********/
//addr b   index	: sub bitl data_type     name
//[0x0006.0] 0x6000 : 0x01 0x01 BOOLEAN      Latch C valid
//[0x0006.1] 0x6000 : 0x02 0x01 BOOLEAN      Latch extern valid
//[0x0006.2] 0x6000 : 0x03 0x01 BOOLEAN      Set counter done
//[0x0006.3] 0x0000 : 0x00 0x04
//[0x0006.7] 0x6000 : 0x08 0x01 BOOLEAN      Extrapolation stall
//[0x0007.0] 0x6000 : 0x09 0x01 BOOLEAN      Status of input A
//[0x0007.1] 0x6000 : 0x0A 0x01 BOOLEAN      Status of input B
//[0x0007.2] 0x6000 : 0x0B 0x01 BOOLEAN      Status of input C
//[0x0007.3] 0x0000 : 0x00 0x01
//[0x0007.4] 0x6000 : 0x0D 0x01 BOOLEAN      Status of extern latch
//[0x0007.5] 0x6000 : 0x0E 0x01 BOOLEAN      Sync error
//[0x0007.6] 0x0000 : 0x00 0x01
//[0x0007.7] 0x6000 : 0x10 0x01 BOOLEAN      TxPDO Toggle
//[0x0008.0] 0x6000 : 0x11 0x20 UNSIGNED32   Counter value
//[0x000C.0] 0x6000 : 0x12 0x20 UNSIGNED32   Latch value
//[0x0010.0] 0x6000 : 0x14 0x20 UNSIGNED32   Period value
PACKED_BEGIN
struct SLAVE_DSERVOR_IN {
	bitset<16> none;
	uint32 counterValue;	//计数器值
	uint32 latchValue;
	uint32 periodValue;
	void* slaveInfo;
};
PACKED_END
/*************SM2 outputs**************/
//addr b     index  : sub bitl		data_type    name
//[0x0000.0] 0x7000 : 0x01 0x01		BOOLEAN      Enable latch C
//[0x0000.1] 0x7000 : 0x02 0x01		BOOLEAN      Enable latch extern on positive edge
//[0x0000.2] 0x7000 : 0x03 0x01		BOOLEAN      Set counter
//[0x0000.3] 0x7000 : 0x04 0x01		BOOLEAN      Enable latch extern on negative edge
//[0x0000.4] 0x0000 : 0x00 0x04
//[0x0001.0] 0x0000 : 0x00 0x08
//[0x0002.0] 0x7000 : 0x11 0x20 UNSIGNED32   Set counter value
PACKED_BEGIN
struct SLAVE_DSERVOR_OUT {
	bitset<1> enable_latch_c;
	bitset<1> enable_latch_extern_on_positive_edge;
	bitset<1> set_counter;
	bitset<1> enable_latch_extern_on_negative_edge;
	bitset<12> nonesense;
	uint32 setCounterValue;
	void* slaveInfo;
};
PACKED_BEGIN

//定义链表
extern slave_di dis;
extern slave_do dos;
extern slave_ai ais;
extern slave_ao aos;

//伺服驱动器链表
extern pservo_input		sis;
extern pservo_output	sos;

//倍福位移传感器
extern dservor_input dserverInput;
extern dservor_output dserverOutput;

#ifdef __cplusplus
}
#endif
