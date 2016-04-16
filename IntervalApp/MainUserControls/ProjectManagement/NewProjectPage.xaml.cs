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
    /// Interaction logic for NewProjectPage.xaml
    /// </summary>
    public partial class NewProjectPage : UserControl
    {
        public NewProjectPage()
        {
            InitializeComponent();

        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources["ProjectPrefix"] = TxtPrefix.Text.ToString();
            Switcher.Switch(new ProjectPage(0));
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new MainMenu());
        }
    }
}
