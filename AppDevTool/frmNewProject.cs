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
    public partial class frmNewProject : Form
    {
        public frmNewProject()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        public static frmConsoleDesigner CreateNewConsoleApplication(string projectTitle)
        {
            var newForm = new frmConsoleDesigner();
            newForm.Text = projectTitle;

            return newForm;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if(txtProject.Text == string.Empty)
            {
                MessageBox.Show("Fill in a Project Name!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if(rbConsole.Checked)
            {
                // Only support Console atm...
                var frm = Owner as frmMain;
                
                frm.AddForm(CreateNewConsoleApplication(txtProject.Text));
                Close();
            }
            else
            {
                MessageBox.Show("GUI Applications are not supported yet... :'(", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
