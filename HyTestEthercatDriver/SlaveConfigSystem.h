#pragma once
//��վ��ʼ����������ϵͳ��SlaveConfigSystem��

#include "mycontext.h"

//��ʼ��SOEM�д�վ������Ϣ�����ش�վ������ԭ������������ѡ����ȶ���ȡ�������
int initSlaveConfigInfo();
//��ʼ������slave_arr������Ϣ������վ����ΪDI��DO��AI��AO���࣬�����context�У�ԭ������getSlaveInfoImpl��������
void initLocalSlaveInfo();
//����id��ȡ��վ��Ϣ��name��������.
int getSlaveInfoImpl(SLAVET_ARR *slave, char* slaveName, int id);
