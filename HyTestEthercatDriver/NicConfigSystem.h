#pragma once

#include "mycontext.h"

//��ʼ��������Ϣ������ȡ��վ����ʵ�ֺ���
int getAdapterNumImpl();

//����Ҫѡ�������������������������
char* setAdapterIdImpl(int nicId);

//��ȡ��id�����������кźͿɶ����ƣ����������ɹ�
int getAdapterNameImpl(char* adapterName, char* adapterDesc, int id);