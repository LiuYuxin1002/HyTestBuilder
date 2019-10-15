using System;
using System.Runtime.InteropServices;

namespace HyTestEtherCAT.localEntity
{
    //单个从站
    [StructLayout(LayoutKind.Sequential)]
    public struct EtherCATEntity
    {
        public int id;
        public int type;//Di,DO,AI,AO
        public int channelNum;
        public IntPtr ptrToSth;
        public string name;
    }
}
