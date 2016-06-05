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
    public class Result
    {
        public string errormsg;
        public DataSet wynik;
        public long executiontime;
    }
    /// <summary>
    /// Main class in library with basic method needed to connect to database, execute queries, open and close connection
    /// </summary>
    public static class Connection
    {

        /// <summary>
        /// This is used to make connection to Oracle database
        /// </summary>
        /// <value>The value can be any valid integer</value>
        private static OracleConnection conn;

        /// <summary>
        /// This is used to connect to Oracle database
        /// </summary>
        /// <value>The value must be a string with all needed information to connect to Oracle database</value>
        private static string OracleServer = "Data Source=(DESCRIPTION="
                                     + "(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))"
                                     + "(CONNECT_DATA=(SERVICE_NAME=XE)));"
                                     + "User Id=hurtownie;Password=haslo;";

        /// <summary>
        /// ?
        /// </summary>
        /// <param name="rtb">? </param>
        /// <returns>?</returns>
        public static string GetString(RichTextBox rtb)
        {
            var textRange = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
            return textRange.Text;
        }


        /// <summary>
        /// Method try to open connection with database
        /// </summary>
        /// <returns>Returns if connection is open (true) or failed to open (false)</returns>
        public static bool Open()
        {
            try
            {
                conn = new OracleConnection(OracleServer);
                conn.Open();
                //System.Console.WriteLine("Connected to database");
                return true;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            return false;
        }


        /// <summary>
        /// Method close connection with database
        /// </summary>
        public static void Close()
        {
            conn.Close();
            conn.Dispose();
        }


        /// <summary>
        /// Method execute query with data set as result
        /// </summary>
        /// <param name="sql">SQL query</param>
        /// <returns>Returns DataSet with result</returns>
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

        /// <summary>
        /// Method execute query with data set as result
        /// </summary>
        /// <param name="sql">SQL query</param>
        /// <returns>Returns DataSet with result</returns>
        public static Result ExecuteDataSet2(string sql)
        { Result ret = new Result();
            try
            {
                Open();
                DataSet ds = new DataSet();
                //startowanie czasomierza
                var watch = System.Diagnostics.Stopwatch.StartNew();
                OracleDataAdapter da = new OracleDataAdapter(sql, conn);
                watch.Stop();
                da.Fill(ds, "result");
                Close();
                ret.errormsg = "OK";
                ret.wynik = ds;
                ret.executiontime = watch.ElapsedMilliseconds;
                return ret;
            }
            catch (Exception ex)
            {
                ret.errormsg = ex.Message;
                ret.wynik = null;
                return ret;
            }
        }


        /// <summary>
        /// ?
        /// </summary>
        /// <param name="sql">SQL query </param>
        /// <returns>?</returns>
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


        /// <summary>
        /// Execute SQL query with DataSet as result
        /// </summary>
        /// <param name="sql">SQL query </param>
        /// <returns>Returns error code or -1 if successful</returns>
        public static string ExecuteNonQuery(string sql)
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
                Console.Write(affected);
                return affected.ToString();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ex.Message;
            }
            Console.Write(-1);
            return "-1";
        }

        /// <summary>
        /// Execute SQL query without dataset result
        /// </summary>
        /// <param name="sql">SQL query </param>
        /// <returns>Returns error code or -1 if successful</returns>
        public static Result ExecuteNonQuery2(string sql)
        {
            Result ret = new Result();
            try
            {
                Open();
                int affected;
                var watch = System.Diagnostics.Stopwatch.StartNew();
                OracleTransaction mytransaction = conn.BeginTransaction();
                OracleCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                cmd.Connection = conn;
                affected = cmd.ExecuteNonQuery();
                mytransaction.Commit();
                //Test czy rzeczywiscie mierzy czas;
                //System.Threading.Thread.Sleep(100);
                watch.Stop();
                Close();
                Console.Write(affected);
                ret.errormsg = "OK";
                ret.executiontime = watch.ElapsedMilliseconds;
                return ret;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ret.errormsg = ex.Message;
                return ret;
            }
        }



        /// <summary>
        /// Method add table to database
        /// </summary>
        /// <param name="tablename">Table name  projectPrefix_FUNCTION_tableName </param>
        /// <param name="attributes">Columns name </param>
        /// <param name="type">Columns type</param>
        /// <returns>?</returns>
        public static void add_table(string tablename, string attributes, string type)
        {
            string sql = "Create table " + type + "_" + tablename + "(" + attributes + ")";
            ExecuteNonQuery(sql);
        }


        /// <summary>
        /// Method insert row to database
        /// </summary>
        /// <param name="tableName">Table name  projectPrefix_FUNCTION_tableName </param>
        /// <param name="columns">Table columns</param>
        /// <param name="data">Data to insert</param>
        /// <returns>?</returns>
        public static void insert_row(string tableName, string columns, string data)
        {
            string sql = "INSERT INTO " +  tableName + " (" + columns + ") VALUES ("+ data + ")";
            ExecuteNonQuery(sql);
        }
    }
}
