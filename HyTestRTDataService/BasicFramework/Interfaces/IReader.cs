using System;

namespace HyTestRTDataService.Interfaces
{
    public interface IReader
    {
        /// <summary>
        /// 单点读模拟量，对于伺服驱动器，需要按照配置表来选择channel值
        /// </summary>
        int ReadAnalog(int deviceId, int channel);
        
        /// <summary>
        /// 单点读数字量
        /// </summary>
        bool ReadBoolean(int deviceId, int channel);
        
    }

    public interface IAutoReader
    {
        event EventHandler<DataChangedEventArgs> AutoDataChanged;

        void InitAutoReadConfig();
        void StartAutoRead();
    }

    public class DataChangedEventArgs : EventArgs
    {
        public int slave;
        public int channel;
        public int value;

        public DataChangedEventArgs(int slave, int channel, int value)
        {
            this.slave = slave;
            this.channel = channel;
            this.value = value;
        }
    }
}
