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
        //加载Config到本地
        void ReadSubConfig(object configInfo);
        //获取本地Config
        object GetSubConfig();
        //重新扫描配置
        void ScanSubConfig();
        //保存配置到subconfig.Info
        void SaveSubConfig(object var);
    }
}
