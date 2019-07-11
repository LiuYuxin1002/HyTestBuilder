#pragma once

#include "mycontext.h"

//初始化网卡信息，并获取从站数量实现函数
int getAdapterNumImpl();

//设置要选择的网卡，返回连接网卡名称
char* setAdapterIdImpl(int nicId);

//获取第id个网卡的序列号和可读名称，返回正数成功
int getAdapterNameImpl(char* adapterName, char* adapterDesc, int id);