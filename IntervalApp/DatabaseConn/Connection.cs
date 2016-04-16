using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;
using System.Data;
using System.Windows.Controls;
using System.Windows.Documents;

namespace IntervalApp.DatabaseConn
{
    public class Connection
    {
        public string GetString(RichTextBox rtb)
        {
            var textRange = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
            return textRange.Text;
        }

        private OracleConnection conn;

        private string OracleServer = "Data Source=(DESCRIPTION="
                                     + "(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))"
                                     + "(CONNECT_DATA=(SERVICE_NAME=XE)));"
                                     + "User Id=hurtownie;Password=haslo;";

        public bool Open()
        {
            try
            {
                conn = new OracleConnection(OracleServer);
                conn.Open();
                System.Console.WriteLine("Connectod to database");
                return true;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                            }
            return false;
        }

        public void Close()
        {
            conn.Close();
            conn.Dispose();
        }

        public DataSet ExecuteDataSet(string sql)
        {
            try
            {
                DataSet ds = new DataSet();
                OracleDataAdapter da = new OracleDataAdapter(sql, conn);
                da.Fill(ds, "result");
                return ds;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            return null;
        }
        public OracleDataReader ExecuteReader(string sql)
        {
            try
            {
                OracleDataReader reader;
                OracleCommand cmd = new OracleCommand(sql, conn);
                reader = cmd.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            return null;
        }
        public int ExecuteNonQuery(string sql)
        {
            try
            {
                int affected;
                OracleTransaction mytransaction = conn.BeginTransaction();
                OracleCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                cmd.Connection = conn;
                affected = cmd.ExecuteNonQuery();
                mytransaction.Commit();
                return affected;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            return -1;
        }

        public void add_table(string tablename,string attributes,string type)
        {
            string sql = "Create table "+ type+"_"+ tablename +"(" + attributes +  ")";
            ExecuteNonQuery(sql);
        }

        public void insert_project(string attributes)
        {
            
            this.Open();

            string sql = "INSERT INTO MAIN_PROJECTS (NAME, PREFIX) VALUES " + " (" + attributes + ")";
            ExecuteNonQuery(sql);

            this.Close();
        }

    }
}

