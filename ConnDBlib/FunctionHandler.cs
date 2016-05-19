using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;

namespace ConnDBlib
{
    public static class FunctionHandler
    {
        public static void addFunction(string tablename, string attributes, string type)
        {
            //TODO
            string sql = "Create table " + type + "_FUNCTION_" + tablename + "(" + attributes + ")";
            Connection.ExecuteNonQuery(sql);
        }

        public static DataSet getFunctions(string prefix)
        {
            string test = "select table_name as facts from dba_tables where table_name like '" + prefix + "_FUNCTION_%' and owner='HURTOWNIE'";
            return Connection.ExecuteDataSet(test);
        }

        public static void dropFunction(string tableName)
        {
            string test = "DROP table " + tableName + " CASCADE CONSTRAINTS";
            Connection.ExecuteNonQuery(test);
        }

        public static DataSet getFunctionColumns(string tableName)
        {
            string test = "SELECT  column_name, data_type,data_length FROM USER_TAB_COLUMNS WHERE table_name ='" + tableName + "'";
            return Connection.ExecuteDataSet(test);

        }

        public static DataSet getFirstHundredFunction(string tableName)
        {
            string test = "SELECT * FROM " + tableName + " where rownum < 100";
            System.Console.WriteLine(test);
            return Connection.ExecuteDataSet(test);
        }

    }
}
