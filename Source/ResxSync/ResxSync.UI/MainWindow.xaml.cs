using Microsoft.Win32;
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

namespace ResxSync.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ResxCollection col = new ResxCollection();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddFileButtonClick(object sender, RoutedEventArgs e)
        {
            #region Get file
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Resx (*.resx)|*.resx";
            ofd.RestoreDirectory = true;

            var res = ofd.ShowDialog();
            if (!res.HasValue || res.Value != true) return;
            #endregion

            col.Add(ofd.FileName);

            DrawAllKeys();
            DrawUnsyncedKeys();
        }

        private void RemoveFileButtonClick(object sender, RoutedEventArgs e)
        {
            string selectedFileName = LoadedFiles.SelectedItem as string;

            col.Remove(selectedFileName);

            DrawAllKeys();
            DrawUnsyncedKeys();
        }

        private void DrawAllKeys()
        {
            AllKeys.Items.Clear();

            AllKeys.Items.Add(String.Join("\t", col.ResxFiles.Keys));

            foreach (var key in col.KeyValues)
            {
                string lbResult = key.Key;

                foreach (var value in key.Value)
                {
                    lbResult += "\t" + value.Value;
                }
                AllKeys.Items.Add(lbResult);
            }

            LoadedFiles.Items.Clear();

            foreach (var fileName in col.ResxFiles.Keys)
            {
                LoadedFiles.Items.Add(fileName);
            }
        }

        private void DrawUnsyncedKeys()
        {
            var unsynced = col.GetUnsynced();

            UnsyncedKeys.Items.Clear();

            UnsyncedKeys.Items.Add(String.Join("\t", col.ResxFiles.Keys));
            foreach (var key in unsynced)
            {
                string lbResult = key.Key;

                foreach (var value in key.Value)
                {
                    lbResult += "\t" + value.Value;
                }
                UnsyncedKeys.Items.Add(lbResult);
            }
        }
    }
}
