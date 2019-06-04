using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyTestIEInterface;
using HyTestIEEntity;
using HyTestEtherCAT.localEntity;

namespace HyTestEtherCAT
{
    public class EtherCAT : ConnectionContext, IConnection, IAdapterLoader, IDeviceLoader, IReadWrite
    {
        #region region_属性

        #endregion

        #region region_常量
        private const int SAFECODE = 0;
        #endregion

        #region region_成员变量

        private static EtherCAT ethercat;
        public static EtherCAT getEtherCAT(bool hasAdapter)//单例
        {
            if(ethercat == null)
            {
                ethercat = new EtherCAT(hasAdapter);
                return ethercat;
            }
            else
            {
                return ethercat;
            }
        }

        #endregion

        #region region_构造函数
        private EtherCAT(bool hasAdapter)
        {
            haveAdapter = hasAdapter;
        }
        #endregion

        #region region_事件

        public event EventHandler datachanged;

        #endregion

        #region region_连接管理
        public int close()
        {
            throw new NotImplementedException();
        }

        public int connect()
        {
            throw new NotImplementedException();
        }

        public int disconnect()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region region_硬件

        public IOdevice[] getDevice()
        {
            int slaveNum = CppConnect.getSlaveNum();
            deviceNum = slaveNum;
            for (int i = 0; i < slaveNum; i++)
            {
                SlaveInfo tmpSlave = new SlaveInfo();
                int err = CppConnect.getSlaveInfo(ref tmpSlave, i);
                if (err == SAFECODE)
                {
                    //devices[i] = tmpSlave;
                }
                else//有错误
                {
                    throw new Exception();
                }
            }
            return devices;
        }

        #endregion

        #region region_读写
        public int ReadAnalog(List<int> deviceList, List<int[]> channelList, ref List<int[]> values)
        {
            throw new NotImplementedException();
        }

        public int ReadAnalog(int deviceId, int channel, ref int value)
        {
            throw new NotImplementedException();
        }

        public int ReadDigital(List<int> deviceList, List<int[]> channelList, ref List<bool[]> values)
        {
            throw new NotImplementedException();
        }

        public int ReadDigital(int deviceId, int channel, ref bool value)
        {
            throw new NotImplementedException();
        }

        public int WriteAnalog(List<int> deviceList, List<int[]> channelList, List<int[]> values)
        {
            throw new NotImplementedException();
        }

        public int WriteAnalog(int deviceId, int channel, int value)
        {
            return CppConnect.setIntergerValue(deviceId, channel, value);
        }
        
        public int WriteDigital(List<int> deviceList, List<int[]> channelList, List<bool[]> values)
        {
            throw new NotImplementedException();
        }

        public int WriteDigital(int deviceId, int channel, bool value)
        {
            return CppConnect.setDigitalValue(deviceId, channel, value);
        }
        #endregion

        #region region_网卡
        public Adapter[] getAdapter()
        {
            adapterNum = CppConnect.getAdapterNum();
            adapters = new Adapter[adapterNum];

            StringBuilder tmpAdapterName = new StringBuilder();
            StringBuilder tmpAdapterDesc = new StringBuilder();
            tmpAdapterName.Capacity = 128;
            tmpAdapterDesc.Capacity = 128;
            for (int i = 0; i < adapterNum; i++)
            {
                int err = CppConnect.getAdapterName(tmpAdapterName, tmpAdapterDesc, i);
                if (err != 0)//有错误
                {
                    Console.WriteLine("您输入的id有误，请检查！"); //一般来讲是这个错误
                }
                adapters[i] = new Adapter();
                adapters[i].name = tmpAdapterName.ToString();
                adapters[i].desc = tmpAdapterDesc.ToString();
                tmpAdapterName.Clear();
                tmpAdapterDesc.Clear();
            }
            return adapters;
        }

        public ErrorCode setAdapter(int id)
        {
            int errmsg = CppConnect.setAdapterId(id);   //返回-1表示赋值失败
            if (errmsg < 0)
            {
                return ErrorCode.ADAPTER_SELECT_FAIL;
            }
            else                                        //返回0表示没有错误
            {
                deviceNum = errmsg;
                return ErrorCode.NO_ERROR;
            }
        }

        #endregion

        #region other method

        #endregion
    }
}
