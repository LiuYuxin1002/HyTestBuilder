#include "HiredisHelper.h"
#include <stdlib.h>
#include <iostream>
#include <string>
using namespace std;
int main() {
	redisContext *c = getRedisClient();
	
	//string cmdstr = "hmset testhash";
	//char* strbuf = new char[1000];
	//for (int i = 0; i < 10; i++) {
	//	cmdstr += " key";
	//	cmdstr += itoa(i+1, strbuf, 10);
	//	cmdstr += " value";
	//	cmdstr += itoa(i+1, strbuf, 10);
	//}
	//printf("%s\n", cmdstr);
	//redisReply* reply2 = (redisReply*)redisCommand(c, cmdstr.c_str());

	map<char*, char*>* insertMap = new map<char *, char *>();
	//pair<char*, char*>* pair1 = new pair<char *, char *>("1","2");
	insertMap->insert(pair<char *, char *>("1", "2"));
	insertMap->insert(pair<char*, char*>("3","4"));
	addKeyValues("test", *insertMap);

	/*addKeyValue("1", "2rt5");
	char* reply = getKeyValue("1");
	printf("%s\n", reply);*/
	printf("¸ã¶¨");
	return 0;
}