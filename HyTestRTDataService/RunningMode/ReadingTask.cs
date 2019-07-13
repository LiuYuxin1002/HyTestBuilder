using System.Collections.Generic;
using System.Data;

namespace HyTestRTDataService.RunningMode
{
    /// <summary>
    /// 此类作用在于保存读写所需任务
    /// </summary>
    public class ReadingTask
    {
        private int taskId;
        private string[] varNameList;
        private int taskDuration;
        private int frequency;
        private DataTable taskData;

        #region Properties

        /// <summary>
        /// 任务ID
        /// </summary>
        public int TaskId
        {
            get { return this.taskId; }
            set { this.taskId = value; }
        }

        /// <summary>
        /// 变量名称列表
        /// </summary>
        public string[] VarNameList
        {
            get
            {
                return this.varNameList;
            }
            set
            {
                this.varNameList = value;
            }
        }

        /// <summary>
        /// 持续时间
        /// </summary>
        public int TaskDuration
        {
            get
            {
                return this.taskDuration;
            }
            set
            {
                this.taskDuration = value;
            }
        }

        /// <summary>
        /// 测试频率
        /// </summary>
        public int Frequency
        {
            get
            {
                return this.frequency;
            }
            set
            {
                this.frequency = value;
            }
        }

        /// <summary>
        /// 任务数据
        /// </summary>
        public DataTable TaskData
        {
            get
            {
                return this.taskData;
            }
            set
            {
                this.taskData = value;
            }
        }

        #endregion

        public ReadingTask(int taskId, string[] varNameList, int taskDuration, int frequency)
        {
            this.taskId = taskId;
            this.varNameList = varNameList;
            this.taskDuration = taskDuration;
            this.frequency = frequency;
        }
    }
}
