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

namespace AppDevTool
{
    public partial class frmConsoleDesigner : Form
    {
        private Dictionary<string, AppComponent> _components;
        private List<string> _builtInComponents;

        private int GetComponentAmount<T>()
        {
            int amnt = 0;

            foreach (var comp in _components.Values)
            {
                if (comp is T)
                {
                    amnt++;
                }
            }

            return amnt;
        }

        public frmConsoleDesigner()
        {
            InitializeComponent();
            _components = new Dictionary<string, AppComponent>();
            _builtInComponents = new List<string>();
        }

        private void AddProtectedComponent(string key, AppComponent appComp)
        {
            if (_components.ContainsKey(key))
            {
                throw new ArgumentException("Multiple Protected Components Exists! Only 1 Instance of the same Protected Component is allowed!");
            }

            _components.Add(key, appComp);
        }

        public void AddProjectComponent(string key, AppComponent appComp)
        {
            if (appComp is IProtectedComponent)
            {
                throw new InvalidOperationException("Cannot add Protected Components to the project!");
            }

            _components.Add(key, appComp);
        }

        public AppComponent GenerateComponent(string componentType)
        {
            if (componentType == AppConsts.COMPONENT_DEFAULT_BUILT_IN_CONSOLE_PRINT)
            {
                return new ConsolePrint();
            }
            else if (componentType == AppConsts.COMPONENT_DEFAULT_BUILT_IN_CONSOLE_READKEY)
            {
                return new ConsoleReadKey();
            }

            return null;
        }

        public void AddComponent(string componentType)
        {
            // Check in built-in components first
            foreach (var comp in _builtInComponents)
            {
                if (comp == AppConsts.COMPONENT_DEFAULT_BUILT_IN_CONSOLE_PRINT)
                {
                    AddProjectComponent(AppConsts.COMPONENT_DEFAULT_BUILT_IN_CONSOLE_PRINT + GetComponentAmount<ConsolePrint>(), new ConsolePrint());
                    break;
                }
                else if (comp == AppConsts.COMPONENT_DEFAULT_BUILT_IN_CONSOLE_READKEY)
                {
                    AddProjectComponent(AppConsts.COMPONENT_DEFAULT_BUILT_IN_CONSOLE_READKEY + GetComponentAmount<ConsoleReadKey>(), new ConsoleReadKey());
                    break;
                }
            }
        }

        public AppComponent GetProjectComponentByKey(string componentKey)
        {
            return _components[componentKey];
        }

        public AppComponent GetProjectComponent(string componentName)
        {
            foreach (var comp in _components)
            {
                if (comp.Value.ComponentName == componentName)
                {
                    return comp.Value;
                }
            }

            return null;
        }

        public void RemoveProjectComponent(string key)
        {
            _components.Remove(key);
        }

        private void frmConsoleDesigner_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            AddBuiltInComponents();
            InitializeProjectComponents();
        }

        private void AddBuiltInComponents()
        {
            _builtInComponents.Add(AppConsts.COMPONENT_DEFAULT_BUILT_IN_CONSOLE_PRINT);
            _builtInComponents.Add(AppConsts.COMPONENT_DEFAULT_BUILT_IN_CONSOLE_READKEY);

            foreach (var comp in _builtInComponents)
            {
                lstComponents.Items.Add(comp, 1);
            }
        }

        private void InitializeProjectComponents()
        {
            ApplicationStart appStart = new ApplicationStart();
            appStart.ApplicationName = Text.Replace(" ", string.Empty);

            AddProtectedComponent(AppConsts.COMPONENT_APPLICATION_START, appStart);
            AddProtectedComponent(AppConsts.COMPONENT_APPLICATION_END, new ApplicationEnd());
            ForceProjectComponentsRefresh();
        }

        public void ForceProjectComponentsRefresh()
        {
            tvProjectComponents.Nodes.Clear();
            TreeNode rootNode = new TreeNode(Text, 2, 2);
            tvProjectComponents.Nodes.Add(rootNode);

            var c = GetProjectComponentByKey(AppConsts.COMPONENT_APPLICATION_START);

            tvProjectComponents.Nodes[0].Nodes.Add(c.GetTreeNode());

            foreach (var comp in _components)
            {
                if (comp.Value is ApplicationStart || comp.Value is ApplicationEnd)
                {
                    continue;
                }

                if (!(comp.Value is IProtectedComponent))
                {
                    comp.Value.UpdateComponentNameIndex(_components.Values.ToList());
                }

                tvProjectComponents.Nodes[0].Nodes.Add(comp.Value.GetTreeNode());
            }

            tvProjectComponents.Nodes[0].Nodes.Add(GetProjectComponentByKey(AppConsts.COMPONENT_APPLICATION_END).GetTreeNode());

            tvProjectComponents.ExpandAll();
        }

        public string GenerateSourceCode()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var comp in _components.Values)
            {
                sb.Append(comp.SourceCode);
            }

            return sb.ToString();
        }

        private void tvProjectComponents_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void tvProjectComponents_DragDrop(object sender, DragEventArgs e)
        {
            string componentType = e.Data.GetData(DataFormats.Text).ToString();
            TreeNode tn = tvProjectComponents.GetNodeAt(tvProjectComponents.PointToClient(new Point(e.X, e.Y)));

            if (tn != null)
            {
                var comp = GetProjectComponent(tn.Text);

                if (comp != null)
                {
                    var childComp = GenerateComponent(componentType);

                    comp.AddChildComponent(childComp);
                }
                else
                {
                    AddComponent(componentType);
                }
            }
            else
            {
                AddComponent(componentType);
            }
            ForceProjectComponentsRefresh();
        }

        private void lstComponents_MouseDown(object sender, MouseEventArgs e)
        {
            foreach (ListViewItem node in lstComponents.SelectedItems)
            {
                lstComponents.DoDragDrop(node.Text, DragDropEffects.Copy | DragDropEffects.Move);
            }
        }

        private void tvProjectComponents_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void tvProjectComponents_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            var comp = e.Node.Tag as AppComponent;

            if (comp.SettingsForm != null)
            {
                if (comp is ConsolePrint)
                {
                    var printComponent = comp as ConsolePrint;
                    splitContainer3.Panel2.Controls.Clear();
                    var settingsForm = printComponent.SettingsForm as SettingsForms.frmPrint;
                    settingsForm.Component = printComponent;
                    settingsForm.Show();
                    splitContainer3.Panel2.Controls.Add(settingsForm);
                }
            }
        }
    }
}
