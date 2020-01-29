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
        public Resx AssociatedResx;

        public ResxControl()
        {
            InitializeComponent();
        }

        public void Init(Resx resx, ResxSyncer syncer)
        {
            AssociatedResx = resx;

            ValuesSP.Children.Clear();

            foreach (var syncKey in syncer.SyncKeys)
            {
                StackPanel kvpSP = new StackPanel() { Orientation = Orientation.Horizontal };
                kvpSP.Children.Add(new Label() { Content = syncKey.Key });

                TextBox valueTB = new TextBox();
                
                if (syncKey.Value.Owners.Contains(resx))
                {
                    valueTB.Text = resx.KVPs[syncKey.Key];
                }

                valueTB.TextChanged += (object sender, TextChangedEventArgs e) =>
                {
                    resx.KVPs[syncKey.Key] = valueTB.Text;
                };

                kvpSP.Children.Add(valueTB);

                ValuesSP.Children.Add(kvpSP);
            }
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
