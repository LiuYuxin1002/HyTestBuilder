using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.ComponentModel.Design;

namespace HyTestRTDataService.ConfigMode.Component
{
    [Designer(typeof(ConfigDesigner), typeof(IDesigner))]
    public partial class ControlConfigManager : Control
    {
        public ControlConfigManager()
        {
            InitializeComponent();
        }
        public ControlConfigManager(IContainer container):this()
        {
            container.Add(this);
        }
    }

    internal class ConfigDesigner : ComponentDesigner
    {
        public ConfigDesigner() : base()
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
            ControlConfigManager confman = this.Component as ControlConfigManager;
            if (confman == null)
                return;

            FormConfigManager svrForm = new FormConfigManager(confman);
            svrForm.ShowDialog();
        }

    }

}

