using log4net.Appender;
using log4net.Core;
using System;
using System.IO;
using System.Windows.Forms;

namespace HyTestRTDataService.Utils
{
    class TextBoxAppender : AppenderSkeleton
    {
        public RichTextBox RichTextBox { get; set; }

        public string FormName { get; set; }

        public string TextBoxName { get; set; }

        public TextBoxAppender()
        {
        }

        private Control FindControlRecursive(Control root, string textBoxName)
        {
            if (root == null) return null;
            if (root.Name == textBoxName) return root;
            foreach (Control c in root.Controls)
            {
                Control t = FindControlRecursive(c, textBoxName);
                if (t != null) return t;
            }
            return null;
        }

        protected override void Append(LoggingEvent loggingEvent)
        {
            if (this.RichTextBox == null)
            {
                if (string.IsNullOrEmpty(FormName) ||
                    string.IsNullOrEmpty(TextBoxName))
                {
                    MessageBox.Show(FormName + "," + TextBoxName);
                    return;
                }
                Console.WriteLine(FormName);

                Form form = Application.OpenForms[FormName];
                this.RichTextBox = (RichTextBox)FindControlRecursive(form, TextBoxName);
                if (this.RichTextBox == null)
                {
                    return;
                }


                form.FormClosing += (s, e) => this.RichTextBox = null;
            }
            StringWriter writer = new StringWriter();
            this.Layout.Format(writer, loggingEvent);
            this.RichTextBox.BeginInvoke((MethodInvoker)delegate
            {
                this.RichTextBox.AppendText(writer.ToString());
            });
        }
    }
}
