using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppDevTool.AppComponents;

namespace AppDevTool.SettingsForms
{
    public partial class frmPrint : Form
    {
        public ConsolePrint Component { get; set; }

        public frmPrint()
        {
            InitializeComponent();
            TopLevel = false;
            AutoScroll = true;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Component.Message = string.Empty;
            Close();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            Component.Message = txtMessage.Text;
            Close();
        }

        private void frmPrint_Load(object sender, EventArgs e)
        {
            txtMessage.Text = Component.Message;
        }
    }
}
