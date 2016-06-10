using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnDBlib
{

    /// <summary>
    /// Contains all method to startup app
    /// </summary>
    public static class InitHandler
    {


        /// <summary>
        /// Add reqiured tables
        /// </summary>
        /// <returns>
        /// Information if everything goes ok.
        /// </returns>
        public static string addTables()
        {
            Result result;
            //attributes = "id int not null PRIMARY KEY, " + attributes;
            string sql =    "CREATE TABLE QUERY_HISTORY(ID_QUERY NUMBER NOT NULL," +
                            " PROJECT_PREFIX VARCHAR2(3) NOT NULL ENABLE," +
                            " QUERY_TEXT VARCHAR2(4000 BYTE) NOT NULL ENABLE," +
                            " QUERY_COMMENT VARCHAR2(4000 BYTE) NOT NULL ENABLE," +
                            " QUERY_TIME NUMBER NOT NULL ENABLE)";

            
            result = Connection.ExecuteNonQuery2(sql);
            if (result.errormsg != "OK")
                return "History problem";

            string sql2 =   "CREATE TABLE MAIN_PROJECTS" +
                            " (NAME VARCHAR2(50 BYTE) NOT NULL ENABLE," +
                            " PREFIX VARCHAR2(3 BYTE) NOT NULL ENABLE," +
                            " CONSTRAINT MAIN_PROJECTS_PK PRIMARY KEY(NAME))";
            result = Connection.ExecuteNonQuery2(sql2);
            if (result.errormsg != "OK")
                return "Projects problem";
            return "OK";
        }

        /// <summary>
        /// Add reqiured sequences
        /// </summary>
        /// <returns>
        /// Information if everything goes ok.
        /// </returns>
        public static string addSequences()
        {
            Result result;
            //attributes = "id int not null PRIMARY KEY, " + attributes;
            string sql = "CREATE SEQUENCE query_id";
            result = Connection.ExecuteNonQuery2(sql);
            if (result.errormsg != "OK")
                return "Sequence problem";
            return "OK";
        }

        /// <summary>
        /// Add reqiured triggers
        /// </summary>
        /// <returns>
        /// Information if everything goes ok.
        /// </returns>
        public static string addTriggers()
        {
            Result result;
            //attributes = "id int not null PRIMARY KEY, " + attributes;
            string sql =    "CREATE TRIGGER query_log_id " +
                            " BEFORE INSERT ON query_history " +
                            " FOR EACH ROW " +
                            " BEGIN " +
                            " SELECT query_id.NEXTVAL " +
                            " INTO   :new.id_query " +
                            " FROM dual; " +
                            " END;";
            result = Connection.ExecuteNonQuery2(sql);
            if (result.errormsg != "OK")
                return "Triggers problem";
            return "OK";
        }

        /// <summary>
        /// Add test project
        /// </summary>
        public static void addTestProject()
        {
            ProjectHandler.newProject("'TEST','TES'");
        }


        /// <summary>
        /// Check if project was initialized.
        /// </summary>
        /// <returns>
        /// Information if everything goes ok.
        /// </returns>
        public static string checkIfInitDone()
        {
            Result result;
            //attributes = "id int not null PRIMARY KEY, " + attributes;
            string sql = "SELECT * FROM main_projects";
            result = Connection.ExecuteNonQuery2(sql);
            Console.WriteLine(result.errormsg);
            if (result.errormsg != "OK")
                return "Nope";
            string sql1 = "SELECT * FROM query_history";
            result = Connection.ExecuteNonQuery2(sql1);
            Console.WriteLine(result.errormsg);
            if (result.errormsg != "OK")
                return "Nope";
            return "OK";
        }

    }
}
