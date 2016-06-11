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
using IntervalApp.MainUserControls;

namespace IntervalApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        ///<summary>
        ///MainWindow Construct
        /// </summary>
        /// <param name="newPage">Selected UserControl</param>
        public MainWindow()
        {
            InitializeComponent();
            Switcher.pageSwitcher = this;
            Switcher.Switch(new MainMenu());
        }

        ///<summary>
        ///Method open new UserControl
        /// </summary>
        /// <param name="nextPage">Selected UserControl</param>
        public void Navigate(UserControl nextPage)
        {
            this.Content = nextPage;
        }

        ///<summary>
        ///Method open new UserControl
        /// </summary>
        /// <param name="nextPage">Selected UserControl</param>
        /// <param name="state">Contain object that will be send to new UserControl</param>
        public void Navigate(UserControl nextPage, object state)
        {
            this.Content = nextPage;
            ISwitchable s = nextPage as ISwitchable;
 
            if (s != null)
                s.UtilizeState(state);
            else
                throw new ArgumentException("NextPage is not ISwitchable! "
                  + nextPage.Name.ToString());
        }
    }
}
