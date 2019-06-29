========================================================================
    DYNAMIC LINK LIBRARY : ExportClass Project Overview
========================================================================

AppWizard has created this ExportClass DLL for you.  

This file contains a summary of what you will find in each of the files that
make up your ExportClass application.

ExportClass.vcxproj
    这个项目主要负责生成供C#调用的动态链接库(dll)程序，主要包括输出文件（ExportClass.cpp）、
	配置线程管理器（Configuration Process Manager）和运行时线程管理器（Run-time Process 
	Manager）两部分。

	其中，配置线程管理器主要包括： 
		1.主站网络配置任务系统（NicConfigSystem）
		2.从站初始化配置任务系统（SlaveConfigSystem）
		3.Redis内存实时数据库初始化任务系统（RedisConfigSystem）
	
	运行时线程管理器主要包括：
		1.从站读取线程管理系统（SlaveReadManager)
		2.从站写入线程管理系统（SlaveWriteManager)
		3.安全线程管理系统（Safe-thread Management System)：又包括
			a)运行安全互锁管理（非周期）
			b)通讯状态监测管理（周期）

ExportClass.cpp
    主要负责生成供C#调用的动态链接库(dll)程序。

AssemblyInfo.cpp
	Contains custom attributes for modifying assembly metadata.

/////////////////////////////////////////////////////////////////////////////
Other notes:

AppWizard uses "TODO:" to indicate parts of the source code you
should add to or customize.

/////////////////////////////////////////////////////////////////////////////
