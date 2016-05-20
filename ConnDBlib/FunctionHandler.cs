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
        public static string makeQueryAddFunction(string tablename, string key, string eventTime, string eventValue, string fromString, string whereString)
        {
            //TODO
            string sql = "CREATE TABLE " + tablename 
               + " AS SELECT " + key + " as KEY_ID, " 
               + eventTime + " AS START_INTERVAL, " 
               + " LEAD(" + eventTime + ") OVER(ORDER BY " + key + ", " + eventTime + ") AS END_INTERVAL,"
               + " REGR_SLOPE(" + eventValue + ", TO_NUMBER(TO_CHAR(" + eventTime + ",'YYYYMMDD')) ) OVER(ORDER BY " + key + ", " + eventTime + " ROWS BETWEEN CURRENT ROW AND 1 FOLLOWING) AS SLOPE,"
               + " REGR_INTERCEPT(" + eventValue + ", TO_NUMBER(TO_CHAR(" + eventTime + ",'YYYYMMDD')) ) OVER(ORDER BY " + key + ", " + eventTime + " ROWS BETWEEN CURRENT ROW AND 1 FOLLOWING) AS INTERCEPT"
               + " FROM " + fromString;
            if (whereString != "")
                sql = sql + " WHERE " + whereString;
          return sql;
        }

        public static int addFunction(string query, string tableName)
        {
            Connection.ExecuteNonQuery(query);
            string sql = "DELETE FROM " + tableName + "WHERE SLOPE IS NULL";
            return Connection.ExecuteNonQuery(sql);
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
