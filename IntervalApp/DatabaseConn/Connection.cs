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
    }
}
