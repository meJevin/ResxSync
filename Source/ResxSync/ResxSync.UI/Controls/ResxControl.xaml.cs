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
        public ResxControl()
        {
            InitializeComponent();
        }

        public void FillFrom(Resx resx)
        {
            ValuesSP.Children.Clear();

            foreach (var v in resx.KVPs.Values)
            {
                ValuesSP.Children.Add(new TextBox() { Text = v });
            }
        }
    }
}
