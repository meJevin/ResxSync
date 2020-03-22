using ResxSync.Library.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for SyncKeyControl.xaml
    /// </summary>
    public partial class SyncKeyControl : UserControl
    {
        private ResxSyncer _syncer;
        private SyncKey _syncKey;

        public SyncKeyControl()
        {
            InitializeComponent();
        }

        public void Init(SyncKey syncKey, ResxSyncer syncer)
        {
            _syncer = syncer;
            _syncKey = syncKey;

            KeyTB.Text = _syncKey.Key;
            OwnersTB.Text = _syncKey.Owners.Count.ToString();
        }

        public void UpdateWith(SyncKey syncKey)
        {
            KeyTB.Text = syncKey.Key;
            OwnersTB.Text = syncKey.Owners.Count.ToString();
        }
    }
}
