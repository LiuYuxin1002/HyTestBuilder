// 这是主 DLL 文件。
#include "mycontext.h"
#include "NicConfigSystem.h"
#include "SlaveConfigSystem.h"
#include "SlaveReadManager.h"
#include "SlaveWirteManager.h"

//using namespace System::Reflection;
//[assembly:AssemblyKeyFileAttribute("ec_driver.snk")];
//[assembly:AssemblyDelaySignAttribute(false)];
#ifdef __cplusplus
extern "C"
{
#define TEXPORT extern "C" _declspec(dllexport)
#else
#define TEXPORT _declspec(dllexport)
#endif
	/*ADAPTER OPERATION*/

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

	TEXPORT void setAdapter(char* adapter) {
		setAdapterImpl(adapter);
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
	TEXPORT void doWork(ProcessCallback processCallBack) {
		operationResult* res = slavePrepareToRead(processCallBack);
	}

	TEXPORT void prepareToRead(ProcessCallback processCallBack) {
		operationResult* res = slavePrepareToRead(processCallBack);
	}

	TEXPORT int readStart() {
		return slaveReadStart()->error;
	}

	TEXPORT int readStop() {
		return slaveReadStop()->error;
	}

	//MANUL READ
	TEXPORT int getAnalogValue(int deviceId, int channelId) {
		return getAnalogValueImpl(deviceId, channelId);
	}

	TEXPORT int getDigitalValue(int deviceId, int channelId) {
		return getDigitalValueImpl(deviceId, channelId);
	}

	//High Frequency Read
	TEXPORT void HighFreqRead(int deviceId, int channelId, HighFreqCallback callback) {
		HighFreqReadImpl(deviceId, channelId, callback);
	}

	TEXPORT void HighFreqReadStop(int deviceId, int channelId) {
		HighFreqReadStop(deviceId, channelId);
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




