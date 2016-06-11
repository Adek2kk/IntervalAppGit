using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Controls;

namespace IntervalApp.Switchable
{
    ///<summary>
    ///Class needed to switch UserControls in Window
    /// </summary>
    public static class Switcher
    {
        ///<summary>
        ///Main window to switch
        /// </summary>
        public static MainWindow pageSwitcher;

        ///<summary>
        ///Method open selected UserControl
        /// </summary>
        /// <param name="newPage">Selected UserControl</param>
        public static void Switch(UserControl newPage)
        {
            pageSwitcher.Navigate(newPage);
        }

        ///<summary>
        ///Method open selected UserControl
        /// </summary>
        /// <param name="newPage">Selected UserControl</param>
        /// <param name="state">Contain object to send to new UserControl</param>
        public static void Switch(UserControl newPage, object state)
        {
            pageSwitcher.Navigate(newPage, state);
        }
    }
}
