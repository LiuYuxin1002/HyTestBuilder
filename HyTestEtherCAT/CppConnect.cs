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
        //自动配置从站，成功更新结构体数组
        [DllImport("HyTestEthercatDriver.dll")]
        public static extern int initSlaveConfig();
        [DllImport("HyTestEthercatDriver.dll", CharSet = CharSet.Ansi,  CallingConvention = CallingConvention.Cdecl)]
        public static extern int getSlaveInfo(StringBuilder slaveName, ref EtherCATEntity slaveInfo, int id);
        //Set slave running state to init. If you need to running again, please call initSlaveConfig() and getSlaveInfo() again.
        [DllImport("HyTestEthercatDriver.dll")]
        public static extern void stopRunning();
        //设置从站某端口信息
        [DllImport("HyTestEthercatDriver.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int setAnalogValue(int slaveId, int channel, int value);
        [DllImport("HyTestEthercatDriver.dll", EntryPoint = "setDigitalValue", CallingConvention = CallingConvention.Cdecl)]
        public static extern int setDigitalValue(int slaveId, int channel, byte value);

        /// <summary>
        /// 获取模拟量
        /// </summary>
        /// <returns>返回真实值</returns>
        [DllImport("HyTestEthercatDriver.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int getAnalogValue(int slaveId, int channelId);
        /// <summary>
        /// 获取数字量，返回int需要自行处理
        /// </summary>
        /// <returns>返回1=true，返回0=false</returns>
        [DllImport("HyTestEthercatDriver.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int getDigitalValue(int slaveId, int channelId);
        [DllImport("HyTestEthercatDriver.dll")]
        public static extern int prepareToRead();
        [DllImport("HyTestEthercatDriver.dll")]
        public static extern int readStart();
        [DllImport("HyTestEthercatDriver.dll")]
        public static extern int readSuspend();
        [DllImport("HyTestEthercatDriver.dll")]
        public static extern int readStop();
        [DllImport("HyTestEthercatDriver.dll")]
        public static extern int readResume();
    }
}
