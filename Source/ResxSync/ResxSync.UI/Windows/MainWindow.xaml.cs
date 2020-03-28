using Microsoft.Win32;
using ResxSync.Library.Core;
using ResxSync.UI.Controls;
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

namespace ResxSync.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WorkspaceControl _currentWorkspace;
        public WorkspaceControl CurrentWorkspace
        {
            get
            {
                return _currentWorkspace;
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            //DummyFill();
        }

        private void DummyFill()
        {
            var wsp1 = CreateNewWorkspace();

            var wsp2 = CreateNewWorkspace();
            wsp2.AddResx(Utils.DummyResxDirectory + "1.resx");

            var wsp3 = CreateNewWorkspace();
            wsp3.AddResx(Utils.DummyResxDirectory + "2.resx");
            wsp3.AddResx(Utils.DummyResxDirectory + "1.resx");
            wsp3.AddResx(Utils.DummyResxDirectory + "1.resx");
            wsp3.AddResx(Utils.DummyResxDirectory + "1.resx");
            wsp3.AddResx(Utils.DummyResxDirectory + "2.resx");

            var wsp4 = CreateNewWorkspace();

            wsp4.AddResx(Utils.DummyResxDirectory + "1.resx");
            wsp4.AddResx(Utils.DummyResxDirectory + "2.resx");
            wsp4.AddResx(Utils.DummyResxDirectory + "3.resx");
            wsp4.AddResx(Utils.DummyResxDirectory + "3.resx");
        }

        private WorkspaceControl CreateNewWorkspace()
        {
            SelectableWorkspaceControl swc = new SelectableWorkspaceControl();
            swc.Margin = new Thickness(0, 10, 0, 10);

            WorkspacesSP.Children.Add(swc);

            swc.Deleted += (object sender, SelectableWorkspaceControlEventArgs e) =>
            {
                // Remove from ListView
                WorkspacesSP.Children.Remove(e.Control);

                DeleteWorkspace(e.Workspace);
            };

            swc.Selected += (object sender, SelectableWorkspaceControlEventArgs e) =>
            {
                SelectWorkspace(e.Workspace);
            };

            swc.MouseDoubleClick += (object sender, MouseButtonEventArgs e) =>
            {
                foreach (SelectableWorkspaceControl ws in WorkspacesSP.Children)
                {
                    ws.Deselect();
                }

                swc.Select();
            };

            return swc.Workspace;
        }

        private void SelectWorkspace(WorkspaceControl wsToSelect)
        {
            _currentWorkspace = wsToSelect;

            CurrentWorkspaceG.Children.Clear();
            CurrentWorkspaceG.Children.Add(_currentWorkspace);
        }

        private void DeleteWorkspace(WorkspaceControl wsToDelete)
        {
            if (_currentWorkspace == wsToDelete)
            {
                CurrentWorkspaceG.Children.Clear();
                _currentWorkspace = null;

                GC.Collect();
            }
        }

        private void AddWorkspaceMI_Click(object sender, RoutedEventArgs e)
        {
            CreateNewWorkspace();
        }

        private void AddResxMI_Click(object sender, RoutedEventArgs e)
        {
            if (_currentWorkspace == null)
            {
                MessageBox.Show("No workspace selected!");
                return;
            }

            _currentWorkspace.AddResx();
        }
    }
}
