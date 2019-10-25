using System.Collections.Generic;
using System.Data;

namespace HyTestRTDataService.RunningMode
{
    /// <summary>
    /// 此类作用在于保存高频读取任务
    /// </summary>
    public class ReadingTask
    {
        private int taskId;
        private string[] varNameList;
        private int taskDuration;
        private int frequency;
        private DataTable taskData;
        
        public int TaskId { get => taskId; set => taskId = value; }
        public string[] VarNameList { get => varNameList; set => varNameList = value; }
        public int TaskDuration { get => taskDuration; set => taskDuration = value; }
        public int Frequency { get => frequency; set => frequency = value; }
        public DataTable TaskData { get => taskData; set => taskData = value; }

        public ReadingTask(int taskId, string[] varNameList, int taskDuration, int frequency)
        {
            this.taskId = taskId;
            this.varNameList = varNameList;
            this.taskDuration = taskDuration;
            this.frequency = frequency;
        }
    }
}
