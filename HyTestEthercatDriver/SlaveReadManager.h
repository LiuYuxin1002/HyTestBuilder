#pragma once
//��վ��ȡ�̹߳���ϵͳ��SlaveReadManager)

#include "mycontext.h"
//׼�������������״̬������վ״̬�ǲ���OP; ���redis�������������
int slavePrepareToRead();

//��ʼ��ȡ�����ƶ�ȡ�߳�while_trueѭ�������Զ�ȡ
int slaveReadStart();
//��ͣ��ȡ
int slaveReadSuspend();
//�ָ���ȡ
int slaveReadResume();
//ֹͣ��ȡ
int slaveReadStop();

int getDigitalValueImpl(int deviceId, int channelId);

int getAnalogValueImpl(int deviceId, int channelId);