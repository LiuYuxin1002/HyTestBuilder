// 这是主 DLL 文件。
#include "mycontext.h"
#include "NicConfigSystem.h"
#include "SlaveConfigSystem.h"
#include "SlaveReadManager.h"
#include "SlaveWirteManager.h"


//SoemImplClass* transfer = new SoemImplClass();
#ifdef __cplusplus
extern "C"
{
#define TEXPORT extern "C" _declspec(dllexport)
#else
#define TEXPORT _declspec(dllexport)
#endif


	TEXPORT int getAdapterNum() {
		return getAdapterNumImpl();
	}

	TEXPORT int getAdapterName(char* adapterName, char*adapterDesc, int id)
	{
		return getAdapterNameImpl(adapterName, adapterDesc, id);
	}

	TEXPORT int setAdapterId(int nicId)
	{
		setAdapterIdImpl(nicId);
		if (ifbuf == NULL) {	//赋值失败
			return -1;
		}
		else {					//赋值成功
			return 100;
		}
	}


	TEXPORT int initSlaveConfig()
	{
		return initSlaveConfigInfo();
	}

	TEXPORT void getSlaveInfo(SLAVET_ARR* slave, int id)
	{
		getSlaveInfoImpl(slave, id);
	}


	TEXPORT int setIntegerValue(int slaveId, int channel, int value)
	{
		return setIntegerValueImpl(slaveId, channel, value);
	}


	TEXPORT int setBoolValue(int slaveId, int channel, boolean value)
	{
		return slaveWriteSigleDigital(slaveId, channel, value);
	}

	TEXPORT int prepareToRead() {
		return slavePrepareToRead();
	}

	TEXPORT int readStart() {
		return slaveReadStart();
	}

	TEXPORT int readSuspend() {
		return slaveReadSuspend();
	}

	TEXPORT int readStop() {
		return slaveReadStop();
	}

	TEXPORT int getAnalogValue(int deviceId, int channelId) {
		
	}

	TEXPORT int getDigitalValue(int deviceId, int channelId) {

	}
	

#ifdef __cplusplus
}
#endif




