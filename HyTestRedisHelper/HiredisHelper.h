#pragma once
#include "hiredis.h"
#include <iostream>
#include <string>
#include <map>

#define REDIS_PORT 6379
#define REDIS_IP "127.0.0.1"

extern redisContext* client;

//方法声明
redisContext* getRedisClient();//获取客户端
void		  closeRedisClient();//关闭客户端

void	addKeyValue(char* key, char* value);					//string_add:	set key value
char*	getKeyValue(char* key);									//string_get:	get key
void	addKeyValue(char* key, char* field, char* value);		//hash_add_one:	hset key field value
char*	getKeyValue(char* key, char* field);					//hash_get_one:	hmget key field
void	addKeyValues(char* key, std::map<char*, char*> value);	//hash_add_all:	hmset
std::map<char*, char*> getKeyValues(char* key);					//hash_get_all:	hgetall
void	removeKey(char* key);
void	removeField(char* key, char* field);

//hiredis.h简介
/*
获取连接：redisClient()
发送命令：
	redisReply reply = redisCommand(context, "cmd");
	redisReply reply = redisCommand(context, "set foo %s", value);
	redisReply reply = redisCommand(context, "set foo %b", value, (size_t) valuelen);
redisReply的使用：
	type:		返回类型
	integer:	当返回redis_reply_integer，用这个接受
	len:		字符串长度
	str:		当返回字符串或者err时候用这个接收
	elements:	返回元素个数
	element:	返回数组用这个接收，是一个redisReply**
*/
//redis常用命令
