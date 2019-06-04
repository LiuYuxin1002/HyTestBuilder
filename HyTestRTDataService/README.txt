实时数据服务模块-DLL项目说明

1.项目概要。
英文全称：Hydraulic Test Real-Time Data Service Module
实时数据服务模块位于接口层之上，应用层之下的数据服务层。
主要目的是对已经实现的接口（比如HyTestEtherCAT.dll）进行调用，并被应用层调用实现：
	1）协议转换		（包括：协议选择）
	2）数据读写		（包括：变量映射、数据读写等）
	3）测试环境配置	（包括：硬件配置、数据库配置等）

2.项目规划。
	1）三个部分分三个文件夹：Protocol, DataRw, Config
