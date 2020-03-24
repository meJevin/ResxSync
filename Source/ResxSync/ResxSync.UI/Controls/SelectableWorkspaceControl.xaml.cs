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
    /// Interaction logic for SelectableWorkspaceControl.xaml
    /// </summary>
    public partial class SelectableWorkspaceControl : UserControl
    {
        public event EventHandler Deleted;

        public event EventHandler Selected;
        public event EventHandler Deselected;

        private bool _isSelected = false;
        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
        }

        public SelectableWorkspaceControl()
        {
            InitializeComponent();
        }

        public void Select()
        {
            _isSelected = true;

            BorderThickness = new Thickness(3);

            Selected?.Invoke(this, null);
        }

        public void Delete()
        {
            Deleted?.Invoke(this, null);
        }

        public void Deselect()
        {
            _isSelected = false;

            BorderThickness = new Thickness(0);

            Deselected?.Invoke(this, null);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Delete();
        }
    }
}
