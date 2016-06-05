using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;

///<summary>
///Namespace contain class required to make connection with Oracle database
/// </summary>
namespace ConnDBlib
{
    ///<summary>
    ///Contain methods required to mangage query log
    /// </summary>
    public static class StatHandler
    {

        /// <summary>
        /// Add one query log
        /// </summary>
        /// <param name="prefixProject">Project prefix</param>
        /// <param name="query">Contain sql query to save in db</param>
        /// <param name="time">Contain time of executed query</param>
        /// <param name="comment">Contain user comment to query</param>
        public static void addQueryLog(string prefixProject, string query, long time, string comment)
        {
            Connection.insert_row("QUERY_HISTORY", "PROJECT_PREFIX, QUERY_TEXT, QUERY_COMMENT, QUERY_TIME", "'" + prefixProject + "', '" + query + "', '" + comment + "', '" + time + "'");
        }

        /// <summary>
        /// Download all query logs from database 
        /// </summary>
        /// <param name="prefixProject">Project prefix</param>
        /// <returns>Returns DataSet with queries logs</returns>
        public static DataSet getFunctions(string prefixProject)
        {
            string test = "select QUERY_TEXT, QUERY_COMMENT, QUERY_TIME from QUERY_HISTORY where PROJECT_PREFIX = '" + prefixProject + "'";
            return Connection.ExecuteDataSet(test);
        }
    }
}
