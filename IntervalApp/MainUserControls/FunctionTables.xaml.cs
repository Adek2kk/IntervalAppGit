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
    /// Interaction logic for FunctionTables.xaml
    /// </summary>
    public partial class FunctionTables : UserControl
    {
        public FunctionTables()
        {
            InitializeComponent();
            SetButtonFunctionss();
        }

        private void SetButtonFunctionss()
        {

            DataSet testowy = FunctionHandler.getFunctions(Application.Current.Resources["ProjectPrefix"].ToString());

            foreach (DataRow row in testowy.Tables["result"].Rows)
            {
                Button c = new Button();
                c.Content = row[0].ToString();
                c.Click += ShowFunction_Click;
                this.FunctionContainer.Children.Add(c);
            }
        }

        private void BtnAddFunction_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new CreateFunctionTable());
        }

        private void ShowFunction_Click(object sender, RoutedEventArgs e)
        {
            Button tmp = (Button)sender;
            Switcher.Switch(new ShowFunction(tmp.Content.ToString()));
        }

    }
}
