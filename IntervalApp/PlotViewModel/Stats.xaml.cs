using IntervalApp.PlotViewModel;
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

using ConnDBlib;
using System.Data;
using System.ComponentModel;

namespace IntervalApp.MainUserControls
{
    /// <summary>
    /// Interaction logic for Stats.xaml
    /// </summary>
    public partial class Stats : UserControl
    {
        public List<string> listSelectedItems;
        public List<StatHolder> selectedStat;
        public Stats()
        {
            InitializeComponent();
            populateQueryList();
            selectedStat = new List<StatHolder>();
            listSelectedItems = new List<string>();
        }
        private void populateQueryList()
        {
            DataSet queries = StatHandler.getFunctions(Application.Current.Resources["ProjectPrefix"].ToString());
            List <string> listItems = new List<string> ();

            foreach (DataRow row in queries.Tables["result"].Rows)
            {
                string comment = row[1].ToString();
                string query = row[0].ToString();
                string time = row[2].ToString(); ;
                listItems.Add(query + "|comment: " + comment + "\n|time:" + time);
            }
            listView.ItemsSource = listItems;
        }
        private void BtnTestPlot_Click(object sender, RoutedEventArgs e)
        {
            PlotWindow plot = new PlotWindow(selectedStat);
            plot.Show();
        }

        private void BtnAddQuery_Click(object sender, RoutedEventArgs e)
        {
            string text = listView.SelectedItem.ToString();
            listSelectedItems.Add(text);
            listViewSelected.ItemsSource = listSelectedItems;

            StatHolder stat = new StatHolder();
            stat.query = text.Substring(0, text.LastIndexOf('|'));
            stat.time = Convert.ToInt64(text.Substring(text.LastIndexOf(':') + 1));
            selectedStat.Add(stat);
            Counter.Text = selectedStat.Count.ToString();
        }
    }

    
}
