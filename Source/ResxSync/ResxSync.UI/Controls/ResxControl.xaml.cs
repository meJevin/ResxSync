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
            };

            TextBox KeyTB = new TextBox()
            {
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
                Text = syncKey.Key,
                IsReadOnly = true,
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

            Grid grid = new Grid();

            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());

            grid.Children.Add(KeyTB);
            grid.Children.Add(ValueTB);

            Grid.SetColumn(ValueTB, 1);

            ValuesSP.Children.Add(grid);
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            HighlightB.BorderBrush = new SolidColorBrush(Colors.Aqua);
            HighlightB.BorderThickness = new Thickness(2);
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            HighlightB.BorderThickness = new Thickness(0);
        }

        public void Select()
        {
            SelectionB.BorderBrush = new SolidColorBrush(Colors.Aquamarine);
            SelectionB.BorderThickness = new Thickness(2);
        }

        public void Deselect()
        {
            SelectionB.BorderThickness = new Thickness(0);
        }
    }
}
