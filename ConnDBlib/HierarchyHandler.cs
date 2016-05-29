using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnDBlib
{
    /// <summary>
    /// Class contains all method required to manage Hierarchy and Relations
    /// </summary>
    public static class HierarchyHandler
    {
        /// <summary>
        /// Method finds last '_' character and substring text after it. 
        /// </summary>
        /// <param name="t">unknown</param>
        /// <returns>Returns string after substring</returns>
        public static string alu(string t)
        {
            string ret;
            int idx = t.LastIndexOf('_');
            ret = t.Substring(idx + 1);
            ret = new string(ret.Take(6).ToArray());
            return ret; 
        }

        /// <summary>
        /// Add new foreign key between two tables
        /// </summary>
        /// <param name="table1">First table name</param>
        /// <param name="column1">First column name</param>
        /// <param name="table2">Second table name</param>
        /// <param name="column2">Second column name</param>
        public static void addForeignKey(string table1, string column1, string table2, string column2)
        {

            string sql = "ALTER TABLE " + table1 +
                        " ADD CONSTRAINT " + alu(table1) + "_" + alu(column1) + "_" + alu(table2) + "_" + alu(column2) + " " +
                        "FOREIGN KEY" + "(" + column1 + ") " +
                        "REFERENCES " + table2 + "(" + column2 + ")";
            System.Console.WriteLine(sql);
            Connection.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// Drop selected constriant
        /// </summary>
        /// <param name="table">Table name  projectPrefix_FUNCTION_tableNamex </param>
        /// <param name="constraint">Constrait name</param>
        public static void dropConstraint(string table, string constraint)
        {
            string sql = "ALTER TABLE " + table + " DROP CONSTRAINT " + constraint;
            Connection.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// Download all constraints from database with specific prefix ant type
        /// </summary>
        /// <param name="prefix">Project prefix </param>
        /// <param name="typ">Constraint type</param>
        public static DataSet getConstraints(string prefix,string typ)
        {
            string test = "SELECT * FROM USER_CONSTRAINTS where constraint_name not like 'SYS%' and TABLE_NAME like '"+prefix+"_"+typ+"%'";
            return Connection.ExecuteDataSet(test);
        }

    }
}
