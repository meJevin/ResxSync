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
        private WorkspaceControl CurrentWorkspace;

        public MainWindow()
        {
            InitializeComponent();

            WorkspacesLV.Items.Add(new SelectableWorkspaceControl());
            WorkspacesLV.Items.Add(new SelectableWorkspaceControl());
            WorkspacesLV.Items.Add(new SelectableWorkspaceControl());

            foreach (SelectableWorkspaceControl ws in WorkspacesLV.Items)
            {
                ws.Deleted += (object sender, SelectableWorkspaceControlEventArgs e) =>
                {
                    // Remove from ListView
                    WorkspacesLV.Items.Remove(e.Control);

                    DeleteWorkspace(e.Workspace);
                };

                ws.Selected += (object sender, SelectableWorkspaceControlEventArgs e) =>
                {
                    SelectWorkspace(e.Workspace);
                };
            }
        }

        private void SelectWorkspace(WorkspaceControl wsToSelect)
        {
            CurrentWorkspace = wsToSelect;

            CurrentWorkspaceG.Children.Clear();
            CurrentWorkspaceG.Children.Add(CurrentWorkspace);
        }

        private void DeleteWorkspace(WorkspaceControl wsToDelete)
        {
            if (CurrentWorkspace == wsToDelete)
            {
                CurrentWorkspaceG.Children.Clear();
                CurrentWorkspace = null;
            }
        }

        private void AddFileMI_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            var res = ofd.ShowDialog();

            if (res.HasValue && res.Value)
            {
                CurrentWorkspace.AddResx(ofd.FileName);
            }
        }

        private void WorkspacesLV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (WorkspacesLV.SelectedItem == null)
            {
                return;
            }

            var wsSelected = WorkspacesLV.SelectedItem as SelectableWorkspaceControl;

            foreach (SelectableWorkspaceControl ws in WorkspacesLV.Items)
            {
                ws.Deselect();
            }

            wsSelected.Select();
        }
    }
}
