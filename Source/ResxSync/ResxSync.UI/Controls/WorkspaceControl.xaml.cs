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

        Dictionary<ResxControl, GridSplitter> ResxAndSplitters = new Dictionary<ResxControl, GridSplitter>();

        public WorkspaceControl()
        {
            InitializeComponent();
            AddResx(@"..\..\..\ResxSync.Library.Tests\Dummy\1.resx");
            AddResx(@"..\..\..\ResxSync.Library.Tests\Dummy\2.resx");
            AddResx(@"..\..\..\ResxSync.Library.Tests\Dummy\2.resx");
            AddResx(@"..\..\..\ResxSync.Library.Tests\Dummy\3.resx");
        }

        public void AddResx(string path)
        {
            // Load resx and add it to syncer
            Resx loadedResx = new Resx(path);

            AddResxControl(loadedResx);
        }  

        private void RemoveResxControl(ResxControl resxControl)
        {
            // Remove ResxControl and its gridsplitter from main grid
            var resxControlColumnIndex = Grid.GetColumn(resxControl);
            var resxControlChildIndex = ResxControlsG.Children.IndexOf(resxControl);

            for (int currIndx = resxControlChildIndex + 2; currIndx < ResxControlsG.Children.Count; ++currIndx)
            {
                var currChild = ResxControlsG.Children[currIndx];

                Grid.SetColumn(currChild, Grid.GetColumn(currChild) - 2);
            }

            ResxControlsG.ColumnDefinitions.RemoveRange(resxControlColumnIndex, 2);
            ResxControlsG.Children.RemoveRange(resxControlChildIndex, 2);
        }

        private void AddResxControl(Resx loadedResx)
        {
            // Last phantom item so that we can resize the last item in grid via gridsplitter
            ResxControlsG.ColumnDefinitions.RemoveAt(ResxControlsG.ColumnDefinitions.Count - 1);

            // Add to syncer
            syncer.Add(loadedResx);

            // Create ResxControl
            ResxControl resxControl = new ResxControl();
            resxControl.Init(loadedResx, syncer);
            resxControl.Deleted += (object sender, EventArgs args) =>
            {
                var resxControlToDelete = sender as ResxControl;

                RemoveResxControl(resxControlToDelete);

                syncer.Remove(resxControlToDelete._resx);

                foreach (var resx in ResxAndSplitters.Keys)
                {
                    resx.Init(resx._resx, syncer);
                }
            };

            // Add it to grid
            ResxControlsG.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            ResxControlsG.Children.Add(resxControl);
            Grid.SetColumn(resxControl, ResxControlsG.ColumnDefinitions.Count - 1);

            // Create splitter for ResxControl
            GridSplitter splitter = new GridSplitter()
            {
                Background = new SolidColorBrush(Color.FromArgb(255, 0, 255, 0)),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
            };

            // Add it to grid
            ResxControlsG.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(5) });
            ResxControlsG.Children.Add(splitter);
            Grid.SetColumn(splitter, ResxControlsG.ColumnDefinitions.Count - 1);

            ResxAndSplitters.Add(resxControl, splitter);

            foreach (var resx in ResxAndSplitters.Keys)
            {
                resx.Init(resx._resx, syncer);
            }

            // Last phantom item so that we can resize the last item in grid via gridsplitter
            ResxControlsG.ColumnDefinitions.Add(new ColumnDefinition());
        }
    }
}
