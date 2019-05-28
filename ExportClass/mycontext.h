#pragma once

#include <stdio.h>
#include <vector>
#include <iostream>
#include <string>
#include "ethercat.h"

using namespace std;

#ifdef __cplusplus
extern "C" {
#endif // __cplusplus

#define EC_TIMEOUT 500
#define EC_STACK_SIZE 128000
#define MAX_SLAVE 1000
#define MAX_CHANNEL 16
#define MAP_SIZE 4096

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
		char*	name;			//可读名称
		int		channelNum;		//1,2,4,8...目前打算只支持这四种
		void*	ptrToSlave;		//指向对应的从站端口
	};
	extern struct SLAVET_ARR slave_arr[MAX_SLAVE];

	typedef struct SLAVE_DI *slave_di;
	typedef struct SLAVE_DO *slave_do;
	typedef struct SLAVE_AI *slave_ai;
	typedef struct SLAVE_AO *slave_ao;

	struct SLAVE_DI {
		bool values[MAX_CHANNEL];
		slave_di next = NULL;
		SLAVET_ARR* slaveinfo = NULL;//指回去，指向从站整体列表
	};

	struct SLAVE_DO {
		bool values[MAX_CHANNEL];
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

	//定义链表
	extern slave_di dis;
	extern slave_do dos;
	extern slave_ai ais;
	extern slave_ao aos;

	//方法声明

	//int getAdapterNumImpl();					//获取计算机网卡信息
	//int getContextInfoImpl(char* adapterName);
	//int setAdapterIdImpl(int nicId);				//设置网卡,返回从站数目
	//int initConfigImpl();						//自动配置从站
	//void getSlaveInfoImpl();					//返回配置后的从站信息
	int setIntegerValueImpl(int slaveId, int channel, int value);	//设置从站端口值
	int setBoolValueImpl(int slaveId, int channel, boolean value);
	//int initSlaveInfoImpl(char* ifname);		//初始化从站

#ifdef __cplusplus
}
#endif // __cplusplus
