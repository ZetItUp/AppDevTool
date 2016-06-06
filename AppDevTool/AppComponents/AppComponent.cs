using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppDevTool.AppComponents
{
    public class AppComponent
    {
        protected List<AppComponent> _childComponents;

        protected string _sourceCode = string.Empty;
        public string ComponentName { get; set; }
        public int ParentTabs { get; set; }
        public int ImageIndex { get; set; }

        public virtual Form SettingsForm { get; protected set; }

        public void AddChildComponent(AppComponent childComponent)
        {
            _childComponents.Add(childComponent);
        }

        public TreeNode GetTreeNode()
        {
            TreeNode compNode = new TreeNode(ComponentName, ImageIndex, ImageIndex);
            compNode.Tag = this;

            foreach(var child in _childComponents)
            {
                compNode.Nodes.Add(child.GetTreeNode());
            }

            return compNode;
        }

        public void RemoveChildComponent(AppComponent childComponent)
        {
            _childComponents.Remove(childComponent);
        }

        public void RemoveChildComponent(int index)
        {
            _childComponents.RemoveAt(index);
        }

        public AppComponent(string componentName)
        {
            SettingsForm = null;
            ImageIndex = 1;
            ParentTabs = 0;
            _childComponents = new List<AppComponent>();
            ComponentName = componentName;
        }

        public virtual void UpdateComponentNameIndex(List<AppComponent> components)
        {
            
        }

        public virtual string SourceCode
        {
            get
            {
                return string.Empty;
            }
        }
    }
}
