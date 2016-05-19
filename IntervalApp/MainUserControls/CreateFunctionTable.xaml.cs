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
using System.Data;
using ConnDBlib;

namespace IntervalApp.MainUserControls
{
    /// <summary>
    /// Interaction logic for CreateFunctionTable.xaml
    /// </summary>
    public partial class CreateFunctionTable : UserControl
    {
        public CreateFunctionTable()
        {
            InitializeComponent();
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new ProjectPage(2));
        }

        private void BtnSheet_Click(object sender, RoutedEventArgs e)
        {
            TableSheet sheet = new TableSheet();
            sheet.Show();
        }
        private void BtnExecute_Click(object sender, RoutedEventArgs e)
        {
            FunctionHandler.addFunction(TxtQuery.Text.ToString(), getTableName());
            Switcher.Switch(new ProjectPage(2));
        }
        private void BtnGenerate_Click(object sender, RoutedEventArgs e)
        {
            TxtQuery.Text = FunctionHandler.makeQueryAddFunction(getTableName(), "", "", "", "");
            
        }

        private string getTableName()
        {
            return Application.Current.Resources["ProjectPrefix"].ToString() + "_FUNCTION_" + TxtTableName.Text.ToString();
        }
        }
}
