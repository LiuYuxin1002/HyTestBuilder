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

	extern ec_adaptert* adapter;
	extern char ifbuf[1024];		//网卡名称缓存
	typedef struct adaptert *ptrToAdapter;
	struct adaptert {
		string name;
		vector<string> nicDesc;
		vector<string> nicName;
		int adapterNum;
	};
	extern struct adaptert myadapter;

	
	struct SLAVET_ARR
	{
		int		id;				//ID
		int		type;			//0:empty, 1:di, 2:do, 3:ai, 4:ao
		int		channelNum;		//1,2,4,8...目前打算只支持这四种
		void*	ptrToSlave1;		//指向对应的从站端口
		void*	ptrToSlave2;
		char*	name;			//可读名称
	};
	extern struct SLAVET_ARR slave_arr[MAX_SLAVE];

	typedef struct SLAVE_DI *slave_di;
	typedef struct SLAVE_DO *slave_do;
	typedef struct SLAVE_AI *slave_ai;
	typedef struct SLAVE_AO *slave_ao;

	struct SLAVE_DI {
		bitset<8> values;
		slave_di next = NULL;
		SLAVET_ARR* slaveinfo = NULL;//指回去，指向从站整体列表
	};

	struct SLAVE_DO {
		bitset<8> values;
		slave_do next = NULL;
		SLAVET_ARR* slaveinfo = NULL;
	};
	
	struct SLAVE_AI {
		int16 values[MAX_CHANNEL];
		slave_ai next = NULL;
		SLAVET_ARR* slaveinfo = NULL;
	};

	struct SLAVE_AO {
		int16 values[MAX_CHANNEL];
		slave_ao next = NULL;
		SLAVET_ARR* slaveinfo = NULL;
	};

	//松下伺服驱动器映射声明部分
	typedef struct SLAVE_SERVO_OUT *pservo_output;
	typedef struct SLAVE_SERVO_IN  *pservo_input;

	struct SLAVE_SERVO_OUT {
		int16 controlWord;			//common
		int8  operationMode;		//common
		int16 maxTorque;			//speed
		int32 targetPositon;		//position
		int16 touchProbeFunction;	//common
		int32 targetVelocity;		//speed
		void* slaveInfo;
	};

	struct SLAVE_SERVO_IN {
		int16 errorCode;			//common
		int16 StatusWord;			//common
		int8  OperationMode;		//common
		int32 positionValue;		//position
		int32 velocityValue;		//velocity
		int16 torqueValue;			//torque
		int32 touchProbeStatus;		//common
		int32 touchProbePoslPosValue;
		int32 digitalInput;
		void* slaveInfo;
	};

	struct SLAVE_COUPLER {
		void* slaveInfo;
	};

	//定义链表
	extern slave_di dis;
	extern slave_do dos;
	extern slave_ai ais;
	extern slave_ao aos;

	//伺服驱动器链表
	extern pservo_input sis;
	extern pservo_output sos;

#ifdef __cplusplus
}
#endif
