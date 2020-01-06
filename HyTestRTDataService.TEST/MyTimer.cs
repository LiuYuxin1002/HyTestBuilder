using System;
using System.Runtime.InteropServices;
using System.Security;

namespace HyTestRTDataService.TEST
{
    public class MyTimer
    {
        #region private members
        private long ticksPerSecond = 0;
        private long elapsedTime = 0;
        private long baseTime = 0;
        #endregion
        #region windows API
        /// <summary>
        /// 获取时间的精度
        /// </summary>
        /// <param name="PerformanceFrequency"></param>
        /// <returns></returns>
        [SuppressUnmanagedCodeSecurity]
        [DllImport("kernel32")]
        static private extern bool QueryPerformanceFrequency(ref long PerformanceFrequency);
        /// <summary>
        /// 获取时间计数
        /// </summary>
        /// <param name="PerformanceCount"></param>
        /// <returns></returns>
        [SuppressUnmanagedCodeSecurity]
        [DllImport("kernel32")]
        static private extern bool QueryPerformanceCounter(ref long PerformanceCount);
        #endregion
        #region constructors
        /// <summary>
        /// new
        /// </summary>
        public MyTimer()
        {
            // Use QueryPerformanceFrequency to get frequency of the timer
            if (!QueryPerformanceFrequency(ref ticksPerSecond))
                throw new ApplicationException("Timer: Performance Frequency Unavailable");
            Reset();
        }
        #endregion
        #region public methods
        /// <summary>
        /// 重置时间相关计数器
        /// </summary>
        public void Reset()
        {
            long time = 0;
            QueryPerformanceCounter(ref time);
            baseTime = time;
            elapsedTime = 0;
        }
        /// <summary>
        /// 获取当前与最近一次 reset 时间差
        /// </summary>
        /// <returns>The time since last reset.</returns>
        public double GetTime()
        {
            long time = 0;
            QueryPerformanceCounter(ref time);
            return (double)(time - baseTime) / (double)ticksPerSecond;
        }
        /// <summary>
        /// 获取当前系统的时间 ticks 数
        /// </summary>
        /// <returns>The current time in seconds.</returns>
        public double GetAbsoluteTime()
        {
            long time = 0;
            QueryPerformanceCounter(ref time);
            return (double)time * 1000 / (double)ticksPerSecond;
        }
        /// <summary>
        /// 获取此次与上次调用此方法的两次时间差
        /// </summary>
        /// <returns>The number of seconds since last call of this function.</returns>
        public double GetElapsedTime()
        {
            long time = 0;
            QueryPerformanceCounter(ref time);
            double absoluteTime = (double)(time - elapsedTime) * 1000 / (double)ticksPerSecond;
            elapsedTime = time;
            return absoluteTime;
        }

        public static implicit operator System.Timers.Timer(MyTimer v)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
