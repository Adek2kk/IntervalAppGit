using IntervalApp.Switchable;
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

namespace IntervalApp.MainUserControls
{
    /// <summary>
    /// Interaction logic for QueryResult.xaml
    /// </summary>
    public partial class QueryResult : UserControl
    {
        string query;
        public QueryResult()
        {
            InitializeComponent();
        }
        public QueryResult(DataSet wynik, string query)
        {
            InitializeComponent();
            resultQuery.ItemsSource = wynik.Tables["result"].DefaultView;
            this.query = query;
        }

        private void goBack_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new ProjectPage(6));
        }
    }
}
