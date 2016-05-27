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
using System.Windows.Shapes;
using ConnDBlib;

namespace IntervalApp.MainUserControls
{
    /// <summary>
    /// Interaction logic for TableSheet.xaml
    /// </summary>
    public partial class TableSheet : Window
    {
        public TableSheet()
        {
            InitializeComponent();
            populateListView();
        }
        public void populateListView()
        {
            List<string> listItemsFac = new List<string>();
            List<string> listItemsDim = new List<string>();
            List<string> listItemsFun = new List<string>();
            DataSet dimensions = DimensionHandler.getDimensions(Application.Current.Resources["ProjectPrefix"].ToString());
            DataSet facts = FactHandler.getFacts(Application.Current.Resources["ProjectPrefix"].ToString());
           DataSet functions = FunctionHandler.getFunctions(Application.Current.Resources["ProjectPrefix"].ToString());
            foreach (DataRow row in dimensions.Tables["result"].Rows)
            {
                listItemsDim.Add(row[0].ToString());
            }
            foreach (DataRow row in facts.Tables["result"].Rows)
            {
                listItemsFac.Add(row[0].ToString());
            }
            foreach (DataRow row in functions.Tables["result"].Rows)
           {
                listItemsFun.Add(row[0].ToString());
           }

            listViewFacts.ItemsSource = listItemsFac;
            listViewDim.ItemsSource = listItemsDim;
            listViewFun.ItemsSource = listItemsFun;
        }
        public List<string> columnList(string tableName)
        {
            List<string> listItems = new List<string>();
            DataSet testowy = DimensionHandler.getDimensionColumns(tableName);
            foreach (DataRow row in testowy.Tables["result"].Rows)
            {
                listItems.Add(row[0].ToString());
            }
            return listItems;
        }

        private void listViewFacts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listViewFactsCol.ItemsSource = columnList(listViewFacts.SelectedItem.ToString());
        }

        private void listViewDim_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listViewDimCol.ItemsSource = columnList(listViewDim.SelectedItem.ToString());
        }

        private void listViewFun_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listViewFunCol.ItemsSource = columnList(listViewFun.SelectedItem.ToString());
        }
    }
}
