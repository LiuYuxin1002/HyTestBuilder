#pragma once
//��վ��ȡ�̹߳���ϵͳ��SlaveReadManager)

#include "mycontext.h"
//׼�������������״̬������վ״̬�ǲ���OP; ���redis�������������
int slavePrepareToRead();
//��ʼ��ȡ�����ƶ�ȡ�߳�while_trueѭ�������Զ�ȡ
int slaveReadStart();
//ֹͣ��ȡ
int slaveReadStop();
//�����ȡ�������������������0��1�������ģ���������ؾ�����ֵ
int slaveReadSingleVariable(int slaveid, int channelid);
//˽�з�������ȡ�̣߳�����Ӧ�ƶ���cpp�ļ�����*
void readSlave(void*);
//˽�з���������վ״̬OP�����ؼ����
int checkSlaveState();
//˽�з��������redis����״��������redis״̬������δ�������޷����ӡ���������������ڴ�������
int checkRedisState();