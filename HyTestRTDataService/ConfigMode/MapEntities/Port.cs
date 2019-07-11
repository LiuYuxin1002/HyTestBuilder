using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyTestRTDataService.ConfigMode.MapEntities
{
    /// <summary>
    /// 硬件端口信息类
    /// </summary>
    [Serializable]
    public class Port
    {
        public string portId;   //数据类型+序号
        public int portType;
        public string name;
        public int deviceId;
        public int channelId;
        public int isBounded;

        public Port()
        {

        }

        public Port(string port)
        {
            string[] info = port.Split('-');
            deviceId = int.Parse(info[0]) + 2;
            channelId = int.Parse(info[1]) - 1;
        }
    }
}
