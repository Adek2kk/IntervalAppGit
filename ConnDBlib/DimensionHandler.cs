using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;

namespace ConnDBlib
{

    /// <summary>
    /// Class contains all method required to manage dimension tables
    /// </summary>
    public static class DimensionHandler
    {

        /// <summary>
        /// Method add dimension to database
        /// </summary>
        /// <param name="tablename">Contains table name with out project prefix or type</param>
        /// <param name="attributes">Contains columns with restiction  </param>
        /// <param name="type">Project prefix </param>
        public static Result addDimension(string tablename, string attributes, string type)
        {
            //attributes = "id int not null PRIMARY KEY, " + attributes;
            string sql = "Create table " + type + "_DIMENSION_" + tablename + "(" + attributes + ")";
            return Connection.ExecuteNonQuery2(sql);
        }


        /// <summary>
        /// Method download all dimension from database with specific project prefix
        /// </summary>
        /// <param name="prefix">Project prefix </param>
        /// <returns>Returns DataSet witn dimensions</returns>
        public static DataSet getDimensions(string prefix)
        {
            string test = "select table_name as dimensions from dba_tables where table_name like '" + prefix + "_DIMENSION_%' and owner='HURTOWNIE'";
            return  Connection.ExecuteDataSet(test);
        }


        /// <summary>
        /// Method drop dimension table from database
        /// </summary>
        /// <param name="prefix">Project prefix </param>
        /// <param name="tableName">Contains table name with out project prefix or type</param>
        /// <returns>?</returns>
        public static void dropDimension(string prefix, string tableName)
        {
            string test = "DROP table " + prefix + "_DIMENSION_" + tableName + " CASCADE CONSTRAINTS";
            Connection.ExecuteNonQuery(test);
        }


        /// <summary>
        /// Download all columns from database for specific dimension table
        /// </summary>
        /// <param name="tableName">Table name  projectPrefix_DIMENSION_tableName </param>
        /// <returns>Returns DataSet with columns, types and data length</returns>
        public static DataSet getDimensionColumns(string tableName)
        {
            string test = "SELECT  column_name, data_type,data_length FROM USER_TAB_COLUMNS WHERE table_name ='" + tableName + "'";
            return Connection.ExecuteDataSet(test);

        }
    }
}
