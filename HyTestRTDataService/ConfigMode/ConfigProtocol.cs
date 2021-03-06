﻿using System.Collections.Generic;
using HyTestRTDataService.Interfaces;
using HyTestRTDataService.Context;
using HyTestRTDataService.EtherCAT;
using HyTestRTDataService.Entities;

namespace HyTestRTDataService.ConfigMode
{
    public class ConfigProtocol
    {
        public static int currentProtocol;
        public IList<Protocol> protocolList;

        public static IReader reader;
        public static IWriter writer;
        public static IDeviceLoader deviceLoader;
        public static IAdapterLoader adapterLoader;
        public static IConnection HtEcConnector;

        public static ConnectionContext context;

        public ConfigProtocol()
        {
            protocolList = new List<Protocol>
            {
                new Protocol("EtherCAT"),
                new Protocol("NULL"),
            };
        }

        private static object GetServiceEntity()
        {
            object serviceEntity;
            switch (currentProtocol)
            {
                case 0:
                    serviceEntity = HtEthercat.getEtherCAT();
                    break;
                default:
                    serviceEntity = null;
                    break;
            }
            return serviceEntity;
        }

        /// <summary>
        /// Get Reader Which Is Adapte To The Protocol In Config
        /// </summary>
        public static IReader GetReader()
        {
            if (reader == null) reader = (IReader)GetServiceEntity();
            return reader;
        }

        /// <summary>
        /// Get Writer Which Is Adapte To The Protocol In Config
        /// </summary>
        public static IWriter GetWriter()
        {
            if (writer == null) writer = (IWriter)GetServiceEntity();
            return writer;
        }

        /// <summary>
        /// Get DeviceLoader Which Is Adapte To The Protocol In Config
        /// </summary>
        public static IDeviceLoader GetDeviceLoader()
        {
            if (deviceLoader == null) deviceLoader = (IDeviceLoader)GetServiceEntity();
            return deviceLoader;
        }

        /// <summary>
        /// Get AdapterLoader Which Is Adapte To The Protocol In Config
        /// </summary>
        /// <returns></returns>
        public static IAdapterLoader GetAdapterLoader()
        {
            if (adapterLoader == null)
            {
                adapterLoader = (IAdapterLoader)GetServiceEntity();
            }
            return adapterLoader;
        }

        /// <summary>
        /// Get HtEcConnector.
        /// </summary>
        /// <returns></returns>
        public static IConnection GetConnection()
        {
            if (HtEcConnector == null) HtEcConnector = (IConnection)GetServiceEntity();
            return HtEcConnector;
        }
    }
}
