#pragma once

#include "DataSerialization.h"
#include "mycontext.h"

using namespace std;

//�������л�
void serialized(map<char*, char*>* map, void* tmpslave, int type) {
	char* intstrbuf = new char[128];
	//����ת��
#if type==1
#define SLAVETYPE slave_di
#else //type==3
#define SLAVETYPE slave_ai
#endif

	while (tmpslave != NULL) {
		int chnum = ((SLAVETYPE)tmpslave)->slaveinfo->channelNum;
		int slavid = ((SLAVETYPE)tmpslave)->slaveinfo->id;

		for (int i = 0; i < chnum; i++) {
			char* infostr = "";
			char* valuestr = "";
			strcat(infostr, itoa(slavid, intstrbuf, 10));				//slaveid
			strcat(infostr, "_");										//���ӷ�
			strcat(infostr, itoa(chnum, intstrbuf, 10));				//channelid
			//strcat(valuestr, itoa(((SLAVETYPE)tmpslave)->values[i], intstrbuf, 10));	//boolֵ��0��1 TODO:::AI
			map->insert(pair<char*, char*>(infostr, valuestr));	//����
		}
		tmpslave = ((SLAVETYPE)tmpslave)->next;
	}
}

//*********************************************************************
// Method:    ��վ���ݵ�Map
// FullName:  slave data to map
// Steps:	  Ŀ���Ǵ�վDI��AI����Ϣ���������ݰ��� slaveId_channelId value����ʽ����map��
// Returns:   std::map<char*, char*>
// Qualifier:
//*********************************************************************
map<char*, char*> dataToMap() {
	map<char*, char*>* dataInfo = new map<char *, char *>();
	
	char* intstrbuf = new char[128];
	//start DI
	slave_di tmpdi = dis;
	serialized(dataInfo, tmpdi, 1);
	
	//while (tmpdi != NULL) {
	//	int chnum = tmpdi->slaveinfo->channelNum;
	//	int slavid = tmpdi->slaveinfo->id;
	//	
	//	for (int i = 0; i < chnum; i++) {
	//		char* infostr = "";
	//		char* valuestr = "";
	//		strcat(infostr, itoa(slavid, intstrbuf, 10));				//slaveid
	//		strcat(infostr, "_");										//���ӷ�
	//		strcat(infostr, itoa(chnum, intstrbuf, 10));				//channelid
	//		strcat(valuestr, itoa(tmpdi->values[i], intstrbuf, 10));	//boolֵ��0��1
	//		dataInfo->insert(pair<char*, char*>(infostr, valuestr));	//����
	//	}
	//	tmpdi = tmpdi->next;
	//}
	//start AI
	slave_ai tmpai = ais;
	serialized(dataInfo, tmpai, 3);
	//while (tmpai != NULL) {
	//	int chnum = tmpai->slaveinfo->channelNum;
	//	int slavid = tmpai->slaveinfo->id;
	//
	//	for (int i = 0; i < chnum; i++) {
	//		char* infostr = "";
	//		char* valuestr = "";
	//		strcat(infostr, itoa(slavid, intstrbuf, 10));				//slaveid
	//		strcat(infostr, "_");										//���ӷ�
	//		strcat(infostr, itoa(chnum, intstrbuf, 10));				//channelid
	//		strcat(valuestr, itoa(tmpai->values[i], intstrbuf, 10));	//boolֵ��0��1
	//		dataInfo->insert(pair<char*, char*>(infostr, valuestr));	//����
	//	}
	//	tmpai = tmpai->next;
	//}
	return *dataInfo;
}