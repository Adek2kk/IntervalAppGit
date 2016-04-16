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

namespace IntervalApp.MainUserControls.ProjectManagement
{
    /// <summary>
    /// Interaction logic for OpenProjectPage.xaml
    /// </summary>
    public partial class OpenProjectPage : UserControl
    {
        public OpenProjectPage()
        {
            InitializeComponent();
            FillProjectsButtons();
        }

        private void FillProjectsButtons()
        {
            Connection conn = new Connection();
            conn.Open();
            string test = "select prefix from main_projects";
            DataSet testowy = conn.ExecuteDataSet(test);


            foreach (DataRow row in testowy.Tables["result"].Rows)
            {
                Button c = new Button();
                c.Content = row[0].ToString();
                c.Click += GoToProject_Click;
                this.ProjectsContainer.Children.Add(c);
            }

            conn.Close();
        }

        private void GoToProject_Click(object sender, RoutedEventArgs e)
        {
            Button tmp = (Button)sender;
            Application.Current.Resources["ProjectPrefix"] = tmp.Content.ToString();
            Switcher.Switch(new ProjectPage());
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new MainMenu());
        }
    }
}
