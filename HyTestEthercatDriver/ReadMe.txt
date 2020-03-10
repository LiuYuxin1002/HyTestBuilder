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
Slave:2
 Name:EL5151
 Output size: 48bits
 Input size: 112bits
 State: 4
 Delay: 155[ns]
 Has DC: 1
 DCParentport:1
 Activeports:1.0.0.0
 Configured address: 1002
 Man: 00000002 ID: 141f3052 Rev: 001a0000
 SM0 A:1000 L: 128 F:00010026 Type:1
 SM1 A:1080 L: 128 F:00010022 Type:2
 SM2 A:1100 L:   6 F:00010024 Type:3
 SM3 A:1180 L:  14 F:00010020 Type:4
 FMMU0 Ls:00000000 Ll:   6 Lsb:0 Leb:7 Ps:1100 Psb:0 Ty:02 Act:01
 FMMU1 Ls:00000006 Ll:  14 Lsb:0 Leb:7 Ps:1180 Psb:0 Ty:01 Act:01
 FMMUfunc 0:1 1:2 2:3 3:0
 MBX length wr: 128 rd: 128 MBX protocols : 0c
 CoE details: 07 FoE details: 01 EoE details: 00 SoE details: 00
 Ebus current: 130[mA]
 only LRD/LWR:0
PDO mapping according to CoE :
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
