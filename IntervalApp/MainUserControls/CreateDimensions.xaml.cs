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
using IntervalApp.AccessoryUserControls;
using System.Data;
using ConnDBlib;

namespace IntervalApp.MainUserControls
{
    /// <summary>
    /// Interaction logic for CreateDimensions.xaml
    /// </summary>
    public partial class CreateDimensions : UserControl
    {
        private bool updating;
        private List<ColumnBar2> ColumnList;

        public CreateDimensions()
        {
            InitializeComponent();
            ColumnList = new List<ColumnBar2>();
            AddColumnBar();
            updating = false;
        }

        public CreateDimensions(string tableName)
        {
            InitializeComponent();
            this.BtnCreate.Content = "Update";
            ColumnList = new List<ColumnBar2>();
            fillUpdateFields(tableName);
            TxtTableName.Text = tableName.Substring(tableName.LastIndexOf('_') + 1);
            updating = true;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new ProjectPage(0));
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            bool result;
            result = CreateNewDimension();
            if (result)
                Switcher.Switch(new ProjectPage(0));
        }

        private void BtnAddColumn_Click(object sender, RoutedEventArgs e)
        {
            AddColumnBar();
        }

        private void AddColumnBar()
        {
            StackPanel sp = new StackPanel();
            sp.Orientation = System.Windows.Controls.Orientation.Horizontal;

            ColumnBar2 ftp = new ColumnBar2();
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
                ColumnBar2 ftp = (ColumnBar2)o;
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
            if (updating)
                DimensionHandler.dropDimension(Application.Current.Resources["ProjectPrefix"].ToString(), TxtTableName.Text.ToString().ToUpper());
            

            string tmpStr = "";
            bool allFill = true;
            foreach (ColumnBar2 tmp in ColumnList)
            {
                if (tmp.TxtColumnName.Text.ToString() == "" || tmp.TxtColumnType.Text.ToString() == "")
                    allFill = false;
                tmpStr = tmpStr + tmp.TxtColumnName.Text.ToString() + " " + tmp.TxtColumnType.Text.ToString() +" "+tmp.TxtColumnCons.Text.ToString() + ",";
            }
            tmpStr = tmpStr.Remove(tmpStr.Length - 1);

            if (TxtTableName.Text.ToString() == "")
                allFill = false;
            if (allFill)
            {
                DimensionHandler.addDimension(TxtTableName.Text, tmpStr, Application.Current.Resources["ProjectPrefix"].ToString());
                Console.WriteLine(tmpStr);
                DataSet testowy = DimensionHandler.getDimensions(Application.Current.Resources["ProjectPrefix"].ToString());
                Console.WriteLine(testowy.Tables["result"].ToString());
                return true;
            }
            else
                MessageBox.Show("Fill all Textbox!!!");

            return false;

        }
        private void fillUpdateFields(string tableName)
        {

            DataSet testowy = DimensionHandler.getDimensionColumns(tableName);
            int i = 0;
            foreach (DataRow row in testowy.Tables["result"].Rows)
            {
                AddColumnBar();
                ColumnList[i].TxtColumnName.Text = row[0].ToString();
                ColumnList[i].TxtColumnType.Text = row[1].ToString() + "(" + row[2].ToString() + ")";
                i++;
            }
        }
    }
}
