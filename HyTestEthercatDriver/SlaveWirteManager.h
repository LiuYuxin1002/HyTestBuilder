#pragma once
//��վд���̹߳���ϵͳ��SlaveWriteManager)

#include "mycontext.h"

//д�뵥��������
int slaveWriteSigleDigital(int slaveid, int channelid, bool value);
//д���������������ȳ���ȫѭ��ʽд����Ч�ʿ��ܲ�һ��
int slaveWriteBatchDigital(int *slaveid, int *channelid, bool *value);

int slaveWriteSingleAnalog(int slaveId, int channelId, int value);

int slaveWriteBatchAnalog(int *slaveId, int *channelId, int *value);
//��Ӧ���ǣ�
//1.������������ϣ���α�֤�����ȶ��ԣ������飬��Ҫѭ��д��IOmap
//2.��Ƶ�ջ���������������������Ϊ�о��㣿**���Է����أ�