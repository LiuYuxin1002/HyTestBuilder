#pragma once
//从站读取线程管理系统（SlaveReadManager)

#include "mycontext.h"
//准备读，检查连接状态，检查从站状态是不是OP; 检查redis服务器连接情况
int slavePrepareToRead();
//开始读取，控制读取线程while_true循环周期性读取
int slaveReadStart();
//停止读取
int slaveReadStop();
//单点读取，如果是数字量，返回0和1，如果是模拟量，返回具体数值
int slaveReadSingleVariable(int slaveid, int channelid);
//私有方法，读取线程，后面应移动到cpp文件当中*
void readSlave(void*);
//私有方法，检查从站状态OP，返回检查结果
int checkSlaveState();
//私有方法，检查redis连接状况，返回redis状态：服务未开启、无法连接、最大请求已满、内存已满等
int checkRedisState();