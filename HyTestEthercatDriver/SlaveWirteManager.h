#pragma once
//从站写入线程管理系统（SlaveWriteManager)

#include "mycontext.h"

//写入单个数字量
int slaveWriteSigleDigital(int slaveid, int channelid, bool value);
//写入批量数字量，先尝试全循环式写法，效率可能差一点
int slaveWriteBatchDigital(int *slaveid, int *channelid, bool *value);

int slaveWriteSingleAnalog(int slaveId, int channelId, int value);

int slaveWriteBatchAnalog(int *slaveId, int *channelId, int *value);
//还应考虑，
//1.周期性输出场合，如何保证周期稳定性？经试验，需要循环写入IOmap
//2.高频闭环控制如何做？这个可以作为研究点？**测试方案呢？