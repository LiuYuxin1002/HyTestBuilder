using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyTestRTDataService.ConfigMode
{
    /// <summary>
    /// 本接口提供SubConfig配置的基本方法声明
    /// </summary>
    public interface IConfigBase
    {
        /// <summary>
        /// 加载Config到本地
        /// </summary>
        /// <param name="configInfo"></param>
        void ReadSubConfig(object configInfo);

        /// <summary>
        /// 获取本地Config
        /// </summary>
        /// <returns></returns>
        object GetSubConfig();
        
        /// <summary>
        /// 重新扫描配置
        /// </summary>
        void ScanSubConfig();

        /// <summary>
        /// 保存配置到subconfig.Info
        /// </summary>
        /// <param name="var"></param>
        void SaveSubConfig(object var);
    }
}
