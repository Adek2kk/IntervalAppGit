using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;

namespace ConnDBlib
{

    /// <summary>
    /// Class contains all method required to manage fact tables
    /// </summary>
    public static class FactHandler
    {

        /// <summary>
        /// Method add fact to database
        /// </summary>
        /// <param name="tablename">Contains table name with out project prefix or type</param>
        /// <param name="attributes">Contains columns with restiction  </param>
        /// <param name="type">Project prefix </param>
        public static void addFact(string tablename, string attributes, string type)
        {
            //attributes = "id int not null PRIMARY KEY, " + attributes;
            string sql = "Create table " + type + "_FACT_" + tablename + "(" + attributes + ")";
            Connection.ExecuteNonQuery(sql);
        }


        /// <summary>
        /// Method download all fact from database with specific project prefix
        /// </summary>
        /// <param name="prefix">Project prefix </param>
        /// <returns>Returns DataSet witn facts</returns>
        public static DataSet getFacts(string prefix)
        {
            string test = "select table_name as facts from dba_tables where table_name like '" + prefix + "_FACT_%' and owner='HURTOWNIE'";
            return Connection.ExecuteDataSet(test);
        }


        /// <summary>
        /// Method drop fact table from database
        /// </summary>
        /// <param name="prefix">Project prefix </param>
        /// <param name="tableName">Contains table name with out project prefix or type</param>
        public static void dropFact(string prefix, string tableName)
        {
            string test = "DROP table " + prefix + "_FACT_" + tableName + " CASCADE CONSTRAINTS";
            Connection.ExecuteNonQuery(test);
        }


        /// <summary>
        /// Download all columns from database for specific fact table
        /// </summary>
        /// <param name="tableName">Table name  projectPrefix_FACT_tableName </param>
        /// <returns>Returns DataSet with columns, types and data length</returns>
        public static DataSet getFactColumns(string tableName)
        {
            string test = "SELECT  column_name, data_type,data_length FROM USER_TAB_COLUMNS WHERE table_name ='" + tableName + "'";
            return Connection.ExecuteDataSet(test);

        }
    }
}
