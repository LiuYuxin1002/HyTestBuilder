using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyTestRTDataService.ConfigMode
{
    public interface IConfigBase
    {
        //加载Config到本地
        void ReadSubConfig(object configInfo);
        //获取本地Config
        object GetSubConfig();
        //扫描
        void ScanSubConfig();
        //保存
        void SaveSubConfig(object var);
    }
}
