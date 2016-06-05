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
using System.Windows.Shapes;

using MahApps.Metro.Controls;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;

namespace IntervalApp.PlotViewModel
{
    /// <summary>
    /// Interaction logic for PlotWindow.xaml
    /// </summary>
    public partial class PlotWindow : MetroWindow
    {
        private MainViewModel mvm = null;
        public List<StatHolder> selectedStat;
        public PlotWindow(List<StatHolder> holder)
        {
            InitializeComponent();
            selectedStat = holder;
            
            mvm = new MainViewModel();
            mvm.Plot_2_Column(selectedStat);

            Chart.DataContext = mvm;

        }
    }
}
