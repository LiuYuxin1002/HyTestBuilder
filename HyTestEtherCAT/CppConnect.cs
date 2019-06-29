using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using HyTestIEEntity;
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
        //设置所选网卡，如果失败返回错误信息，成功返回当前连接的从站信息
        [DllImport("HyTestEthercatDriver.dll", EntryPoint = "setAdapterId", CallingConvention = CallingConvention.Cdecl)]
        public static extern int setAdapterId(int nicId);
        //自动配置从站，成功更新结构体数组
        [DllImport("HyTestEthercatDriver.dll")]
        public static extern int initConfig();
        [DllImport("HyTestEthercatDriver.dll")]
        public static extern int initSlaveConfig();
        [DllImport("HyTestEthercatDriver.dll", EntryPoint = "getSlaveInfo", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern int getSlaveInfo(ref SlaveInfo slaveInfo, int id);
        //设置从站某端口信息
        [DllImport("HyTestEthercatDriver.dll")]
        public static extern int setIntergerValue(int slaveId, int channel, int value);
        [DllImport("HyTestEthercatDriver.dll")]
        public static extern int setDigitalValue(int slaveId, int channel, bool value);

        [DllImport("HyTestEthercatDriver.dll")]
        public static extern int prepareToRead();
        [DllImport("HyTestEthercatDriver.dll")]
        public static extern int readStart();
        [DllImport("HyTestEthercatDriver.dll")]
        public static extern int readSuspend();
        [DllImport("HyTestEthercatDriver.dll")]
        public static extern int readStop();
    }
}
