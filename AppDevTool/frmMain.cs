using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppDevTool
{
    public partial class frmMain : Form
    {
        frmSourceCode frmSource;

        public frmMain()
        {
            InitializeComponent();
            frmSource = new frmSourceCode();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNewProject frm = new frmNewProject();
            frm.Owner = this;
            frm.ShowDialog(this);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            DevSettings.LoadSettings();
            AddForm(frmNewProject.CreateNewConsoleApplication("My Console Application"));
        }

        public void AddForm(Form newForm)
        {
            newForm.MdiParent = this;
            newForm.Show();
        }

        private void showSourceCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: Add GUI stuff

            var frm = ActiveMdiChild;
            if (frm is frmConsoleDesigner)
            {
                var currForm = frm as frmConsoleDesigner;

                if(frmSource == null || frmSource.IsDisposed)
                {
                    frmSource = new frmSourceCode();
                }

                frmSource.UpdateSourceCode(currForm.GenerateSourceCode());
                frmSource.Show();
            }
        }

        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPreferences frm = new frmPreferences();
            frm.ShowDialog(this);
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(DevSettings.CompilerPath == string.Empty)
            {
                MessageBox.Show("There is no compiler set for the application!\nGo to Options -> Preferences and set one.\n(F7 for Hotkey)", "Run Application", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Save a temporary source file
            WriteTempFile();
            CompileFile();
            LaunchFile();
        }

        private void CompileFile()
        {
            ProcessStartInfo compilerInfo = new ProcessStartInfo();
            compilerInfo.FileName = DevSettings.CompilerPath;
            compilerInfo.Arguments = DevSettings.GetCompilerArguments();

            try
            {
                using (Process exeProcess = Process.Start(compilerInfo))
                {
                    exeProcess.WaitForExit();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Unable to create the application!", "Create Application Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LaunchFile()
        {
            ProcessStartInfo file = new ProcessStartInfo();
            file.FileName = DevSettings.OutputPathToFullPath();

            try
            {
                using (Process exeProcess = Process.Start(file))
                {
                    exeProcess.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to start the application!", "Start Application Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void WriteTempFile()
        {
            var frm = ActiveMdiChild;
            if (frm is frmConsoleDesigner)
            {
                var currForm = frm as frmConsoleDesigner;
                using (StreamWriter sw = new StreamWriter("OutputFile.cs"))
                {
                    sw.Write(currForm.GenerateSourceCode());
                }
            }
        }

        public static void PerformExitOperations()
        {
            DevSettings.SaveSettings();
            Application.Exit();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PerformExitOperations();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            PerformExitOperations();
        }
    }
}
