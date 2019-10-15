#pragma once
//从站读取线程管理系统（SlaveReadManager)

#include "mycontext.h"
//准备读，检查连接状态，检查从站状态是不是OP; 检查redis服务器连接情况
operationResult* slavePrepareToRead(ProcessCallBack processCallBack);

//开始读取，控制读取线程while_true循环周期性读取
operationResult* slaveReadStart();
//停止读取
operationResult* slaveReadStop();
//read digital
int getDigitalValueImpl(int deviceId, int channelId);
//read analog
int getAnalogValueImpl(int deviceId, int channelId);