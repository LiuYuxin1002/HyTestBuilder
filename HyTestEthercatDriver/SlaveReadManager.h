#pragma once
//��վ��ȡ�̹߳���ϵͳ��SlaveReadManager)

#include "mycontext.h"
//׼�������������״̬������վ״̬�ǲ���OP; ���redis�������������
operationResult* slavePrepareToRead(ProcessCallBack processCallBack);

//��ʼ��ȡ�����ƶ�ȡ�߳�while_trueѭ�������Զ�ȡ
operationResult* slaveReadStart();
//ֹͣ��ȡ
operationResult* slaveReadStop();
//read digital
int getDigitalValueImpl(int deviceId, int channelId);
//read analog
int getAnalogValueImpl(int deviceId, int channelId);