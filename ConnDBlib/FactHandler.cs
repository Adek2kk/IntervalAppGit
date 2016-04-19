using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;

namespace ConnDBlib
{
    public static class FactHandler
    {
        public static void addFact(string tablename, string attributes, string type)
        {
            string sql = "Create table " + type + "_FACT_" + tablename + "(" + attributes + ")";
            Connection.ExecuteNonQuery(sql);
        }

        public static DataSet getFacts(string prefix)
        {
            string test = "select table_name as facts from dba_tables where table_name like '" + prefix + "_FACT_%' and owner='HURTOWNIE'";
            return Connection.ExecuteDataSet(test);
        }

        public static void dropFact(string prefix, string tableName)
        {
            string test = "DROP table " + prefix + "_FACT_" + tableName + " CASCADE CONSTRAINTS";
            Connection.ExecuteNonQuery(test);
        }

        public static DataSet getFactColumns(string tableName)
        {
            string test = "SELECT  column_name, data_type,data_length FROM USER_TAB_COLUMNS WHERE table_name ='" + tableName + "'";
            return Connection.ExecuteDataSet(test);

        }
    }
}
