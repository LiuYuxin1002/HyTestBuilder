#pragma once

#include "mycontext.h"

//��ȡ��վ����ʵ�ֺ���
int getAdapterNumImpl();

//����Ҫѡ����������������������ԭ���Ƿ��ش�վ�������������ۣ�
char* setAdapterIdImpl(int nicId);

//��ȡ��id�����������кźͿɶ����ƣ�ԭ���Ƿ������кţ��ɶ��Բ�ǿ��
int getAdapterNameImpl(char* adapterName, char* adapterDesc, int id);