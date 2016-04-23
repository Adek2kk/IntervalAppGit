using IntervalApp.Switchable;
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
    /// Interaction logic for Hierarchies.xaml
    /// </summary>
    public partial class Hierarchies : UserControl
    {
        public Hierarchies()
        {
            InitializeComponent();
        }

        private void addHierarchy_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new CreateHierarchies());
        }
    }
}
