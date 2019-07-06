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

#define EC_TIMEOUT 500
#define EC_STACK_SIZE 128000
#define MAX_SLAVE 1000
#define MAX_CHANNEL 16
#define MAP_SIZE 4096

	extern ec_adaptert* adapter;
	extern char ifbuf[1024];		//�������ƻ���
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
		int		channelNum;		//1,2,4,8...Ŀǰ����ֻ֧��������
		void*	ptrToSlave;		//ָ���Ӧ�Ĵ�վ�˿�
		//char*	name;			//�ɶ�����
		int		name;
	};
	extern struct SLAVET_ARR slave_arr[MAX_SLAVE];

	typedef struct SLAVE_DI *slave_di;
	typedef struct SLAVE_DO *slave_do;
	typedef struct SLAVE_AI *slave_ai;
	typedef struct SLAVE_AO *slave_ao;

	struct SLAVE_DI {
		bitset<8> values;
		slave_di next = NULL;
		SLAVET_ARR* slaveinfo = NULL;//ָ��ȥ��ָ���վ�����б�
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

	//��������
	extern slave_di dis;
	extern slave_do dos;
	extern slave_ai ais;
	extern slave_ao aos;

	//��������

	int setIntegerValueImpl(int slaveId, int channel, int value);	//���ô�վ�˿�ֵ
	int setBoolValueImpl(int slaveId, int channel, boolean value);

#ifdef __cplusplus
}
#endif
