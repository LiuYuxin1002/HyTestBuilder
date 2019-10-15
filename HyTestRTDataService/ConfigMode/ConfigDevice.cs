using HyTestIEInterface;
using System.Windows.Forms;
using System.Data;
using HyTestRTDataService.ConfigMode.MapEntities;
using HyTestIEInterface.Entity;
using System.Collections.Generic;

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

        public ConfigDevice(ConfigDeviceInfo deviceInfo)
        {
            loader = ConfigProtocol.GetDeviceLoader();

            ReadSubConfig(deviceInfo);
        }

        //获取设备树，保存到本地字段
        public TreeNode GetDeviceTree(bool refresh)
        {
            if (refresh)
            {
                ScanSubConfig();
            }
            this.deviceTree = ArrToTreeNode(this.deviceInfo.deviceList);
            return this.deviceTree;
        }

        //从IODevice数组生成dataTable
        public DataTable getDeviceTable(bool refresh)
        {
            if (refresh || deviceInfo.deviceList==null)
            {
                ScanSubConfig();
            }
            
            return ArrToDataTable();
        }

        //将info中的list<list<>>变为TreeNode
        private TreeNode ArrToTreeNode(List<List<IOdevice>> deviceList)
        {
            TreeNode deviceTree = new TreeNode("DEVICE");

            int deviceNum = 0, channelNum = 0;

            foreach (List<IOdevice> deviceGroup in deviceList)
            {
                //For the first, we need to select coupler out.
                IOdevice coupler = deviceGroup[0];
                TreeNode couplerNode = new TreeNode(coupler.name);
                channelNum += coupler.channelNum;   //If servo, neet to count channel.
                deviceNum += deviceGroup.Count;//add to deviceNum.
                for(int i=1; i<deviceGroup.Count; i++)
                {
                    
                    //For others, we need to add them to the coupler Node one-by-one.
                    IOdevice device = deviceGroup[i];
                    TreeNode deviceNode = new TreeNode(device.name);
                    for (int channel = 0; channel < device.channelNum; channel++)
                    {
                        //For each channel, we add them auto-ly.
                        TreeNode ch = new TreeNode("Channel" + (channel + 1));
                        deviceNode.Nodes.Add(ch);
                    }
                    channelNum += device.channelNum;
                    couplerNode.Nodes.Add(deviceNode);
                }
                deviceTree.Nodes.Add(couplerNode);
            }
            /* TODO: Refresh deviceNum & channelNum. but if you haven't click scan_butten, 
               this will not work. So we should find solution to get these two params before 
               running.*/
            this.deviceInfo.deviceNum = deviceNum;
            this.deviceInfo.allChannelCount = channelNum;

            return deviceTree;
        }

        //将info中的list<list<>>变为datatable
        private DataTable ArrToDataTable()
        {
            //包括：id，设备名称，端口号，设备类型，所连接的变量名（可空）
            DataTable table = new DataTable();
            table.TableName = "DEVICE_TABLE";
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("设备名称", typeof(string));
            table.Columns.Add("设备编号", typeof(string));
            table.Columns.Add("设备类型", typeof(string));
            table.Columns.Add("端口号", typeof(string));
            table.Columns.Add("变量名", typeof(string));
            //TODO: 考虑添加groupID
            //完善表
            int id = 1;
            foreach (List<IOdevice> devices in deviceInfo.deviceList)
            {
                foreach(IOdevice device in devices)
                {
                    if (device == null)
                    {
                        break;
                    }
                    DataRow row = table.NewRow();
                    row["ID"] = id++;
                    row["设备名称"] = device.name;
                    row["设备编号"] = device.id;
                    row["设备类型"] = device.type;
                    int channelnum = device.channelNum;
                    for (int i = 0; i < channelnum; i++)
                    {
                        DataRow tmpRow = row;
                        row["端口号"] = i;
                        row["变量名"] = "N/A";
                        table.Rows.Add(tmpRow);
                    }
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
            this.deviceInfo.deviceList = loader.GetDevice();
        }

        public void SaveSubConfig(object var)
        {
            this.deviceTree = (TreeNode)var;
            RefreshDataWithTree();
        }

        #endregion

        //将树转为LIST保存到info当中
        private void RefreshDataWithTree()
        {
            //TODO: 暂时不需要但是之后升级配置界面后肯定需要
        }
    }
}
