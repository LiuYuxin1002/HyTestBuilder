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
    /* 最主要的数据： 
     * device[]         底层直接获取到的
     * datatable        device[]转化来的，主要保存手段
     * treenode         device[]转化来的，主要展示手段
     * device number    device的个数
     */
    public class ConfigDevice :IConfigBase
    {
        private ConfigDeviceInfo deviceInfo;
        private IDeviceLoader loader;
        private TreeNode deviceTree;
        private string[] type = {"DI", "DO", "AI", "AO", };

        public ConfigDevice(ConfigDeviceInfo deviceInfo)
        {
            loader = EtherCAT.getEtherCAT(true);

            ReadSubConfig(deviceInfo);
        }

        //获取设备树，保存到本地字段
        public TreeNode GetDeviceTree(bool refresh)
        {
            if (refresh)
            {
                ScanSubConfig();
            }
            this.deviceTree = ArrToTreeNode();
            return this.deviceTree;
        }

        //从IODevice数组生成dataTable
        public DataTable getDeviceTable(bool refresh)
        {
            if (refresh || deviceInfo.deviceTable==null)
            {
                ScanSubConfig();
            }
            
            return deviceInfo.deviceTable;
        }

        private TreeNode ArrToTreeNode()
        {
            IOdevice[] deviceArr = this.deviceInfo.deviceArr;

            TreeNode tmpTree = new TreeNode("Devices");
            TreeNode[] typeNode = new TreeNode[4];
            for (int i = 0; i < 4; i++)
            {
                typeNode[i] = new TreeNode(type[i]);
            }

            tmpTree.Nodes.AddRange(typeNode);

            for (int i = 0; i < deviceInfo.deviceNum; i++)
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

        private DataTable ArrToDataTable()
        {
            //包括：id，设备名称，端口号，设备类型，所连接的变量名（可空）
            DataTable table = new DataTable();
            table.TableName = "DEVICE_TABLE";
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("设备名称", typeof(string));
            table.Columns.Add("设备编号", typeof(string));
            table.Columns.Add("端口号", typeof(string));
            table.Columns.Add("设备类型", typeof(string));
            table.Columns.Add("变量名", typeof(string));
            //完善表
            int id = 1;
            foreach (IOdevice device in deviceInfo.deviceArr)
            {
                if (device==null)
                {
                    break;
                }
                DataRow row = table.NewRow();
                row["ID"] = id++;
                row["设备名称"] = device.name;
                row["设备编号"] = device.id;
                row["设备类型"] = type[device.type-1];
                int channelnum = device.channelNum;
                for (int i = 0; i < channelnum; i++)
                {
                    row["端口号"] = i;
                    row["变量名"] = "N/A";
                }
            }
            return table;
        }

        #region 公共方法
        public void ReadSubConfig(object configInfo)
        {
            this.deviceInfo = (ConfigDeviceInfo)configInfo;
        }

        public object GetSubConfig()
        {
            return this.deviceInfo;
        }

        public void ScanSubConfig()
        {
            this.deviceInfo.deviceArr = loader.getDevice();
            RefreshDataWithArray();
        }

        public void SaveSubConfig(object var)
        {
            this.deviceTree = (TreeNode)var;
            RefreshDataWithTree();
        }

        //根据树刷新全局
        private void RefreshDataWithTree()
        {
            
        }
        //根据数组刷新全局
        private void RefreshDataWithArray()
        {
            this.deviceInfo.deviceNum = this.deviceInfo.deviceArr.Length;
            this.deviceInfo.deviceTable = ArrToDataTable();
        }
        #endregion

    }
}
