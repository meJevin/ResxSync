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
            AddResxControl(@"..\..\..\ResxSync.Library.Tests\Dummy\1.resx");
            AddResxControl(@"..\..\..\ResxSync.Library.Tests\Dummy\2.resx");
            AddResxControl(@"..\..\..\ResxSync.Library.Tests\Dummy\2.resx");
            AddResxControl(@"..\..\..\ResxSync.Library.Tests\Dummy\3.resx");
        }

        private void AddResxControl(string path)
        {
            // Last phantom item so that we can resize the last item in grid via gridsplitter
            LoadedResxFilesG.ColumnDefinitions.RemoveAt(LoadedResxFilesG.ColumnDefinitions.Count - 1);

            // Load resx and add it to syncer
            Resx loadedResx = new Resx(path);
            syncer.Add(loadedResx);

            // Create ResxControl
            ResxControl resxControl = new ResxControl()
            {
            };
            resxControl.Init(loadedResx, syncer);

            // Add it to grid
            LoadedResxFilesG.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            LoadedResxFilesG.Children.Add(resxControl);
            Grid.SetColumn(resxControl, LoadedResxFilesG.ColumnDefinitions.Count - 1);

            // Create splitter for ResxControl
            GridSplitter splitter = new GridSplitter()
            {
                Background = new SolidColorBrush(Color.FromArgb(255, 0, 255, 0)),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
            };

            // Add it to grid
            LoadedResxFilesG.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(5) });
            LoadedResxFilesG.Children.Add(splitter);
            Grid.SetColumn(splitter, LoadedResxFilesG.ColumnDefinitions.Count - 1);

            ResxAndSplitters.Add(resxControl, splitter);

            foreach (var resx in ResxAndSplitters.Keys)
            {
                resx.Init(resx._resx, syncer);
            }

            // Last phantom item so that we can resize the last item in grid via gridsplitter
            LoadedResxFilesG.ColumnDefinitions.Add(new ColumnDefinition());
        }
    }
}
