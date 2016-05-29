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

namespace IntervalApp.MainUserControls
{
    /// <summary>
    /// Interaction logic for Stats.xaml
    /// </summary>
    public partial class Stats : UserControl
    {
        
        public Stats()
        {
            InitializeComponent();
            
        }

        private void BtnTestPlot_Click(object sender, RoutedEventArgs e)
        {
            PlotWindow plot = new PlotWindow();
            plot.Show();
        }
    }
       
}
