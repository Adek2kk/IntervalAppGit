using IntervalApp.DatabaseConn;
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
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Connection conn = new Connection();
            conn.Open();
            string sql = "Create table " + tableName.Text + "(testowoid int)";
            Console.WriteLine(sql);
            conn.ExecuteNonQuery(sql);
            conn.Close();
        }
    }
}
