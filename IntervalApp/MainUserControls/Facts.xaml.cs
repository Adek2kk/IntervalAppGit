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


using MahApps.Metro.Controls;
using IntervalApp.Switchable;
using IntervalApp.DatabaseConn;
using System.Data;

namespace IntervalApp.MainUserControls
{
    /// <summary>
    /// Interaction logic for Facts.xaml
    /// </summary>
    public partial class Facts : UserControl
    {
        public Facts()
        {
            InitializeComponent();
            SetButtonFacts();
        }

       
        private void SetButtonFacts()
        {

            Connection conn = new Connection();
            conn.Open();
            string test = "select table_name as facts from dba_tables where table_name like '" + Application.Current.Resources["ProjectPrefix"] + "_FACT_%' and owner='HURTOWNIE'";
            DataSet testowy = conn.ExecuteDataSet(test);


            foreach (DataRow row in testowy.Tables["result"].Rows)
            {
                Button c = new Button();
                c.Content = row[0].ToString();
                c.Click += EditFact_Click;
                this.FactsContainer.Children.Add(c);
            }

            conn.Close();

        }

        private void BtnAddFact_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new CreateFacts());
        }

        private void EditFact_Click(object sender, RoutedEventArgs e)
        {
            Button tmp = (Button)sender;
            Switcher.Switch(new CreateFacts(tmp.Content.ToString()));
        }
    }
}
