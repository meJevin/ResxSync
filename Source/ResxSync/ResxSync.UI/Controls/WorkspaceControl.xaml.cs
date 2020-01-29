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
            resxControl.FillFrom(loadedResx);
            LoadedResxFilesSP.Children.Add(resxControl);
        }
    }
}
