using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyTestRTDataService.Entities
{
    /// <summary>
    /// 硬件端口信息类
    /// </summary>
    [Serializable]
    public class Port
    {
        public string portId;   //normal id
        public int portType;    //port type in config
        public string name;     //port name in config
        public int deviceId;    //device id
        public int channelId;   //channel id in this device
        public int isBounded;   //tmp

        public Port() { }

        public Port(string port)    //附带端口修正
        {
            string[] info = port.Split('-');
            deviceId = int.Parse(info[0]) + 1;
            channelId = int.Parse(info[1]) - 1;
        }
    }
}
