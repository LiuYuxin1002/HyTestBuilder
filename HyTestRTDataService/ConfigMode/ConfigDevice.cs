using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyTestIEInterface;
using HyTestIEEntity;
using System.Windows.Forms;
using System.Data;
using HyTestEtherCAT;

namespace HyTestRTDataService.ConfigMode
{
    class ConfigDevice
    {
        private IDeviceLoader loader;
        private IList<IOdevice> deviceList;
        private IList<IOdevice> ignoreList;

        private IOdevice[] deviceArray;
        private TreeNode deviceTree;
        private DataTable deviceTable;

        public int deviceNum;
        private string[] type = { "DI", "DO", "AI", "AO", };

        public ConfigDevice()
        {
            loader = EtherCAT.getEtherCAT(true);
        }

        public IList<IOdevice> getDeviceList()
        {
            if (this.deviceList == null)
            {
                if (deviceArray == null)
                {
                    deviceArray = loader.getDevice();
                }
                deviceList = deviceArray.ToList();
                deviceNum = deviceList.Count;
            }
            return this.deviceList;
        }

        public IOdevice[] getDeviceArr()
        {
            if (this.deviceArray == null)
            {
                deviceArray = loader.getDevice();
            }
            return this.deviceArray;
        }

        public TreeNode getDeviceTree()
        {
            if (this.deviceArray == null)
            {
                getDeviceArr();
            }

            TreeNode rootNode = new TreeNode("Devices");
            TreeNode[] typeNode = new TreeNode[4];
            for (int i = 0; i < 4; i++)
            {
                typeNode[i] = new TreeNode(type[i]);
            }

            rootNode.Nodes.AddRange(typeNode);

            for (int i = 0; i < this.deviceNum; i++)
            {
                IOdevice device = this.deviceArray[i];
                TreeNode slave = new TreeNode(device.name);
                int slaveType = device.type;
                typeNode[slaveType - 1].Nodes.Add(slave);
                for (int channel = 0; channel < device.channelNum; channel++)
                {
                    TreeNode ch = new TreeNode("channel" + (channel + 1));
                    slave.Nodes.Add(ch);
                }
            }

            return rootNode;
        }

        public DataTable getDeviceTable()
        {
            //包括：id，设备名称，端口号，设备类型，所连接的变量名（可空）
            return default(DataTable);
        }
    }
}
