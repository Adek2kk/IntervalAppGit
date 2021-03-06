﻿using ConnDBlib;
using IntervalApp.Switchable;
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

namespace IntervalApp.MainUserControls
{
    /// <summary>
    /// Interaction logic for CreateHierarchies.xaml
    /// </summary>
    public partial class CreateHierarchies : UserControl
    {
        public CreateHierarchies()
        {
            InitializeComponent();
            populateListView(null);
        }

        public void populateListView(object selected)
        {
            if (selected == null)
            {
                List<string> listItems = new List<string>();

                DataSet dimensions = DimensionHandler.getDimensions(Application.Current.Resources["ProjectPrefix"].ToString());
                // DataSet facts = FactHandler.getFacts(Application.Current.Resources["ProjectPrefix"].ToString());


                foreach (DataRow row in dimensions.Tables["result"].Rows)
                {
                    listItems.Add(row[0].ToString());
                }

                listViewTables1.ItemsSource = listItems;
                listViewTables2.ItemsSource = listItems;
            }
            else
            {
                List<string> listItems = new List<string>();

                DataSet dimensions = DimensionHandler.getDimensions(Application.Current.Resources["ProjectPrefix"].ToString());
                // DataSet facts = FactHandler.getFacts(Application.Current.Resources["ProjectPrefix"].ToString());


                foreach (DataRow row in dimensions.Tables["result"].Rows)
                {
                    if(row[0].ToString()!=selected.ToString())
                    listItems.Add(row[0].ToString());
                }
                listViewTables2.ItemsSource = listItems;
            }
            
        }
        public List<string> columnList(string tableName)
        {
            List<string> listItems = new List<string>();
           // DataSet testowy = FactHandler.getFactColumns(tableName);
            DataSet testowy = DimensionHandler.getDimensionColumns(tableName);
            foreach (DataRow row in testowy.Tables["result"].Rows)
            {
                listItems.Add(row[0].ToString());
            }
            return listItems;
        }

        private void listViewTables1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           populateListView(listViewTables1.SelectedItem);
            //Console.WriteLine(listViewTables1.SelectedItem);
            //listViewTables2.DataContext = this;
        }
        
        private void listViewTables2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listViewColumns2.ItemsSource = columnList(listViewTables2.SelectedItem.ToString());
        }

       /* private void button_Click(object sender, RoutedEventArgs e)
        {
            HierarchyHandler.addForeignKey(listViewTables1.SelectedItem.ToString(), listViewColumns1.SelectedItem.ToString(), listViewTables2.SelectedItem.ToString(), listViewColumns2.SelectedItem.ToString());
        }
        */
        private void createFK_Click(object sender, RoutedEventArgs e)
        {
            // HierarchyHandler.addForeignKey(listViewTables1.SelectedItem.ToString(), listViewColumns1.SelectedItem.ToString(), listViewTables2.SelectedItem.ToString(), listViewColumns2.SelectedItem.ToString());
            Result wynik = new Result();
            wynik = HierarchyHandler.addForeignKey2(listViewTables1.SelectedItem.ToString(),listViewTables2.SelectedItem.ToString(), listViewColumns2.SelectedItem.ToString());
            if (wynik.errormsg != "OK")
            {
                textBlockError.Text = wynik.errormsg;
            }
            else
            {
                Switcher.Switch(new ProjectPage(3));
            }
        }

        private void goBack_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new ProjectPage(3));
        }
    }
}
