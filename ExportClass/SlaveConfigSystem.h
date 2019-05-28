#pragma once
//从站初始化配置任务系统（SlaveConfigSystem）

#include "mycontext.h"

//初始化SOEM中从站配置信息，返回从站数量（原来集成在网卡选择后，先独立取出，解耦）
int initSlaveConfigInfo();
//初始化本地slave_arr数组信息，将从站分组为DI，DO，AI，AO四类，存放在context中（原来叫做getSlaveInfoImpl，不形象）
void initLocalSlaveInfo();
//根据id获取从站信息
int getSlaveInfoImpl(SLAVET_ARR *slave, int id);
