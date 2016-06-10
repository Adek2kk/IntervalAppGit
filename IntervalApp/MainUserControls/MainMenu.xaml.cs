using System.Windows;
using System.Windows.Controls;
using IntervalApp.Switchable;
using IntervalApp.MainUserControls.ProjectManagement;
using ConnDBlib;
using System;

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

            if (InitHandler.checkIfInitDone() != "OK")
            {
                Console.WriteLine(InitHandler.checkIfInitDone());
                BtnInit.Visibility = Visibility.Visible;
            }


        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BtnOpen_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new OpenProjectPage());
        }

        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new NewProjectPage());
        }

        private void BtnTest_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources["ProjectPrefix"] = "TES";
            Switcher.Switch(new ProjectPage(0));
        }

        private void BtnInit_Click(object sender, RoutedEventArgs e)
        {
            string result;
            result = InitHandler.addTables();
            if (result != "OK")
                MessageBox.Show(result + "! Please check database.");
            else
            {
                InitHandler.addTestProject();
                result = InitHandler.addSequences();
                if (result != "OK")
                    MessageBox.Show(result + "! Please check database.");
                else
                {
                    result = InitHandler.addTriggers();
                    if (result != "OK")
                        MessageBox.Show(result + "! Please check databse.");
                }
            }
                        
        }
    }

    
}
