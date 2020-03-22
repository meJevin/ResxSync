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

        public event EventHandler Deleted;

        public ResxControl()
        {
            InitializeComponent();
        }

        public void Init(Resx resx, ResxSyncer syncer)
        {
            _resx = resx;

            ValuesSP.Children.Clear();
            //KeysSP.Children.Clear();

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
                TextWrapping = TextWrapping.NoWrap,
                Height = 50,
            };

            ValueTB.TextChanged += (object sender, TextChangedEventArgs e) =>
            {
                SyncKey_ValueChanged(syncKey, ValueTB.Text);
                HighlightValueTB(ValueTB);
            };

            HighlightValueTB(ValueTB);

            ValuesSP.Children.Add(ValueTB);
            //KeysSP.Children.Add(KeyTB);
        }

        private void SyncKey_ValueChanged(SyncKey syncKey, string newValue)
        {
            if (!_resx.KVPs.ContainsKey(syncKey.Key))
            {
                // Didn't have that key before, let's add it
                syncKey.Owners.Add(_resx);
            }
            else if (newValue == "")
            {
                // Empty value for key, remove it
                syncKey.Owners.Remove(_resx);
                _resx.KVPs.Remove(syncKey.Key);
            }

            _resx.KVPs[syncKey.Key] = newValue;
        }

        private void HighlightValueTB(TextBox TB)
        {
            if (TB.Text == "")
            {
                TB.Background = new SolidColorBrush(Color.FromArgb(100, 255, 0, 0));
            }
            else
            {
                TB.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Deleted?.Invoke(this, e);
        }
    }
}
