using HyTestConfigSys.View;
//------------------------------------------------------------------------------
// <copyright file="ConfigToolWindowControl.xaml.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace HyTestConfigSys
{
    using System.Diagnostics.CodeAnalysis;
    using System.Net.NetworkInformation;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for ConfigToolWindowControl.
    /// </summary>
    public partial class ConfigToolWindowControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigToolWindowControl"/> class.
        /// </summary>
        public ConfigToolWindowControl()
        {
            this.InitializeComponent();
            SetProtocolContext();
            SetAdapterContext();
        }

        /// <summary>
        /// Handles click on the button by displaying a message box.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event args.</param>
        [SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions", Justification = "Sample code")]
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Default event handler naming pattern")]
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            FormIOMapExplorer iomapForm = new FormIOMapExplorer();
            iomapForm.Show();
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
            MessageBox.Show(System.IO.Path.GetFullPath(".."));
        }
    }
}