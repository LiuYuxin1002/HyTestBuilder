#pragma once
//��վ��ȡ�̹߳���ϵͳ��SlaveReadManager)

#include "mycontext.h"
//׼�������������״̬������վ״̬�ǲ���OP; ���redis�������������
int slavePrepareToRead();

//��ʼ��ȡ�����ƶ�ȡ�߳�while_trueѭ�������Զ�ȡ
int slaveReadStart();
//��ͣ��ȡ
int slaveReadSuspend();
//ֹͣ��ȡ
int slaveReadStop();