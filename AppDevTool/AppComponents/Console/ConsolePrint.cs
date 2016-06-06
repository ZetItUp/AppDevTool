using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppDevTool.AppComponents
{
    public class ConsolePrint : AppComponent
    {
        public string Message { get; set; }

        public override string SourceCode
        {
            get
            {
                string tabs = "";

                for(int i = 0; i < ParentTabs; i++)
                {
                    tabs += "\t";
                }

                string src = string.Format("{0}\tConsole.Write(\"{1}\");{2}", tabs, Message, Environment.NewLine);

                return src;
            }
        }

        public override Form SettingsForm
        {
            get
            {
                if(base.SettingsForm == null || base.SettingsForm.IsDisposed)
                {
                    base.SettingsForm = new SettingsForms.frmPrint();
                }

                return base.SettingsForm;
            }

            protected set
            {
                base.SettingsForm = value;
            }
        }

        public ConsolePrint()
            : base("Print")
        {
            SettingsForm = new SettingsForms.frmPrint();
        }

        public override void UpdateComponentNameIndex(List<AppComponent> components)
        {
            int existingAmount = 0;

            foreach (var comp in components)
            {
                if (comp.ComponentName != "Print" &&
                    comp.ComponentName.StartsWith("Print"))
                {
                    existingAmount++;
                }
            }

            int amount = existingAmount;

            foreach (var comp in components)
            {
                if(comp.ComponentName == "Print")
                {
                    comp.ComponentName = "Print" + amount;
                    amount++;
                }
            }
        }
    }
}
