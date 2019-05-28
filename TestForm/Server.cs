using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestForm
{
    public class Server
    {
        public event EventHandler DataRefresh;
        public Timer timer = new Timer();
        public DataPool datapool = new DataPool();
        public Server()
        {
            timer.Tick += Timer_Tick;
            timer.Interval = 1000;
            
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            datapool.data_ai.value1 += 1;
            datapool.data_ai.value2 += 1;
            datapool.data_ai.value3 += 1;
            datapool.data_ai.value4 += 1;
            DataRefresh(null, null);
        }

        public void serverStart()
        {
            timer.Start();
        }

        public void serverStop()
        {
            timer.Stop();
        }
    }

    public class DataPool
    {
        public SlaveData_AI data_ai = new SlaveData_AI();
    }

    public class SlaveData_AI
    {
        public int value1 = 0;
        public int value2 = -19;
        public int value3 = 20;
        public int value4 = 99;
    }
}
