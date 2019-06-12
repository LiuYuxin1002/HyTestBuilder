#pragma once

#include "NicConfigSystem.h"
#include "mycontext.h"

char ifbuf[1024];//申明网卡信息缓存，定义在context

int getAdapterNumImpl() {
	myadapter.adapterNum = 0;
	adapter = ec_find_adapters();					//获取网卡适配器信息，保存到adapter数组中
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
	tmpstr = "\\Device\\NPF_" + tmpstr;				//要不要加前缀，有待考证~~
	strcpy(ifbuf, tmpstr.c_str());					//注意检查ifbuf和所选是否对应
	return ifbuf;
}

int getAdapterNameImpl(char* adapterName, char* adapterDesc, int id) {
	if (myadapter.adapterNum > id) {
		strcpy(adapterName, (char*)myadapter.nicName[id].data());
		strcpy(adapterDesc, (char*)myadapter.nicDesc[id].data());
		printf("Name:%s, Desc:%s\n", adapterName, adapterDesc);			//debug
	}
	else {
		printf("您输入的id有误，请检查！");
		adapterName = adapterDesc = NULL;
		return -1;			//id有误的错误代码
	}
	return 0;
}