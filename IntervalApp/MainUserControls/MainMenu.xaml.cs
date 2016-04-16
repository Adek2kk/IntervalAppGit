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
using IntervalApp.MainUserControls.ProjectManagement;

namespace IntervalApp.MainUserControls
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : UserControl
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BtnOpen_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new ProjectPage());
        }

        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new NewProjectPage());
        }

        private void BtnTest_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources["ProjectPrefix"] = "tes";
            Switcher.Switch(new ProjectPage(0));
        }
    }

    
}
