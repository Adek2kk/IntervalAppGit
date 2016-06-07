using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;

namespace ConnDBlib
{
    /// <summary>
    /// This class contain all method required to manage function in database
    /// </summary>
    public static class FunctionHandler
    {
        /// <summary>C:\GitHub-Interval\IntervalAppGit\ConnDBlib\FunctionHandler.cs
        /// Generate query to create and populate new function table
        /// </summary>
        /// <param name="tablename">Table name  projectPrefix_FUNCTION_tableName</param>
        /// <param name="key">Column name for key</param>
        /// <param name="eventTime">Column name for time</param>
        /// <param name="eventValue">Column name for event value</param>
        /// <param name="fromString">Contain FROM clause</param>
        /// <param name="whereString">Contain WHERE clause</param>
        /// <param name="isNumber">True if column is number type</param>
        /// <returns>Returns generated SQL query</returns>
        public static string makeQueryAddFunction(string tablename, string key, string eventTime, string eventValue, string fromString, string whereString, bool isNumber)
        {
            //TODO
            string sql = "CREATE TABLE " + tablename
               + " AS SELECT " + key + " as KEY_ID, "
               + eventTime + " AS START_INTERVAL, "
               + " LEAD(" + eventTime + ") OVER(ORDER BY " + key + ", " + eventTime + ") AS END_INTERVAL,";
            if (!isNumber)
            {
                sql = sql + " REGR_SLOPE(" + eventValue + ", TO_NUMBER(TO_CHAR(" + eventTime + ",'YYYYMMDD')) ) OVER(ORDER BY " + key + ", " + eventTime + " ROWS BETWEEN CURRENT ROW AND 1 FOLLOWING) AS SLOPE,"
               + " REGR_INTERCEPT(" + eventValue + ", TO_NUMBER(TO_CHAR(" + eventTime + ",'YYYYMMDD')) ) OVER(ORDER BY " + key + ", " + eventTime + " ROWS BETWEEN CURRENT ROW AND 1 FOLLOWING) AS INTERCEPT";
            }
            else
            {
                sql = sql + " REGR_SLOPE(" + eventValue + ", " + eventTime + ") OVER(ORDER BY " + key + ", " + eventTime + " ROWS BETWEEN CURRENT ROW AND 1 FOLLOWING) AS SLOPE,"
                    + " REGR_INTERCEPT(" + eventValue + ", " + eventTime + ") OVER(ORDER BY " + key + ", " + eventTime + " ROWS BETWEEN CURRENT ROW AND 1 FOLLOWING) AS INTERCEPT";
            }
            sql = sql + " FROM " + fromString;
            if (whereString != "")
                sql = sql + " WHERE " + whereString;
          return sql;
        }


        /// <summary>
        /// Execute query to create and populate new function table
        /// </summary>
        /// <param name="query">SQL query to create </param>
        /// <returns>Returns Result class with error</returns>
        public static Result addFunction(string query)
        {
            return Connection.ExecuteNonQuery2(query);
        }


        /// <summary>
        /// Delete rows without end interval
        /// </summary>
        /// <param name="tableName">Table name  projectPrefix_FUNCTION_tableName </param>
        /// <returns>Returns Result class with error</returns>
        public static Result deleteNullSlope(string tableName)
        {
            string sql = "DELETE FROM " + tableName + " WHERE SLOPE IS NULL";
            return Connection.ExecuteNonQuery2(sql);
        }

        /// <summary>
        /// Download all FUNCTION tables from database with specific project prefix
        /// </summary>
        /// <param name="prefix">Project prefix (project prefix should be uppercase with max 3 characters </param>
        /// <returns>Returns DataSet with function tables name</returns>
        public static DataSet getFunctions(string prefix)
        {
            string test = "select table_name as facts from dba_tables where table_name like '" + prefix + "_FUNCTION_%' and owner='HURTOWNIE'";
            return Connection.ExecuteDataSet(test);
        }

        /// <summary>
        /// Check if function exist in database
        /// </summary>
        /// <param name="tableName">Table name  projectPrefix_FUNCTION_tableName </param>
        /// <returns>Returns SQL code</returns>
        public static DataSet checkFunctionExist(string tableName)
        {
            string test = "select table_name as facts from dba_tables where table_name =" + tableName + " and owner='HURTOWNIE'";
            return Connection.ExecuteDataSet(test);
        }

        /// <summary>
        /// Drop selected function table
        /// </summary>
        /// <param name="tableName">Table name  projectPrefix_FUNCTION_tableName </param>
        public static void dropFunction(string tableName)
        {
            string test = "DROP table " + tableName + " CASCADE CONSTRAINTS";
            Connection.ExecuteNonQuery(test);
        }

        /// <summary>
        /// Download all columns from database for specific function table
        /// </summary>
        /// <param name="tableName">Table name  projectPrefix_FUNCTION_tableName </param>
        /// <returns>Returns DataSet with columns, types and data length</returns>
        public static DataSet getFunctionColumns(string tableName)
        {
            string test = "SELECT  column_name, data_type,data_length FROM USER_TAB_COLUMNS WHERE table_name ='" + tableName + "'";
            return Connection.ExecuteDataSet(test);

        }

        /// <summary>
        /// Download first one hundred rows form database for specific function table
        /// </summary>
        /// <param name="tableName">Table name  projectPrefix_FUNCTION_tableName </param>
        /// <returns>Returns DataSet with 100 rows</returns>
        public static DataSet getFirstHundredFunction(string tableName)
        {
            string test = "SELECT * FROM " + tableName + " where rownum < 100";
            System.Console.WriteLine(test);
            return Connection.ExecuteDataSet(test);
        }

    }
}
