// 这是主 DLL 文件。
#include "mycontext.h"
#include "NicConfigSystem.h"
#include "SlaveConfigSystem.h"
#include "SlaveReadManager.h"
#include "SlaveWirteManager.h"

using namespace System::Reflection;
[assembly:AssemblyKeyFileAttribute("ec_driver.snk")];
[assembly:AssemblyDelaySignAttribute(false)];
#ifdef __cplusplus
extern "C"
{
#define TEXPORT extern "C" _declspec(dllexport)
#else
#define TEXPORT _declspec(dllexport)
#endif
	//ADAPTER
	TEXPORT int getAdapterNum() {
		return getAdapterNumImpl();
	}

	TEXPORT int getAdapterName(char* adapterName, char*adapterDesc, int id)
	{
		return getAdapterNameImpl(adapterDesc, adapterName, id);
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

	//SLAVE CONFIG
	TEXPORT int initSlaveConfig()
	{
		return initSlaveConfigInfo();
	}

	TEXPORT int getSlaveInfo(char* strName, SLAVET_ARR* slave, int id)
	{
		return getSlaveInfoImpl(slave, strName, id);
	}

	TEXPORT void stopRunning() {
		stopSlaveRunning();
	}

	//Write Value
	TEXPORT int setAnalogValue(int slaveId, int channel, int value)
	{
		return slaveWriteSingleAnalog(slaveId, channel, value);
	}


	TEXPORT int setDigitalValue(int slaveId, int channel, boolean value)
	{
		return slaveWriteSigleDigital(slaveId, channel, value);
	}

	//AUTO READ
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

	TEXPORT int readResume() {
		return slaveReadResume();
	}

	//MANUL READ
	TEXPORT int getAnalogValue(int deviceId, int channelId) {
		return getAnalogValueImpl(deviceId, channelId);
	}

	TEXPORT int getDigitalValue(int deviceId, int channelId) {
		return getDigitalValueImpl(deviceId, channelId);
	}

///env. setting
	//TODO: write func. in context
	TEXPORT int getRefrenceClock() {
		return 0;
	}
	//TODO: write func. too
	TEXPORT void setRefrenceClock(int useconds) {

	}
	//TODO: notify that the unit frequency is Hz.
	TEXPORT int getSamplingFrequency() {
		return 0;
	}
	//TODO: Hz.
	TEXPORT void setSamplingFrequency(int frequency) {

	}
	//TODO: return the bits of buffer
	TEXPORT int getBufferSize() {
		return 0;
	}
	//TODO: especially the redis buffer
	TEXPORT void setBufferSize() {

	}

#ifdef __cplusplus
}
#endif




