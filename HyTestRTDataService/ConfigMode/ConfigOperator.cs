using HyTestRTDataService.ConfigMode.MapEntities;
using HyTestRTDataService.Entities;
using HyTestRTDataService.Interfaces;
using HyTestRTDataService.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HyTestRTDataService.ConfigMode
{
    public interface ConfigOperator : IDisposable
    {
        
    }

    public class AdapterConfigOperator : ConfigOperator, IDisposable
    {
        private IAdapterLoader loader;

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void ScanSubConfig(Config config)
        {
            NetworkInterface[] networks = NetworkInterface.GetAllNetworkInterfaces();
            config.AdapterInfo.Adapters = new Adapter[networks.Length];
            for (int i = 0; i < networks.Length; i++)
            {
                config.AdapterInfo.Adapters[i] = new Adapter();
                config.AdapterInfo.Adapters[i].Name = networks[i].Description;
                config.AdapterInfo.Adapters[i].Desc = networks[i].Id;
                config.AdapterInfo.Adapters[i].State = networks[i].OperationalStatus.GetType().Name;
            }
            config.AdapterInfo.Selected = 0;
        }
    }

    public class IoMapConfigOperator : ConfigOperator, IDisposable
    {
        public IoMapConfigOperator()
        {
            //ReadSubConfig(iomapInfo);
        }

        //get IOmapTable from .xml file
        private void initIOmapTable()
        {
            DataTable iomapTable = new DataTable();
            iomapTable.TableName = "IOmapTable";
            DataColumn colId    = iomapTable.Columns.Add("ID", typeof(int));
            DataColumn colName  = iomapTable.Columns.Add("变量名", typeof(string));
            DataColumn colType  = iomapTable.Columns.Add("变量类型", typeof(string));
            DataColumn colIO    = iomapTable.Columns.Add("IO类型", typeof(string));
            DataColumn colPort  = iomapTable.Columns.Add("端口号", typeof(string));
            DataColumn colMax   = iomapTable.Columns.Add("变量上限", typeof(int));
            DataColumn colMin   = iomapTable.Columns.Add("变量下限", typeof(int));
        }

        /// <summary>
        /// Refresh iomaps with datatable which is input now.
        /// </summary>
        /// <!--table structure：ID，var name，data type，I/O，port number-->
        public void RefreshMap(Config config)
        {
            if (config.IomapInfo.IoMapTable == null) return;

            config.IomapInfo.IoMapTable.TableName = "IOmapTable";
            config.IomapInfo.InputVarNum = config.IomapInfo.OutputVarNum = 0;
            DataTable mapTable = config.IomapInfo.IoMapTable;
            /*build map with dataTable.*/
            int index = 0;
            /*clear map*/
            config.IomapInfo.MapIndexToName.Clear();
            config.IomapInfo.MapNameToIndex.Clear();
            config.IomapInfo.MapNameToMax.Clear();
            config.IomapInfo.MapNameToMin.Clear();
            config.IomapInfo.MapNameToPort.Clear();
            config.IomapInfo.MapNameToType.Clear();
            config.IomapInfo.MapPortToName.Clear();
            /*remapping*/
            foreach (DataRow row in mapTable.Rows)
            {
                int    id       = index++;
                string name     = (string)row["变量名"];
                string type     = (string)row["变量类型"];
                string iotype   = (string)row["IO类型"];
                string port     = (string)row["端口号"];
                int    vmax     = int.Parse((string)row["变量上限"]);
                int    vmin     = int.Parse((string)row["变量下限"]);

                config.IomapInfo.MapIndexToName[id] = name;
                config.IomapInfo.MapNameToIndex[name] = id;
                config.IomapInfo.MapNameToPort[name] = port;
                config.IomapInfo.MapPortToName[port] = name;
                config.IomapInfo.MapNameToType[name] = type;
                config.IomapInfo.MapNameToMax[name] = vmax;
                config.IomapInfo.MapNameToMin[name] = vmin;

                /*count the num of input and output vars*/
                if (iotype.Contains("I"))
                {
                    config.IomapInfo.InputVarNum++;
                }
                else if (iotype.Contains("O"))
                {
                    config.IomapInfo.OutputVarNum++;
                }
            }
        }

        #region 公共方法
        public string SetIoMap(Config config)
        {
            string fileName = "";
            config.IomapInfo.IoMapTable = ExcelHelper.SelectExcelToDataTable(out fileName);
            RefreshMap(config);
            return fileName;
        }
        #endregion

        public void saveIOmapToExcel(Config config)
        {
            ExcelHelper.DataTableToExcel(config.IomapInfo.IoMapTable);
        }

        public void Dispose()
        {
            // If you need thread safety, use a lock around these 
            // operations, as well as in your methods that use the resource.
            //if (!_disposed)
            //{
            //    if (disposing)
            //    {
            //        if (_resource != null)
            //            _resource.Dispose();
            //        Console.WriteLine("Object disposed.");
            //    }

            //    // Indicate that the instance has been disposed.
            //    _resource = null;
            //    _disposed = true;
            //}

            // Use SupressFinalize in case a subclass
            // of this type implements a finalizer.
            GC.SuppressFinalize(this);
        }
    }

    public class DbConfigOperator : ConfigOperator
    {
        public object GetSubConfig()
        {
            throw new NotImplementedException();
        }

        public void ReadSubConfig(object configInfo)
        {
            throw new NotImplementedException();
        }

        public void SaveSubConfig(object var)
        {
            throw new NotImplementedException();
        }

        public void ScanSubConfig()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            // If you need thread safety, use a lock around these 
            // operations, as well as in your methods that use the resource.
            //if (!_disposed)
            //{
            //    if (disposing)
            //    {
            //        if (_resource != null)
            //            _resource.Dispose();
            //        Console.WriteLine("Object disposed.");
            //    }

            //    // Indicate that the instance has been disposed.
            //    _resource = null;
            //    _disposed = true;
            //}

            // Use SupressFinalize in case a subclass
            // of this type implements a finalizer.
            GC.SuppressFinalize(this);
        }
    }

    public class DevConfigOperator : ConfigOperator, IDisposable
    {
        private IDeviceLoader loader;

        //将info中的list<list<>>变为TreeNode
        public TreeNode ListToTree(Config config)
        {
            List<List<IOdevice>> deviceList = config.DeviceInfo.DeviceList;

            TreeNode deviceTree = new TreeNode("DEVICE");

            int deviceNum = 0, channelNum = 0;

            foreach (List<IOdevice> deviceGroup in deviceList)
            {
                if (deviceGroup == null) break;
                //For the first, we need to select coupler out.
                IOdevice coupler = deviceGroup[0];
                TreeNode couplerNode = new TreeNode(coupler.name);
                channelNum += coupler.channelNum;   //If servo, neet to count channel.
                deviceNum += deviceGroup.Count;     //add to deviceNum.
                for (int i = 1; i < deviceGroup.Count; i++)
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
            config.DeviceInfo.DeviceNum = deviceNum;
            config.DeviceInfo.AllChannelCount = channelNum;

            return deviceTree;
        }

        //将info中的list<list<>>变为datatable
        public DataTable ListToTable(List<List<IOdevice>> deviceList)
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
            foreach (List<IOdevice> devices in deviceList)
            {
                foreach (IOdevice device in devices)
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

        public void ScanSubConfig(Config config)
        {
            if (loader == null) loader = ConfigProtocol.GetDeviceLoader();

            config.DeviceInfo.DeviceList = loader.GetDevice();
        }

        //将树转为LIST保存到info当中
        private void RefreshDataWithTree()
        {
            //TODO: 暂时不需要但是之后升级配置界面后肯定需要
        }

        public void Dispose()
        {
            // If you need thread safety, use a lock around these 
            // operations, as well as in your methods that use the resource.
            //if (!_disposed)
            //{
            //    if (disposing)
            //    {
            //        if (_resource != null)
            //            _resource.Dispose();
            //        Console.WriteLine("Object disposed.");
            //    }

            //    // Indicate that the instance has been disposed.
            //    _resource = null;
            //    _disposed = true;
            //}

            // Use SupressFinalize in case a subclass
            // of this type implements a finalizer.
            GC.SuppressFinalize(this);
        }
    }

    public class ProtocolConfigOperator : ConfigOperator
    {
        public object GetSubConfig()
        {
            throw new NotImplementedException();
        }

        public void ReadSubConfig(object configInfo)
        {
            throw new NotImplementedException();
        }

        public void SaveSubConfig(object var)
        {
            throw new NotImplementedException();
        }

        public void ScanSubConfig()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            // If you need thread safety, use a lock around these 
            // operations, as well as in your methods that use the resource.
            //if (!_disposed)
            //{
            //    if (disposing)
            //    {
            //        if (_resource != null)
            //            _resource.Dispose();
            //        Console.WriteLine("Object disposed.");
            //    }

            //    // Indicate that the instance has been disposed.
            //    _resource = null;
            //    _disposed = true;
            //}

            // Use SupressFinalize in case a subclass
            // of this type implements a finalizer.
            GC.SuppressFinalize(this);
        }
    }

    public class TestEnvConfigOperator : ConfigOperator
    {
        public object GetSubConfig()
        {
            throw new NotImplementedException();
        }

        public void ReadSubConfig(object configInfo)
        {
            throw new NotImplementedException();
        }

        public void SaveSubConfig(object var)
        {
            throw new NotImplementedException();
        }

        public void ScanSubConfig()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            // If you need thread safety, use a lock around these 
            // operations, as well as in your methods that use the resource.
            //if (!_disposed)
            //{
            //    if (disposing)
            //    {
            //        if (_resource != null)
            //            _resource.Dispose();
            //        Console.WriteLine("Object disposed.");
            //    }

            //    // Indicate that the instance has been disposed.
            //    _resource = null;
            //    _disposed = true;
            //}

            // Use SupressFinalize in case a subclass
            // of this type implements a finalizer.
            GC.SuppressFinalize(this);
        }
    }
}
