﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnDBlib
{
    public static class HierarchyHandler
    {
        public static string alu(string t)
        {
            string ret;
            int idx = t.LastIndexOf('_');
            ret = t.Substring(idx + 1);
            ret = new string(ret.Take(6).ToArray());
            return ret; 
        }

        public static void addForeignKey(string table1, string column1, string table2, string column2)
        {

            string sql = "ALTER TABLE " + table1 +
                        " ADD CONSTRAINT " + alu(table1) + "_" + alu(column1) + "_" + alu(table2) + "_" + alu(column2) + " " +
                        "FOREIGN KEY" + "(" + column1 + ") " +
                        "REFERENCES " + table2 + "(" + column2 + ")";
            System.Console.WriteLine(sql);
            Connection.ExecuteNonQuery(sql);
        }
        public static void dropConstraint(string table, string constraint)
        {
            string sql = "ALTER TABLE " + table + " DROP CONSTRAINT " + constraint;
            Connection.ExecuteNonQuery(sql);
        }
        public static DataSet getConstraints(string prefix)
        {
            string test = "SELECT * FROM USER_CONSTRAINTS where constraint_name not like 'SYS%' and TABLE_NAME like '"+prefix+"%'";
            return Connection.ExecuteDataSet(test);
        }

    }
}
