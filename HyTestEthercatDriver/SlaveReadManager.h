#pragma once
//��վ��ȡ�̹߳���ϵͳ��SlaveReadManager)

#include "mycontext.h"
//׼�������������״̬������վ״̬�ǲ���OP; ���redis�������������
operationResult* slavePrepareToRead(ProcessCallback processCallBack);

//��ʼ��ȡ�����ƶ�ȡ�߳�while_trueѭ�������Զ�ȡ
operationResult* slaveReadStart();
//ֹͣ��ȡ
operationResult* slaveReadStop();
//read digital
int getDigitalValueImpl(int deviceId, int channelId);
//read analog
int getAnalogValueImpl(int deviceId, int channelId);
//��Ƶ������ʼ
void HighFreqReadImpl(int deviceId, int channelId, int period, HighFreqCallback callback);
//��Ƶ����ֹͣ
void HighFreqReadStopImpl(int deviceId, int channelId);