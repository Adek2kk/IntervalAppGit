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
using ConnDBlib;
using System.Data;
using IntervalApp.Switchable;

namespace IntervalApp.MainUserControls
{
    /// <summary>
    /// Interaction logic for Query.xaml
    /// </summary>
    public partial class Query : UserControl
    {
        public Query()
        {
            InitializeComponent();
        }
        public Query(string quer)
        {
            InitializeComponent();
            queryText.Document.Blocks.Clear();
            queryText.Document.Blocks.Add(new Paragraph(new Run(quer)));
        }

        private void executeQuery_Click(object sender, RoutedEventArgs e)
        {
            string richText = new TextRange(queryText.Document.ContentStart, queryText.Document.ContentEnd).Text;
            if (checkBoxRet.IsChecked.Value==false)
            {
                
                System.Console.WriteLine(richText);
                string corr = Connection.ExecuteNonQuery(richText);
                //Cos nie teges bo przy errorze powinno zwrocic cos innego niz -1, a zawsze sie wyswietla :/
                textBlockError.Text = corr;
            }
            else
            {
                DataSet queryres = Connection.ExecuteDataSet(richText);
                Switcher.Switch(new QueryResult(queryres,richText));
            }
        }

        private void sheet_Click(object sender, RoutedEventArgs e)
        {
            TableSheet sheet = new TableSheet();
            sheet.Show();
        }
    }
}
