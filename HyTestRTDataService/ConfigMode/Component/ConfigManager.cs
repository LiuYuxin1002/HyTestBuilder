using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.ComponentModel.Design;

namespace HyTestRTDataService.ConfigMode.Component
{
    [Designer(typeof(OpcConfigDesigner), typeof(IDesigner))]
    public partial class ConfigManager : Control
    {
        public ConfigManager()
        {
            InitializeComponent();
        }
        public ConfigManager(IContainer container)
        {
            container.Add(this);
        }
    }

    internal class OpcConfigDesigner : ComponentDesigner
    {
        public OpcConfigDesigner() : base()
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
                    this.m_Verbs.Add(new DesignerVerb("系统配置", new EventHandler(this.OnSystemConfiguration)));
                }
                return this.m_Verbs;
            }
        }

        private void OnSystemConfiguration(object sender, EventArgs args)
        {
            ConfigManager confman = this.Component as ConfigManager;
            if (confman == null)
                return;

            FormConfigManager svrForm = new FormConfigManager(confman);
            svrForm.ShowDialog();
        }

    }

}

