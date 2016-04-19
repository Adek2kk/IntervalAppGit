using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Oracle.DataAccess.Client;
using System.Data;
using System.Windows.Controls;
using System.Windows.Documents;

namespace ConnDBlib
{
    public static class Connection
    {


        private static OracleConnection conn;

        private static string OracleServer = "Data Source=(DESCRIPTION="
                                     + "(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))"
                                     + "(CONNECT_DATA=(SERVICE_NAME=XE)));"
                                     + "User Id=hurtownie;Password=haslo;";


        public static string GetString(RichTextBox rtb)
        {
            var textRange = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
            return textRange.Text;
        }

        public static bool Open()
        {
            try
            {
                conn = new OracleConnection(OracleServer);
                conn.Open();
                System.Console.WriteLine("Connected to database");
                return true;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            return false;
        }

        public static void Close()
        {
            conn.Close();
            conn.Dispose();
        }

        public static DataSet ExecuteDataSet(string sql)
        {
            try
            {
                Open();
                DataSet ds = new DataSet();
                OracleDataAdapter da = new OracleDataAdapter(sql, conn);
                da.Fill(ds, "result");
                Close();
                return ds;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            return null;
        }
        public static OracleDataReader ExecuteReader(string sql)
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
        public static int ExecuteNonQuery(string sql)
        {
            try
            {
                Open();
                int affected;
                OracleTransaction mytransaction = conn.BeginTransaction();
                OracleCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                cmd.Connection = conn;
                affected = cmd.ExecuteNonQuery();
                mytransaction.Commit();
                Close();
                return affected;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            return -1;
        }

        public static void add_table(string tablename, string attributes, string type)
        {
            string sql = "Create table " + type + "_" + tablename + "(" + attributes + ")";
            ExecuteNonQuery(sql);
        }

 
    }
}
