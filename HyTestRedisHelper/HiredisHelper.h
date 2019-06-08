#pragma once
#include "hiredis.h"
#include <iostream>
#include <string>
#include <map>

#define REDIS_PORT 6379
#define REDIS_IP "127.0.0.1"

extern redisContext* client;

//��������
redisContext* getRedisClient();//��ȡ�ͻ���
void		  closeRedisClient();//�رտͻ���

void	addKeyValue(char* key, char* value);					//string_add:	set key value
char*	getKeyValue(char* key);									//string_get:	get key
void	addKeyValue(char* key, char* field, char* value);		//hash_add_one:	hset key field value
char*	getKeyValue(char* key, char* field);					//hash_get_one:	hmget key field
void	addKeyValues(char* key, std::map<char*, char*> value);	//hash_add_all:	hmset
std::map<char*, char*> getKeyValues(char* key);					//hash_get_all:	hgetall
void	removeKey(char* key);
void	removeField(char* key, char* field);

//hiredis.h���
/*
��ȡ���ӣ�redisClient()
�������
	redisReply reply = redisCommand(context, "cmd");
	redisReply reply = redisCommand(context, "set foo %s", value);
	redisReply reply = redisCommand(context, "set foo %b", value, (size_t) valuelen);
redisReply��ʹ�ã�
	type:		��������
	integer:	������redis_reply_integer�����������
	len:		�ַ�������
	str:		�������ַ�������errʱ�����������
	elements:	����Ԫ�ظ���
	element:	����������������գ���һ��redisReply**
*/
//redis��������
