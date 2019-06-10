实时数据服务模块-DLL项目说明

1.项目概要。
英文全称：Hydraulic Test Real-Time Data Service Module
实时数据服务模块位于接口层之上，应用层之下的数据服务层。
主要目的是对已经实现的接口（比如HyTestEtherCAT.dll）进行调用，并被应用层调用实现：
	1）协议转换		（包括：协议选择）
	2）数据读写		（包括：变量映射、数据读写等）
	3）测试环境配置	（包括：硬件配置、数据库配置等）

2.项目规划。
	1）主要分为两个文件夹：ConfigMode（配置模式）, Running（运行模式）,除此之外还有XML工具包和控件Control工具包；
	2）测试环境配置方面：ConfigMode
		a）为上层提供五个控件：数据库配置控件、网卡配置控件、硬件管理控件、测试环境配置控件、变量表导入控件；（体现在控件包当中）
		b）五个控件各自拥有配置文件名称，配置完成后将文件保存到本地。使用时统一加载到内存；
		c）ConfigBase作为配置类的公共基类，拥有读写XML的基本功能；
	3）运行模式方面：RunningMode
		a）为运行中的程序提供数据读写和数据订阅的功能；
		b）数据读写：
		c）数据订阅：目前的工作主要集中在RunningServer上，有他进行数据定时读取到datapool，读写都写到datapool
		d) 提供数据显示控件：DigitalMeter, AnalogMeter