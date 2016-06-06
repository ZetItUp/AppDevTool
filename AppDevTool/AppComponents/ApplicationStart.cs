using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDevTool.AppComponents
{
    public class ApplicationStart : AppComponent, IProtectedComponent
    {
        public string ApplicationName { get; set; }

        public override string SourceCode
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("using System;");
                sb.AppendLine("using System.Collections.Generic;");
                sb.AppendLine("using System.Linq;");
                sb.AppendLine("using System.Text;");
                sb.AppendLine("using System.Threading.Tasks;");
                sb.AppendLine("");
                sb.AppendLine(string.Format("namespace {0}", ApplicationName));
                sb.AppendLine("{");
                sb.AppendLine(string.Format("\tpublic class {0}", ApplicationName));
                sb.AppendLine("\t{");
                sb.AppendLine("\t\tstatic void Main(string[] args)");
                sb.AppendLine("\t\t{");

                ParentTabs = 2;
                foreach(var childComp in _childComponents)
                {
                    childComp.ParentTabs = ParentTabs;
                    sb.Append(childComp.SourceCode);
                }

                sb.AppendLine("\t\t}");
                sb.AppendLine("\t}");

                return sb.ToString();
            }
        }

        public ApplicationStart()
            : base("Application Start")
        {

        }
    }
}
