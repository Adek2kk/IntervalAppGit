using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
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

using ImportData;

namespace IntervalApp.MainUserControls
{
    /// <summary>
    /// Interaction logic for Import.xaml
    /// </summary>
    public partial class Import : UserControl
    {
        public Import()
        {
            InitializeComponent();
        }

        private void BtnFind_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();

            dialog.DefaultExt = ".csv";
            dialog.Filter = "CSV Files (*.csv)|*.csv";
            Nullable<bool> result = dialog.ShowDialog();

            if (result == true)
                TxtSource.Text = dialog.FileName.ToString();
        }

        private void BtnImport_Click(object sender, RoutedEventArgs e)
        {
            string SourcePath;
            SourcePath = TxtSource.Text;
            if (File.Exists(SourcePath))
            {
                if (SourcePath.Substring(SourcePath.Length - 4, 4) == ".csv")
                    ImportCSV.importTable(Application.Current.Resources["ProjectPrefix"].ToString(), SourcePath);
                else
                    MessageBox.Show("Type of file must be a .csv!");
            }
            else
                MessageBox.Show("File doesn't exists!");
        }
    }
}
