using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;

namespace ConnDBlib
{
    public static class DimensionHandler
    {
        public static void addDimension(string tablename, string attributes, string type)
        {
            //attributes = "id int not null PRIMARY KEY, " + attributes;
            string sql = "Create table " + type + "_DIMENSION_" + tablename + "(" + attributes + ")";
            Connection.ExecuteNonQuery(sql);
        }

        public static DataSet getDimensions(string prefix)
        {
            string test = "select table_name as dimensions from dba_tables where table_name like '" + prefix + "_DIMENSION_%' and owner='HURTOWNIE'";
            return  Connection.ExecuteDataSet(test);
        }

        public static void dropDimension(string prefix, string tableName)
        {
            string test = "DROP table " + prefix + "_DIMENSION_" + tableName + " CASCADE CONSTRAINTS";
            Connection.ExecuteNonQuery(test);
        }

        public static DataSet getDimensionColumns(string tableName)
        {
            string test = "SELECT  column_name, data_type,data_length FROM USER_TAB_COLUMNS WHERE table_name ='" + tableName + "'";
            return Connection.ExecuteDataSet(test);

        }
    }
}
