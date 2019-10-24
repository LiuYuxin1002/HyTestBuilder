using HyTestRTDataService.Entities;

namespace HyTestRTDataService.Context
{
    /// <summary>
    /// During running, you can use these to resolve your problems.
    /// </summary>
    public class ConnectionContext
    {
        public static IOdevice[]    devices         { get; set; }   //板卡设备
        public static Adapter[]     adapters        { get; set; }   //网络适配器
        public static bool          needAdapter     { get; set; }   //是否有Adapter
        public static bool          isAutoRead      { get; set; }   //是否有AutoRead
        public static int           deviceNum       { get; set; }   //板卡数量
        public static int           inputDeviceNum  { get; set; }   //输入设备数量
        public static int           outputDeviceNum { get; set; }   //输出设备数量
        public static int           adapterNum      { get; set; }   //本机网卡数量
        public static int           adapterSelectId { get; set; }   //所选网卡
        public static string        adapterId       { get; set; }   //要传入驱动的字符串
    }
}
