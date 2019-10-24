using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.IO;
using System.Windows.Forms;

namespace HyTestRTDataService.ConfigMode.Components
{
    [Designer(typeof(HTConfigDesigner), typeof(IDesigner))]
    public partial class HTConfigManager : Component
    {
        public HTConfigManager()
        {
            InitializeComponent();
        }

        public HTConfigManager(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        private string ConfigFile
        {
            get
            {
                EnvDTE.DTE dte = GetService(typeof(EnvDTE.DTE)) as EnvDTE.DTE;
                string path = Path.GetDirectoryName(dte.Solution.FullName) + @"/bin/config.xml";
                return path;
            }
        }

        private ConfigManager config = null;

        public ConfigManager Config
        {
            get
            {
                if (this.config == null)
                {
                    config = new ConfigManager(ConfigFile);
                }
                return this.config;
            }
        }
    }

    internal class HTConfigDesigner : ComponentDesigner    //引用System.design
    {
        public HTConfigDesigner() : base()
        {
        }

        private DesignerVerbCollection m_Verbs;

        public override DesignerVerbCollection Verbs
        {
            get
            {
                if (this.m_Verbs == null)
                {
                    this.m_Verbs = new DesignerVerbCollection();
                    this.m_Verbs.Add(new DesignerVerb("HT配置界面", new EventHandler(this.OnServerConfigSelected)));
                }
                return this.m_Verbs;
            }
        }

        private void OnServerConfigSelected(object sender, EventArgs args)
        {
            HTConfigManager confman = this.Component as HTConfigManager;
            if (confman == null)
                return;

            FormConfigManager svrForm = new FormConfigManager(confman.Config);
            svrForm.ShowDialog();

        }
    }
}
