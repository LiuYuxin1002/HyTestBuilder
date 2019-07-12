#pragma once

#include "mycontext.h"
#include "ethercat.h"

ec_adaptert* adapter;

struct adaptert myadapter;

const int SLAVE_TYPE_ID = 2;		//type所在位
const int SLAVE_CHANNEL_ID = 5;		//channel所在位

									//局部变量
OSAL_THREAD_HANDLE thread1;
//char IOmap[MAP_SIZE];
boolean needlf;
boolean inOP;
volatile int wkc;
uint8 currentgroup = 0;