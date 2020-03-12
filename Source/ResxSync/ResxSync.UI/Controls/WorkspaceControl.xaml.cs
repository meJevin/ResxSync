using ResxSync.Library.Core;
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
    /// <summary>
    /// Interaction logic for WorkspaceControl.xaml
    /// </summary>
    public partial class WorkspaceControl : UserControl
    {
        ResxSyncer syncer = new ResxSyncer();
        ResxControl focusedResx = null;

        Dictionary<ResxControl, GridSplitter> ResxAndSplitters = new Dictionary<ResxControl, GridSplitter>();

        public WorkspaceControl()
        {
            InitializeComponent();
        }

        private void AddResx(object sender, RoutedEventArgs e)
        {
            var path = Utils.SelectFile();

            Resx loadedResx = new Resx(path);
            syncer.Add(loadedResx);

            ResxControl resxControl = new ResxControl();
            resxControl.Init(loadedResx, syncer);
            resxControl.Width = 250;

            resxControl.PreviewMouseDown += ResxControl_PreviewMouseDown;

            resxControl.ContextMenu = new ContextMenu();
            var deleteMI = new MenuItem() { Header = "Delete" };
            deleteMI.Click += DeleteMI_Click;
            resxControl.ContextMenu.Items.Add(deleteMI);
            resxControl.ContextMenuOpening += ResxControl_ContextMenuOpening;

            LoadedResxFilesG.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            
            LoadedResxFilesG.Children.Add(resxControl);

            Grid.SetColumn(resxControl, LoadedResxFilesG.ColumnDefinitions.Count - 1);

            GridSplitter splitter = new GridSplitter() { Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0)), Width = 5, HorizontalAlignment = HorizontalAlignment.Right, VerticalAlignment = VerticalAlignment.Stretch };
            LoadedResxFilesG.Children.Add(splitter);

            Grid.SetColumn(splitter, LoadedResxFilesG.ColumnDefinitions.Count - 1);

            ResxAndSplitters.Add(resxControl, splitter);
            LoadedResxFilesG.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });

            foreach (var resx in ResxAndSplitters.Keys)
            {
                resx.Init(resx._resx, syncer);
            }
        }

        private void ResxControl_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            focusedResx = (sender as ResxControl);
        }

        private void DeleteMI_Click(object sender, RoutedEventArgs e)
        {
            var resxToDelete = focusedResx._resx;

            syncer.Remove(resxToDelete);

            LoadedResxFilesG.Children.Remove(ResxAndSplitters[focusedResx]);
            LoadedResxFilesG.Children.Remove(focusedResx);

            foreach (var resx in ResxAndSplitters.Keys)
            {
                resx.Init(resx._resx, syncer);
            }
        }

        private void ResxControl_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            foreach (var rx in ResxAndSplitters.Keys)
            {
                if (rx == (sender as ResxControl))
                {
                    focusedResx = rx;
                    rx.Select();
                }
                else
                {
                    rx.Deselect();
                }
            }
        }
    }
}
