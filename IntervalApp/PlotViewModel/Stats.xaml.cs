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
using System.Collections.ObjectModel;

namespace IntervalApp.MainUserControls
{
    /// <summary>
    /// Interaction logic for Stats.xaml
    /// </summary>
    public partial class Stats : UserControl
    {
        ObservableCollection<StatHolder> _QueryCollection = new ObservableCollection<StatHolder>();
        ObservableCollection<StatHolder> _SelectedQueryCollection = new ObservableCollection<StatHolder>();

        public ObservableCollection<StatHolder> QueryCollection { get { return _QueryCollection; } }
        public ObservableCollection<StatHolder> SelectedQueryCollection { get { return _SelectedQueryCollection; } }

        private StatHolder m_SelectedQuery;
        public StatHolder SelectedQuery { get { return m_SelectedQuery; } set { m_SelectedQuery = value; } }
   
        public List<StatHolder> selectedStat;

        public Stats()
        {
            InitializeComponent();
            selectedStat = new List<StatHolder>();
            AllLogs();
            this.DataContext = this;
        }
       
        private void BtnTestPlot_Click(object sender, RoutedEventArgs e)
        {
            PlotWindow plot = new PlotWindow(selectedStat);
            plot.Show();
        }

        private void BtnAddQuery_Click(object sender, RoutedEventArgs e)
        {
            StatHolder stat = new StatHolder();
            //error do opisania, wywala sie jak nic nie ma
            try
            {
                stat.sql = m_SelectedQuery.sql;
                stat.time = m_SelectedQuery.time;
                selectedStat.Add(stat);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Queriy log is empty!");
            }

            _SelectedQueryCollection.Add(m_SelectedQuery);

            this.DataContext = this;
        }

        private void BtnClearQuery_Click(object sender, RoutedEventArgs e)
        {
            _SelectedQueryCollection.Clear();
            selectedStat.Clear();
        }

        private void AllLogs()
        {
            // _QueryCollection.Clear();
            _QueryCollection.Clear();
            _SelectedQueryCollection.Clear();

            DataSet queries = StatHandler.getStats(Application.Current.Resources["ProjectPrefix"].ToString());
            //wywala sie jak nic nie ma
            try
            {
                foreach (DataRow row in queries.Tables["result"].Rows)
                {
                    _QueryCollection.Add(new StatHolder
                    {
                        id_query = Convert.ToInt32(row[0].ToString()),
                        sql = row[1].ToString(),
                        time = Convert.ToInt64(row[3].ToString()),
                        comment = row[2].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private void BtnDeleteSelected_Click(object sender, RoutedEventArgs e)
        {
            string ids = "";
            foreach (StatHolder row in _SelectedQueryCollection)
                ids = ids + "'" + row.id_query + "',";
            ids = ids.Remove(ids.Length - 1);
            StatHandler.deleteStats(ids);
            AllLogs();
        }
    }
}
