using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppDevTool.AppComponents
{
    public class ConsoleReadKey : AppComponent
    {
        public override string SourceCode
        {
            get
            {
                string tabs = "";

                for (int i = 0; i < ParentTabs; i++)
                {
                    tabs += "\t";
                }

                string src = string.Format("{0}\tConsole.Read();{1}", tabs, Environment.NewLine);

                return src;
            }
        }

        public ConsoleReadKey()
            : base(AppConsts.COMPONENT_DEFAULT_BUILT_IN_CONSOLE_READKEY)
        {
            
        }

        public override void UpdateComponentNameIndex(List<AppComponent> components)
        {
            int existingAmount = 0;

            foreach (var comp in components)
            {
                if (comp.ComponentName != AppConsts.COMPONENT_DEFAULT_BUILT_IN_CONSOLE_READKEY &&
                    comp.ComponentName.StartsWith(AppConsts.COMPONENT_DEFAULT_BUILT_IN_CONSOLE_READKEY))
                {
                    existingAmount++;
                }
            }

            int amount = existingAmount;

            foreach (var comp in components)
            {
                if (comp.ComponentName == AppConsts.COMPONENT_DEFAULT_BUILT_IN_CONSOLE_READKEY)
                {
                    comp.ComponentName = AppConsts.COMPONENT_DEFAULT_BUILT_IN_CONSOLE_READKEY + amount;
                    amount++;
                }
            }
        }
    }
}
