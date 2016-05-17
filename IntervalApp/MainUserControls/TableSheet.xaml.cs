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

            listViewFacts.ItemsSource = listItemsFac;
            listViewDim.ItemsSource = listItemsDim;
        }
    }
}
