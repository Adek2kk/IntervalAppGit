using IntervalApp.DatabaseConn;
using System;
using System.Collections.Generic;
using System.Data;
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


using MahApps.Metro.Controls;
using IntervalApp.Switchable;

namespace IntervalApp.MainUserControls
{   

    /// <summary>
    /// Interaction logic for Dimensions.xaml
    /// </summary>
    public partial class Dimensions : UserControl
    {
        public Dimensions()
        {
            InitializeComponent();
            SetButtonDimensions();
        }

        /*
        private void button_Click(object sender, RoutedEventArgs e)
        {
            Connection conn = new Connection();
            conn.Open();
            conn.add_table(tableName.Text, conn.GetString(tableAttributes),"dimension");
            string test = "select table_name as dimensions from dba_tables where table_name like 'DIMENSION_%' and owner='HURTOWNIE'";
            DataSet testowy = conn.ExecuteDataSet(test);
            Console.WriteLine(testowy.Tables["result"].ToString());
            dimensionDataGrid.ItemsSource=testowy.Tables["result"].DefaultView;
            conn.Close();
        }
        */

        private void SetButtonDimensions()
        {

            Connection conn = new Connection();
            conn.Open();
            string test = "select table_name as dimensions from dba_tables where table_name like 'DIMENSION_%' and owner='HURTOWNIE'";
            DataSet testowy = conn.ExecuteDataSet(test);
            

            foreach(DataRow row in testowy.Tables["result"].Rows)
            {
                Button c = new Button();
                c.Content = row[0].ToString();
                c.Click += EditDimension_Click;
                this.DimensionsContainer.Children.Add(c);
            }

            conn.Close();

        }

        private void BtnAddDim_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new CreateDimensions());
        }

        private void EditDimension_Click(object sender, RoutedEventArgs e)
        {
            Button tmp = (Button)sender;     
            Switcher.Switch(new CreateDimensions(tmp.Content.ToString()));
        }
    }
}
