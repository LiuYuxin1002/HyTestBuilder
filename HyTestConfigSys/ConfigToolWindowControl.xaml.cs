using HTConfigSystem.View;
//------------------------------------------------------------------------------
// <copyright file="ConfigToolWindowControl.xaml.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace HTConfigSystem
{
    using HyTestRTDataService.ConfigMode;
    using System.IO;
    using System.Net.NetworkInformation;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for ConfigToolWindowControl.
    /// </summary>
    public partial class ConfigToolWindowControl : UserControl
    {
        private ConfigManager cfm;
        private FormIOMapExplorer iomapForm;
        private FormDevicesExplorer deviceForm;
        private string configPath = "";
        private string ConfigPath
        {
            get
            {
                EnvDTE.DTE dte = new  EnvDTE.DTE();
                return Path.GetDirectoryName(dte.Solution.FullName) + @"/ValveTest/opcserver.xml";
            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigToolWindowControl"/> class.
        /// </summary>
        public ConfigToolWindowControl()
        {
            this.InitializeComponent();
            if (!isInited())
            {
                initConfigFilePath();
            }
            cfm = new ConfigManager();
            SetProtocolContext();
            SetAdapterContext();
        }

        private bool isInited()
        {
            MessageBox.Show(configPath);
            StreamReader sr = new StreamReader(configPath);
            string line = sr.ReadLine();    //这个文件保存具体的config.xml文件的路径
            sr.Close();
            if (File.Exists(line)) return true; //如果存在config.xml文件，返回成功，否则
            else return false;
        }

        private void initConfigFilePath()
        {
            string path = null;
            var openFileDialog = new Microsoft.Win32.SaveFileDialog()
            {
                Filter = "File (*.xml)|*.xml"
            };
            var result = openFileDialog.ShowDialog();
            if (result == true)
            {
                StreamWriter sw = new StreamWriter(configPath);
                sw.WriteLine(path);
                sw.Close();
            }
            
        }

        /// <summary>
        /// Handles click on the button by displaying a message box.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event args.</param>
        //[SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions", Justification = "Sample code")]
        //[SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Default event handler naming pattern")]
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(System.Environment.CurrentDirectory);
            FormIOMapExplorer iomapForm = new FormIOMapExplorer(this.cfm);
            if (iomapForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.cfm.SaveConfig();
            }
        }

        /// <summary>
        /// 在这里初始化支持协议
        /// </summary>
        private void SetProtocolContext()
        {
            string[] protocolList = new string[]
            {
                "EtherCAT",
                "Profinet",
            };
            foreach (string protocol in protocolList)
            {
                this.ComboBox_Protocol.Items.Add(protocol);
            }
            ComboBox_Protocol.SelectedIndex = 0;
        }

        /// <summary>
        /// 在这里初始化网络适配器加载
        /// </summary>
        private void SetAdapterContext()
        {
            this.ComboBox_Adapter.Items.Clear();

            NetworkInterface[] nics = GetAdapterList();
            foreach (NetworkInterface nic in nics)
            {
                this.ComboBox_Adapter.Items.Add(nic.Name);
            }
            ComboBox_Adapter.SelectedIndex = 0;
        }

        /// <summary>
        /// 获取网络适配器列表
        /// </summary>
        /// <returns>网络适配器列表，所需为ID，显示为Name</returns>
        private NetworkInterface[] GetAdapterList()
        {
            return NetworkInterface.GetAllNetworkInterfaces();
        }

        private void ComboBox_Devices_Click(object sender, RoutedEventArgs e)
        {
            FormDevicesExplorer deviceForm = new FormDevicesExplorer();
            deviceForm.ShowDialog();
            //MessageBox.Show(System.IO.Path.GetFullPath(".."));
        }

        private void button_Protocol_Click(object sender, RoutedEventArgs e)
        {
            SetProtocolContext();
        }

        private void button_DB_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_Adapter_Click(object sender, RoutedEventArgs e)
        {
            SetAdapterContext();
        }

        private void button_Init_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ComboBox_IOFrequency_TextChanged(object sender, TextChangedEventArgs e)
        {
            string value = this.ComboBox_FreshFrequency.Text;
            this.cfm.ConfigTestEnvInfo.refreshFrequency = int.Parse(value);
        }

        private void ComboBox_FreshFrequency_TextChanged(object sender, TextChangedEventArgs e)
        {
            string value = this.ComboBox_IOFrequency.Text;
            this.cfm.ConfigTestEnvInfo.redisRefreshFrequency = int.Parse(value);
        }

        private void ComboBox_Protocol_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboBox_DB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboBox_Adapter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.cfm.SaveAdapterConfig(this.ComboBox_Adapter.SelectedIndex);
        }
    }
}