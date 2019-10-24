using System;
using System.Runtime.InteropServices;

namespace HyTestRTDataService.EtherCAT
{
    //单个从站
    [StructLayout(LayoutKind.Sequential)]
    public struct HtEcEntity
    {
        public int id;
        public int type;//Di,DO,AI,AO
        public int channelNum;
        public IntPtr ptrToSth;
        public string name;
    }
}
