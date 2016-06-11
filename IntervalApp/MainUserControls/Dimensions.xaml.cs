using System;
using System.Collections.Generic;
using System.Data;
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
using ConnDBlib;

namespace IntervalApp.MainUserControls
{   

    /// <summary>
    /// Interaction logic for Dimensions.xaml
    /// </summary>
    public partial class Dimensions : UserControl
    {
        public Dimensions()
        {
            InitializeComponent();
            SetButtonDimensions();
        }

        private void SetButtonDimensions()
        {
            DataSet testowy = DimensionHandler.getDimensions(Application.Current.Resources["ProjectPrefix"].ToString());
            
            foreach(DataRow row in testowy.Tables["result"].Rows)
            {
                Button c = new Button();
                c.Content = row[0].ToString();
                c.Click += EditDimension_Click;
                this.DimensionsContainer.Children.Add(c);
            }
        }

        private void BtnAddDim_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new CreateDimensions());
        }

        private void EditDimension_Click(object sender, RoutedEventArgs e)
        {
            Button tmp = (Button)sender;     
            Switcher.Switch(new CreateDimensions(tmp.Content.ToString()));
        }
    }
}
