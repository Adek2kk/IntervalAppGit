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
                Result corr = Connection.ExecuteNonQuery2(richText);
                //Juz dziala, bo obszedlem to dookola
                textBlockError.Text = corr.errormsg + "\nExecution time:"+corr.executiontime+" ms";
            }
            else
            {
                try
                {
                    Result wyniczek = Connection.ExecuteDataSet2(richText);
                    //DataSet queryres = Connection.ExecuteDataSet(richText);
                    if (wyniczek.errormsg == "OK")
                        Switcher.Switch(new QueryResult(wyniczek.wynik, richText, wyniczek.executiontime));
                    else textBlockError.Text = wyniczek.errormsg;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void sheet_Click(object sender, RoutedEventArgs e)
        {
            TableSheet sheet = new TableSheet();
            sheet.Show();
        }
    }
}
