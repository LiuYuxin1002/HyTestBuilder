﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyTestRTDataService.Interfaces
{
    public interface IWriter
    {
        /// <summary>
        /// 单点写模拟量，对于伺服驱动器，需要参照配置表来使用
        /// </summary>
        /// <param name="deviceId">设备编号</param>
        /// <param name="channel">对于普通IO，为端口号；对于伺服驱动器，为配置表对应参数</param>
        /// <param name="value">写入值</param>
        /// <returns></returns>
        int WriteAnalog(int deviceId, int channel, int value);
        /// <summary>
        /// 单点写数字量
        /// </summary>
        /// <returns></returns>
        int WriteBoolean(int deviceId, int channel, byte value);
    }
}
