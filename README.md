# HyTestBuilder

#### 介绍
**HyTestBuilder是一款基于Windows X86平台的液压测控系统开发平台软件**
老版本请看 [HyTestBuilder_old](https://github.com/LiuYuxin1002/HyTestBuilder_old)

#### 软件架构
参见《软件架构说明与课题规划》


#### 安装教程

1. 下载下来编译一下就行；
2. 涉及TwinCat的部分报错就直接删除那部分；

#### 使用说明

1. 开发环境Visual Studio 2015，根据开发环境不同可能会无法编译；

#### 进度说明

##2019.6.9 
1. 完善了配置模式的基本要求，写了配置模式的临时控件ConfigManager
2. 配置模式还存在如下问题：
	1) IOmap映射中，变量名是很好解决的，但是变量名对应的变量代号还有问题；
	2) 数据库配置和测试环境配置没想好怎么设计；
	3) 控件模式下ConfigManager运行会出现找不到ExportClass.dll的问题；
3. 运行模式亟待开发。

##2019.6.10 
更新： 
1. 添加了简易版RunningServer，拥有数据订阅功能。控件调用时，应做好回调函数。
2. 微调接口，将IReadWrite分成了IReader和IWriter，方便使用。
问题： 
1. 读写之前要进行数值转化，double和int转化为int，bool转化为bool；
2. 没有做好RunningServer数据订阅的事件绑定；

##2019.6.11
更新： 
1. 将RunningServer向下添加了数值转换类；
2. 将RunningServer向上添加了数据读写函数并完善了数据订阅；
问题： 
1.为绑定事件建立委托；
2.搭建界面进行读写测试；
3.完善之前的程序留空；
4.维护设备扫描的bug；