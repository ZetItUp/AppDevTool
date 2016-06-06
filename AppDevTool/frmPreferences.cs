using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.IO;

namespace AppDevTool
{
    public partial class frmPreferences : Form
    {
        public frmPreferences()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DevSettings.CompilerPath = txtCompilerPath.Text;
            DevSettings.CompilerArguments = txtArguments.Text;
            DevSettings.SaveSettings();
            Close();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            DialogResult dr = openFileDialog.ShowDialog();

            if(dr == DialogResult.OK)
            {

            }
        }

        private void frmPreferences_Load(object sender, EventArgs e)
        {
            if (DevSettings.CompilerPath == string.Empty)
            {
                var framework = RuntimeEnvironment.GetRuntimeDirectory();
                var cscPath = Path.Combine(framework, "csc.exe");
                txtCompilerPath.Text = cscPath;
            }
            else
            {
                txtCompilerPath.Text = DevSettings.CompilerPath;
            }

            txtArguments.Text = DevSettings.CompilerArguments;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtArguments.Text = DevSettings.DEFAULT_COMPILER_ARGUMENTS;
        }
    }
}
