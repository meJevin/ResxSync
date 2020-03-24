using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ResxSync.UI.Controls
{
    public class SelectableWorkspaceControlEventArgs : EventArgs
    {
        public SelectableWorkspaceControl Control;
        public WorkspaceControl Workspace;
    }

    /// <summary>
    /// Interaction logic for SelectableWorkspaceControl.xaml
    /// </summary>
    public partial class SelectableWorkspaceControl : UserControl
    {
        public event EventHandler<SelectableWorkspaceControlEventArgs> Deleted;

        public event EventHandler<SelectableWorkspaceControlEventArgs> Selected;
        public event EventHandler<SelectableWorkspaceControlEventArgs> Deselected;

        private WorkspaceControl _workspace;
        public WorkspaceControl Workspace
        {
            get
            {
                return _workspace;
            }
        }

        private bool _isSelected = false;
        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
        }

        public SelectableWorkspaceControl()
        {
            InitializeComponent();

            _workspace = new WorkspaceControl();
        }

        public void Select()
        {
            _isSelected = true;

            BorderThickness = new Thickness(3);

            Selected?.Invoke(this, new SelectableWorkspaceControlEventArgs() { Control = this, Workspace = _workspace });
        }

        public void Delete()
        {
            Deleted?.Invoke(this, new SelectableWorkspaceControlEventArgs() { Control = this, Workspace = _workspace });
        }

        public void Deselect()
        {
            _isSelected = false;

            BorderThickness = new Thickness(0);

            Deselected?.Invoke(this, new SelectableWorkspaceControlEventArgs() { Control = this, Workspace = _workspace });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Delete();
        }
    }
}
