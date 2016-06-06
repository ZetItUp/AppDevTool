using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppDevTool
{
    public partial class frmSourceCode : Form
    {
        public frmSourceCode()
        {
            InitializeComponent();
        }

        public void UpdateSourceCode(string source)
        {
            txtSource.Text = source;
        }
    }
}
