﻿using Microsoft.Win32;
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
        }

        private void CreateNewWorkspace()
        {
            SelectableWorkspaceControl swc = new SelectableWorkspaceControl();

            WorkspacesLV.Items.Add(swc);

            swc.Deleted += (object sender, SelectableWorkspaceControlEventArgs e) =>
            {
                // Remove from ListView
                WorkspacesLV.Items.Remove(e.Control);

                DeleteWorkspace(e.Workspace);
            };

            swc.Selected += (object sender, SelectableWorkspaceControlEventArgs e) =>
            {
                SelectWorkspace(e.Workspace);
            };
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

            OpenFileDialog ofd = new OpenFileDialog()
            {
                Filter = "Resx files | *.resx",
                RestoreDirectory = true,
            };

            var res = ofd.ShowDialog();

            if (res.HasValue && res.Value)
            {
                _currentWorkspace.AddResx(ofd.FileName);
            }
        }
    }
}
