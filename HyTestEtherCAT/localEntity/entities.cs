﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HyTestEtherCAT.localEntity
{
    //单个从站
    [StructLayout(LayoutKind.Sequential)]
    public struct SlaveInfo
    {
        public int id;
        public int type;//Di,DO,AI,AO
        public int channelNum;
        public IntPtr ptrToSth;
        //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3)]
        //public string name;
        public int name;
    }
    class entities
    {
    }
}
