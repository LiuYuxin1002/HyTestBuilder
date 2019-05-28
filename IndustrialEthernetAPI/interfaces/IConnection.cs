using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwinCAT.Ads;

namespace IndustrialEthernetAPI
{
    public interface IConnection
    {
        event EventHandler datachanged;
        //ConnectionContext context { get; set; }

        /// <summary>
        /// 建立连接
        /// </summary>
        /// <returns>成功返回正值</returns>
        int connect();
        /// <summary>
        /// 关闭连接
        /// </summary>
        /// <returns>关闭返回正值</returns>
        int close();
        /// <summary>
        /// 断开连接
        /// </summary>
        /// <returns></returns>
        int disconnect();
    }

    class Test
    {
        TcAdsClient client;
    }
}