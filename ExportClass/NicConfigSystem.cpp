#pragma once

#include "NicConfigSystem.h"
#include "mycontext.h"

char ifbuf[1024];//����������Ϣ���棬������context

int getAdapterNumImpl() {
	myadapter.adapterNum = 0;
	adapter = ec_find_adapters();					//��ȡ������������Ϣ�����浽adapter������
	while (adapter != NULL) {
		myadapter.nicDesc.push_back(adapter->desc);
		myadapter.nicName.push_back(adapter->name);
		adapter = adapter->next;
		myadapter.adapterNum++;
	}
	return myadapter.adapterNum;
}

char* setAdapterIdImpl(int nicId) {
	string tmpstr = myadapter.nicName[nicId].data();
	tmpstr = "\\Device\\NPF_" + tmpstr;				//Ҫ��Ҫ��ǰ׺���д���֤~~
	strcpy(ifbuf, tmpstr.c_str());					//ע����ifbuf����ѡ�Ƿ��Ӧ
	return ifbuf;
}

int getAdapterNameImpl(char* adapterName, char* adapterDesc, int id) {
	if (myadapter.adapterNum > id) {
		strcpy(adapterName, (char*)myadapter.nicName[id].data());
		strcpy(adapterDesc, (char*)myadapter.nicDesc[id].data());
		printf("Name:%s, Desc:%s\n", adapterName, adapterDesc);			//debug
	}
	else {
		printf("�������id�������飡");
		adapterName = adapterDesc = NULL;
		return -1;			//id����Ĵ������
	}
	return 0;
}