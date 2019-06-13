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
using HyTestRTDataService.ConfigMode.MapEntities;

namespace HyTestRTDataService.ConfigMode
{
    class ConfigDevice
    {
        private IDeviceLoader loader;
        private IList<IOdevice> deviceList;
        private IList<IOdevice> ignoreList;

        public IOdevice[] deviceArray;
        private TreeNode deviceTree;
        public DataTable deviceTable;

        public int deviceNum;
        private string[] type = { "DI", "DO", "AI", "AO", };

        public ConfigDevice(ConfigDeviceInfo deviceInfo)
        {
            loader = EtherCAT.getEtherCAT(true);
        }

        public IList<IOdevice> GetDeviceList()
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

        public IOdevice[] ScanDeviceArr()
        {
            if (this.deviceArray == null)
            {
                deviceArray = loader.getDevice();
            }
            return this.deviceArray;
        }

        //获取设备树，保存到本地字段
        public TreeNode GetDeviceTree()
        {
            ScanDeviceArr();

            this.deviceTree = ArrToTreeNode(this.deviceArray);

            return this.deviceTree;
        }

        public void SetDeviceTree(TreeNode deviceTree)
        {
            this.deviceTree = deviceTree;
        }

        /// <summary>
        /// 将给定的IOdevice数组转为TreeNode
        /// </summary>
        /// <param name="deviceArray"></param>
        public TreeNode ArrToTreeNode(IOdevice[] deviceArr)
        {
            TreeNode tmpTree = new TreeNode("Devices");
            TreeNode[] typeNode = new TreeNode[4];
            for (int i = 0; i < 4; i++)
            {
                typeNode[i] = new TreeNode(type[i]);
            }

            tmpTree.Nodes.AddRange(typeNode);

            for (int i = 0; i < this.deviceNum; i++)
            {
                IOdevice device = deviceArr[i];
                TreeNode slave = new TreeNode(device.name);
                int slaveType = device.type;
                typeNode[slaveType - 1].Nodes.Add(slave);
                for (int channel = 0; channel < device.channelNum; channel++)
                {
                    TreeNode ch = new TreeNode("channel" + (channel + 1));
                    slave.Nodes.Add(ch);
                }
            }
            return tmpTree;
        }

        //从IODevice数组生成dataTable
        public DataTable getDeviceTable()
        {
            //包括：id，设备名称，端口号，设备类型，所连接的变量名（可空）
            this.deviceTable = new DataTable();
            deviceTable.Columns.Add("ID", typeof(int));
            deviceTable.Columns.Add("设备名称", typeof(string));
            deviceTable.Columns.Add("设备编号", typeof(string));
            deviceTable.Columns.Add("端口号", typeof(string));
            deviceTable.Columns.Add("设备类型", typeof(string));
            deviceTable.Columns.Add("变量名", typeof(string));
            //完善表
            int id = 1;
            foreach (IOdevice device in deviceArray)
            {
                DataRow row = deviceTable.NewRow();
                row["ID"] = id++;
                row["设备名称"] = device.name;
                row["设备编号"] = device.id;
                row["设备类型"] = type[device.type];
                int channelnum = device.channelNum;
                for(int i=0; i<channelnum; i++)
                {
                    row["端口号"] = i;
                    row["变量名"] = "N/A";
                }
            }
            return this.deviceTable;
        }
    }
}
