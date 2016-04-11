using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;
using System.Data;

namespace IntervalApp.DatabaseConn
{
    public class Connection
    {
        private OracleConnection conn;

        private string OracleServer = "Data Source=(DESCRIPTION="
                                     + "(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))"
                                     + "(CONNECT_DATA=(SERVICE_NAME=XE)));"
                                     + "User Id=SYSTEM;Password=password;";

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
    }
}

