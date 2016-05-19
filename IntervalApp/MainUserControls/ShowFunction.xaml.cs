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

using IntervalApp.Switchable;
using ConnDBlib;
using System.Data;

namespace IntervalApp.MainUserControls
{
    /// <summary>
    /// Interaction logic for ShowFunction.xaml
    /// </summary>
    public partial class ShowFunction : UserControl
    {
        public ShowFunction()
        {
            InitializeComponent();
        }
        public ShowFunction(string tableName)
        {
            InitializeComponent();
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new ProjectPage(2));
        }

    }
}
