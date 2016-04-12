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
using IntervalApp.DatabaseConn;
using IntervalApp.AccessoryUserControls;

namespace IntervalApp.MainUserControls
{
    /// <summary>
    /// Interaction logic for CreateDimensions.xaml
    /// </summary>
    public partial class CreateDimensions : UserControl
    {
        
        private List<ColumnBar> ColumnList;

        public CreateDimensions()
        {
            InitializeComponent();
            ColumnList = new List<ColumnBar>();
            AddColumnBar();
        }

                private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new ProjectPage());
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            bool result;
            result = CreateNewDimension();
            if (result)
                Switcher.Switch(new ProjectPage());
        }

        private void BtnAddColumn_Click(object sender, RoutedEventArgs e)
        {
            AddColumnBar();
        }


        //
        
        private void AddColumnBar()
        {
            StackPanel sp = new StackPanel();
            sp.Orientation = System.Windows.Controls.Orientation.Horizontal;

            ColumnBar ftp = new ColumnBar();
            ColumnList.Add(ftp);

            Button b = new Button();
            b.Content = "-";
            b.Height = 23;
            b.Width = 23;
            Thickness margin = b.Margin;
            margin.Left = 20;
            b.Margin = margin;
            b.Click += this.RemoveColumnBar;

            sp.Children.Add(ftp);
            sp.Children.Add(b);

            this.ColumnsContainer.Children.Add(sp);
        }

        private void RemoveColumnBar(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            StackPanel sp = (StackPanel)b.Parent;

            foreach (object o in sp.Children.OfType<ColumnBar>())
            {
                ColumnBar ftp = (ColumnBar)o;
                if (this.ColumnList.Contains(ftp))
                {
                    this.ColumnList.Remove(ftp);
                }
            }
            this.ColumnsContainer.Children.Remove(sp);

            if (this.ColumnList.Count == 0)
            {
                this.AddColumnBar();
            }
        }

        private bool CreateNewDimension()
        {
            string tmpStr = "";
            foreach (ColumnBar tmp in ColumnList)
            {
                tmpStr = tmpStr + " " + tmp.TxtColumnName.Text.ToString() + " " + tmp.TxtColumnType.Text.ToString();
            }
            TmpTxt.Text = tmpStr;

            return false;
        }
    }
}
