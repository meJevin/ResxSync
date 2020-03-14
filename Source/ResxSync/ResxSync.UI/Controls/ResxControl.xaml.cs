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
    /// Interaction logic for ResxControl.xaml
    /// </summary>
    public partial class ResxControl : UserControl
    {
        public Resx _resx;

        public ResxControl()
        {
            InitializeComponent();
        }

        public void Init(Resx resx, ResxSyncer syncer)
        {
            _resx = resx;

            ValuesSP.Children.Clear();

            foreach (var syncKey in syncer.SyncKeys)
            {
                AddSyncKey(syncKey);
            }
        }

        private void AddSyncKey(SyncKey syncKey)
        {
            TextBox ValueTB = new TextBox()
            {
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
                Text = _resx.KVPs.ContainsKey(syncKey.Key) ? _resx.KVPs[syncKey.Key] : "",
                Padding = new Thickness(5),
            };
            ValueTB.TextChanged += (object sender, TextChangedEventArgs e) =>
            {
                if (!_resx.KVPs.ContainsKey(syncKey.Key))
                {
                    syncKey.Owners.Add(_resx);
                }
                else if (ValueTB.Text == "")
                {
                    syncKey.Owners.Remove(_resx);
                    _resx.KVPs.Remove(syncKey.Key);
                    return;
                }

                _resx.KVPs[syncKey.Key] = ValueTB.Text;
            };

            TextBox KeyTB = new TextBox()
            {
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
                Text = syncKey.Key,
                IsReadOnly = true,
                Padding = new Thickness(5),
            };

            Grid grid = new Grid() { HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Stretch};

            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());

            grid.Children.Add(KeyTB);
            grid.Children.Add(ValueTB);

            Grid.SetColumn(ValueTB, 1);

            ValuesSP.Children.Add(grid);
        }
    }
}
