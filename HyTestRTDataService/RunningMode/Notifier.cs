using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyTestRTDataService.RunningMode
{
    public class Notifier
    {
        public event EventHandler<EventArgs> ProgramRunning;

        private static Notifier _notifier;
        public static Notifier notifier
        {
            get
            {
                if (_notifier == null)
                {
                    _notifier = new Notifier();
                }
                return _notifier;
            }
            set
            {
                _notifier = value;
            }
        }

        private Notifier()
        {
            
        }

        public void Run()
        {
            ProgramRunning(null, null);
        }
    }
}
