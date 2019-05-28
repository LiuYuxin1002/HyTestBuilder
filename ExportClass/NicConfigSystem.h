#pragma once

#include "mycontext.h"

//获取从站数量实现函数
int getAdapterNumImpl();

//设置要选择的网卡，返回连接情况（原来是返回从站数量，功能杂糅）
char* setAdapterIdImpl(int nicId);

//获取第id个网卡的序列号和可读名称（原来是返回序列号，可读性不强）
int getAdapterNameImpl(char* adapterName, char* adapterDesc, int id);