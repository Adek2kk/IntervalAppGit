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
    /// Interaction logic for Hierarchies.xaml
    /// </summary>
    public partial class Hierarchies : UserControl
    {
        public Hierarchies()
        {
            InitializeComponent();
            populateHierarchies();
        }

        public void populateHierarchies()
        {
            DataSet hierarchies = HierarchyHandler.getConstraints(Application.Current.Resources["ProjectPrefix"].ToString());
            List<string> listItems = new List<string>();

            foreach (DataRow row in hierarchies.Tables["result"].Rows)
            {
                listItems.Add(row[1].ToString()+"|"+ row[3].ToString());
            }
            listViewHierarchies.ItemsSource = listItems;
        }

        private void addHierarchy_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new CreateHierarchies());
        }

        private void removeFK_Click(object sender, RoutedEventArgs e)
        {
            int idx = listViewHierarchies.SelectedItem.ToString().LastIndexOf('|');
            string tableToDel = listViewHierarchies.SelectedItem.ToString().Substring(idx+1);
            string constrName = listViewHierarchies.SelectedItem.ToString().Substring(0, idx);
            HierarchyHandler.dropConstraint(tableToDel, constrName);
            populateHierarchies();
        }
    }
}
