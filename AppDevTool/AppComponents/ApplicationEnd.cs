using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDevTool.AppComponents
{
    public class ApplicationEnd : AppComponent, IProtectedComponent
    {
        public override string SourceCode
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("}");

                return sb.ToString();
            }
        }

        public ApplicationEnd()
            : base("Application End")
        {

        }
    }
}
