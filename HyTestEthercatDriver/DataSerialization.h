#pragma once

#include <map>

//************************************
//说明： 将这一时刻数据从slave数组拿出来变成map，方便存到redis
//************************************
std::map<char*, char*> dataToMap();