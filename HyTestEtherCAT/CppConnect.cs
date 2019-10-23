using System.Runtime.InteropServices;
using System.Text;
using HyTestEtherCAT.localEntity;

namespace HyTestEtherCAT
{
    public class CppConnect
    {
        //获取/设置计算机网卡信息，接收到Context结构体
        [DllImport("HyTestEthercatDriver.dll")]
        public static extern int getAdapterNum();
        [DllImport("HyTestEthercatDriver.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int getAdapterName(StringBuilder namestr, StringBuilder descstr, int id);
        //设置所选网卡，如果失败返回-1，成功返回100
        [DllImport("HyTestEthercatDriver.dll", EntryPoint = "setAdapterId", CallingConvention = CallingConvention.Cdecl)]
        public static extern int setAdapterId(int nicId);
        [DllImport("HyTestEthercatDriver.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetAdapter(StringBuilder name);
        //自动配置从站，成功更新结构体数组
        [DllImport("HyTestEthercatDriver.dll")]
        public static extern int initSlaveConfig();
        [DllImport("HyTestEthercatDriver.dll", CharSet = CharSet.Ansi,  CallingConvention = CallingConvention.Cdecl)]
        public static extern int getSlaveInfo(StringBuilder slaveName, ref EtherCATEntity slaveInfo, int id);
        //Set slave running state to init. If you need to running again, please call initSlaveConfig() and getSlaveInfo() again.
        [DllImport("HyTestEthercatDriver.dll")]
        public static extern void stopRunning();
        /*Write*/
        [DllImport("HyTestEthercatDriver.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int setAnalogValue(int slaveId, int channel, int value);
        [DllImport("HyTestEthercatDriver.dll", EntryPoint = "setDigitalValue", CallingConvention = CallingConvention.Cdecl)]
        public static extern int setDigitalValue(int slaveId, int channel, byte value);
        /*Read*/
        [DllImport("HyTestEthercatDriver.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int doWork([MarshalAs(UnmanagedType.FunctionPtr)]ProcessCallback callbackPoint);
        [DllImport("HyTestEthercatDriver.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int getAnalogValue(int slaveId, int channelId);
        [DllImport("HyTestEthercatDriver.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int getDigitalValue(int slaveId, int channelId);
        [DllImport("HyTestEthercatDriver.dll")]
        public static extern int prepareToRead();
        [DllImport("HyTestEthercatDriver.dll")]
        public static extern int readStart();
        [DllImport("HyTestEthercatDriver.dll")]
        public static extern int readStop();
    }

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void ProcessCallback(int slave, int channel, int value);
}
