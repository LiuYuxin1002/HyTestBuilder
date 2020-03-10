# HyTestBuilder

## 介绍
**HyTestBuilder是一款基于Windows X86平台的液压测控系统开发平台软件**
老版本请看 [HyTestBuilder_old](https://github.com/LiuYuxin1002/HyTestBuilder_old)
测试用例请看[HyTestBuilderTest](https://github.com/LiuYuxin1002/HyTestBuilderTest)

## 软件架构
参见《软件架构说明与课题规划》


## 安装教程
0. 预处理
	- 安装[winpcap](https://www.winpcap.org)
	- 检查本机是否有 SDK for VS C++(可能会导致无法调试)
1. 添加引用
	- 添加HyTestRTDataService.dll服务引用
	- 添加HyTestEthercatDriver.dll驱动引用
2. 配置  
	1. 在主窗体添加HTConfigManager控件。
    2. 右键选择“HT配置界面”。
	3. 完善信息并点击保存配置（注意：配置文件将存放在解决方案路径/debug目录）。
2. 试运行
	1. 检查配置文件是否加载，如果没有找到配置文件，搜索配置文件config.xml移动到解决方案路径的debug目录下
	2. 检查能否找到驱动引用，如果没有找到，请检查是否安装[winpcap](https://www.winpcap.org)
	3. 如果运行时出现某某问题导致“堆栈不对称”，尝试将项目的启动方式设置为x86，具体操作为：
		项目右键->生成->目标平台下拉框->x86；
		另外还可以添加CallingConvention = CallingConvention.Cdecl；
		如果没有解决，先检查你的数据类型，or联系我。
3. 调试
	调试界面可参考[HyTestBuilderTest](https://github.com/LiuYuxin1002/HyTestBuilderTest)解决方案，如果没有，联系我。

4. 反馈
	如果有好的建议或意见，可以在github创建账号，拉你到HyTestBuider项目：[HyTestBuilder](https://github.com/LiuYuxin1002/HyTestBuilder)

---

## 使用说明

1. 开发环境Visual Studio 2015，根据开发环境不同可能会无法编译；
2. 有时存在找不到ExportDll（现在是HyTestEtherCATDriver.dll）的情况；

## 异常解决
1. Q: HyTestEtherCAT.CppConnect::setDigitalValue”的调用导致堆栈不对称。原因可能是托管的 PInvoke 签名与非托管的目标签名不匹配。请检查 PInvoke 签名的调用约定和参数与非托管的目标签名是否匹配。 
   A: 添加CallingConvention = CallingConvention.Cdecl 

## 进度说明

### 2019.6.9 
1. 完善了配置模式的基本要求，写了配置模式的临时控件ConfigManager
2. 配置模式还存在如下问题：
	1) IOmap映射中，变量名是很好解决的，但是变量名对应的变量代号还有问题；
	2) 数据库配置和测试环境配置没想好怎么设计；
	3) 控件模式下ConfigManager运行会出现找不到ExportClass.dll的问题；
3. 运行模式亟待开发。

---
### 2019.6.10 
#### 更新： 
1. 添加了简易版RunningServer，拥有数据订阅功能。控件调用时，应做好回调函数。
2. 微调接口，将IReadWrite分成了IReader和IWriter，方便使用。
#### 问题： 
1. 读写之前要进行数值转化，double和int转化为int，bool转化为bool；
2. 没有做好RunningServer数据订阅的事件绑定；  

---
### 2019.6.11
#### 更新： 
1. 将RunningServer向下添加了数值转换类；
2. 将RunningServer向上添加了数据读写函数并完善了数据订阅；
#### 问题： 
1. 为绑定事件建立委托；
2. 搭建界面进行读写测试；
3. 完善之前的程序留空；
4. 维护设备扫描的bug；

---
### 2019.6.12

---
### 2019.6.13
#### 更新：
1. 重构ConfigManager，使之变为ConfigForm和各SubConfig之间的桥梁。主要体现在
	1. 唯一拥有Config对象，并能对其进行各种操作；
	2. 唯一拥有各SubConfig对象，可操作；
	3. 调用者拥有ConfigManager对象对所有配置进行操作；
2. 重构subconfig，使之初始化方便：
	1. 抽取关键参数为ConfigXXXInfo类，在subConfig中添加接收ConfigXXXInfo的构造函数；
	2. Config类替换参数为ConfigXXXInfo对象，统一进行初始化；
	3. 形成Config和SubConfig各有一套配置数据的形式
	4. 两套配置数据的关系如下 => Config的改变一定会引起SubConfig参数的改变；SubConfig参数的改变不影响Config的改变；
	点击保存后Config获取Subconfig的值对自身进行数据刷新
3. 完善IOmap的配置，实现了了Excel的导入导出功能；
#### 问题： 
1. IOmap配置部分还有问题：Config配置信息->IOmapConfig配置信息这一块还没有做好
2. 上面做好的同时，就要保证导入数据的正确性，需要添加Datatable的格式校验
3. StringToPort这个函数还是要考虑撤掉

---
### 2019.6.20
#### 前几期回顾： 
1. 一周来，工作主要集中在“读写问题”上，从EtherCAT的读写实现，到底层驱动的读写实现
2. 期间，按照上一期问题完善了IOmapconfig
3. 对configinfo进行了完善

#### 更新： 
1. 调试C++中，redis的循环写入部分，修改部分bug

#### 问题：
1. （读）redis写入解决了，还要解决设备读管理这一完整工作，具有：启停、可调周期的功能
2. （写）设备写入还没有开动，同样要做到可以：启停、周期可调
3. （读）C#端需要对redis做订阅，试验：订阅后C++端写入的最高频率，以便进一步分析可行性

---
### 2019.6.29
#### 更新：
1. 重构解决方案，删除不必要的项目和.c文件，将两个开源项目soem和hiredis整合到HyTestEthercatDriver项目中；
2. 添加了项目HyTestConfigSys作为配置工具窗口（一个custom tool window），用作配置模块的view部分；
3. 重写C++多线程实现；

#### 说明：
1. 总体来说配置系统新的思路就是将HyTestRTdataService项目作为Control，并存放Entity作为Model，将HyTestConfigSys作为View：
	1. Model   => HyTestRTdataService => Config类及其依赖
	2. View    => HyTestConfigSys
	3. Control => HyTestRTdataService => ConfigManager类及其依赖
2. C++多线程实现用于EtherCAT驱动读写从站端口：
	1. 读：完全扫描输入设备，每个周期将其打包为数据包发送到redis
	2. 写：接收调用，改变C++数据池缓存；

---
### 2019.7.2
#### 工作记录
1. 需要调试cpp的写部分。问题在slaveWriteSigleDigital函数，数组slave_arr没有值。

---
### 2019.7.19
#### 工作记录
1. 控件问题解决思路：
	控件先绑定OnConnected事件，当Server触发Connected后会触发绑定OndataChanged事件，绑定了ondatachanged才能在EtherCAT的timer后刷新自己的数值。
	server会在主程序启动后触发programRunning调用connected

### 2020.2.21
#### 添加倍福位移传感器EL5151
##### 传感器参数：   
  SM2 outputs   
     addr b   index: sub bitl data_type    name  
  [0x0000.0] 0x7000:0x01 0x01 BOOLEAN      Enable latch C  
  [0x0000.1] 0x7000:0x02 0x01 BOOLEAN      Enable latch extern on positive edge  
  [0x0000.2] 0x7000:0x03 0x01 BOOLEAN      Set counter  
  [0x0000.3] 0x7000:0x04 0x01 BOOLEAN      Enable latch extern on negative edge  
  [0x0000.4] 0x0000:0x00 0x04  
  [0x0001.0] 0x0000:0x00 0x08  
  [0x0002.0] 0x7000:0x11 0x20 UNSIGNED32   Set counter value  
  SM3 inputs  
     addr b   index: sub bitl data_type    name  
  [0x0006.0] 0x6000:0x01 0x01 BOOLEAN      Latch C valid  
  [0x0006.1] 0x6000:0x02 0x01 BOOLEAN      Latch extern valid  
  [0x0006.2] 0x6000:0x03 0x01 BOOLEAN      Set counter done  
  [0x0006.3] 0x0000:0x00 0x04  
  [0x0006.7] 0x6000:0x08 0x01 BOOLEAN      Extrapolation stall  
  [0x0007.0] 0x6000:0x09 0x01 BOOLEAN      Status of input A  
  [0x0007.1] 0x6000:0x0A 0x01 BOOLEAN      Status of input B  
  [0x0007.2] 0x6000:0x0B 0x01 BOOLEAN      Status of input C  
  [0x0007.3] 0x0000:0x00 0x01  
  [0x0007.4] 0x6000:0x0D 0x01 BOOLEAN      Status of extern latch  
  [0x0007.5] 0x6000:0x0E 0x01 BOOLEAN      Sync error  
  [0x0007.6] 0x0000:0x00 0x01  
  [0x0007.7] 0x6000:0x10 0x01 BOOLEAN      TxPDO Toggle  
  [0x0008.0] 0x6000:0x11 0x20 UNSIGNED32   Counter value  
  [0x000C.0] 0x6000:0x12 0x20 UNSIGNED32   Latch value  
  [0x0010.0] 0x6000:0x14 0x20 UNSIGNED32   Period value  
   
1. 定义(mycontext.h)：添加位移传感器结构体和链表
2. 接口(mycontext.cpp)：中添加读写接口
3. 初始化(SlaveConfigSystem.cpp)：
   1. 修改方法initLocalSlaveInfo()，在条件判断中加入如何判断是位移传感器，我的条件是输入输出都不为0且型号包含“EL5”关键字；
   2. 仿照伺服驱动器的方法，initLocalSlaveInfo()方法的本质目的是初始化数组slave_arr[]，并将数组中的输入输出映射到ec_slave的输入输出
4. 读取计数器(SlaveReadManager.h/.cpp):

5. 写入接口实现(SlaveWriteManager.h/.cpp):