using ConnDBlib;
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
    /// Interaction logic for CreateRelations.xaml
    /// </summary>
    public partial class CreateRelations : UserControl
    {
        public CreateRelations()
        {
            InitializeComponent();
            populateListView();
        }

        public void populateListView()
        {
            List<string> listItemsFac = new List<string>();
            List<string> listItemsDim = new List<string>();
            DataSet dimensions = DimensionHandler.getDimensions(Application.Current.Resources["ProjectPrefix"].ToString());
            DataSet facts = FactHandler.getFacts(Application.Current.Resources["ProjectPrefix"].ToString());

            foreach (DataRow row in dimensions.Tables["result"].Rows)
            {
                listItemsDim.Add(row[0].ToString());       
            }
            foreach (DataRow row in facts.Tables["result"].Rows)
            {
                listItemsFac.Add(row[0].ToString());
            }

            listViewTables1.ItemsSource = listItemsFac;
            listViewTables2.ItemsSource = listItemsDim;
        }
        public List<string> columnList(string tableName)
        {
            List<string> listItems = new List<string>();
            //DataSet testowy = FactHandler.getFactColumns(tableName);
            DataSet testowy = DimensionHandler.getDimensionColumns(tableName);
            foreach (DataRow row in testowy.Tables["result"].Rows)
            {
                listItems.Add(row[0].ToString());
            }
            return listItems;
        }

     /*   private void listViewTables1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listViewColumns1.ItemsSource = columnList(listViewTables1.SelectedItem.ToString());
        }
        */
        private void listViewTables2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listViewColumns2.ItemsSource = columnList(listViewTables2.SelectedItem.ToString());
        }

       /* private void button_Click(object sender, RoutedEventArgs e)
        {
            HierarchyHandler.addForeignKey(listViewTables1.SelectedItem.ToString(), listViewTables2.SelectedItem.ToString(), listViewColumns2.SelectedItem.ToString());
        }*/

        private void createFK_Click(object sender, RoutedEventArgs e)
        {

            //HierarchyHandler.addForeignKey(listViewTables1.SelectedItem.ToString(), listViewColumns1.SelectedItem.ToString(), listViewTables2.SelectedItem.ToString(), listViewColumns2.SelectedItem.ToString());
            Result wynik = new Result();
            wynik = HierarchyHandler.addForeignKey2(listViewTables1.SelectedItem.ToString(), listViewTables2.SelectedItem.ToString(), listViewColumns2.SelectedItem.ToString());

            if(wynik.errormsg!="OK")
            {
                textBlockError.Text = wynik.errormsg;
            }
            else
            Switcher.Switch(new ProjectPage(4));
        }

        private void goBack_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new ProjectPage(4));
        }
    }
}
