using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HyTestRTDataService.ConfigMode.Component
{
    public partial class FormConfigManager : Form
    {
        private ConfigManager confman;

        public FormConfigManager()
        {
            InitializeComponent();
        }

        public FormConfigManager(ConfigManager confman)
        {
            this.confman = confman;
        }
    }
}
