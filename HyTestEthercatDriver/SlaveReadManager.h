#pragma once
//从站读取线程管理系统（SlaveReadManager)

#include "mycontext.h"
//准备读，检查连接状态，检查从站状态是不是OP; 检查redis服务器连接情况
int slavePrepareToRead();

//开始读取，控制读取线程while_true循环周期性读取
int slaveReadStart();
//暂停读取
int slaveReadSuspend();
//恢复读取
int slaveReadResume();
//停止读取
int slaveReadStop();

int getDigitalValueImpl(int deviceId, int channelId);

int getAnalogValueImpl(int deviceId, int channelId);