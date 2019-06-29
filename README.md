# HyTestBuilder

## 介绍
**HyTestBuilder是一款基于Windows X86平台的液压测控系统开发平台软件**
老版本请看 [HyTestBuilder_old](https://github.com/LiuYuxin1002/HyTestBuilder_old)

## 软件架构
参见《软件架构说明与课题规划》


## 安装教程

1. 下载下来编译一下就行；
2. 涉及TwinCat的部分报错就直接删除那部分；

## 使用说明

1. 开发环境Visual Studio 2015，根据开发环境不同可能会无法编译；

## 进度说明

### 2019.6.9 
1. 完善了配置模式的基本要求，写了配置模式的临时控件ConfigManager
2. 配置模式还存在如下问题：
	1) IOmap映射中，变量名是很好解决的，但是变量名对应的变量代号还有问题；
	2) 数据库配置和测试环境配置没想好怎么设计；
	3) 控件模式下ConfigManager运行会出现找不到ExportClass.dll的问题；
3. 运行模式亟待开发。

### 2019.6.10 
#### 更新： 
1. 添加了简易版RunningServer，拥有数据订阅功能。控件调用时，应做好回调函数。
2. 微调接口，将IReadWrite分成了IReader和IWriter，方便使用。
#### 问题： 
1. 读写之前要进行数值转化，double和int转化为int，bool转化为bool；
2. 没有做好RunningServer数据订阅的事件绑定；

### 2019.6.11
#### 更新： 
1. 将RunningServer向下添加了数值转换类；
2. 将RunningServer向上添加了数据读写函数并完善了数据订阅；
#### 问题： 
1. 为绑定事件建立委托；
2. 搭建界面进行读写测试；
3. 完善之前的程序留空；
4. 维护设备扫描的bug；

### 2019.6.12


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

### 2019.6.29
#### 更新：
1. 重构解决方案，删除不必要的项目和.c文件，将两个开源项目soem和hiredis整合到HyTestEthercatDriver项目中；
2. 添加了项目HyTestConfigSys作为配置工具窗口（一个custom tool window），用作配置模块的view部分；
3. 重写C++多线程实现；

#### 说明：
1. 总体来说配置系统新的思路就是将HyTestRTdataService项目作为Control，并存放Entity作为Model，将HyTestConfigSys作为View：
	Model   => HyTestRTdataService => Config类及其依赖
	View    => HyTestConfigSys
	Control => HyTestRTdataService => ConfigManager类及其依赖
2. C++多线程实现用于EtherCAT驱动读写从站端口：
	读：完全扫描输入设备，每个周期将其打包为数据包发送到redis
	写：接收调用，改变C++数据池缓存；