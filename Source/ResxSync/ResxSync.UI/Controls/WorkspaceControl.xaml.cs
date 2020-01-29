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

            LoadedResxFilesSP.Children.Add(resxControl);

            foreach (ResxControl resx in LoadedResxFilesSP.Children)
            {
                resx.Init(resx.AssociatedResx, syncer);
            }
        }

        private void ResxControl_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            focusedResx = (sender as ResxControl);
        }

        private void DeleteMI_Click(object sender, RoutedEventArgs e)
        {
            var resxToDelete = focusedResx.AssociatedResx;

            syncer.Remove(resxToDelete);

            LoadedResxFilesSP.Children.Remove(focusedResx);
        }

        private void ResxControl_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            foreach (ResxControl rx in LoadedResxFilesSP.Children)
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
