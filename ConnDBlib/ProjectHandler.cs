using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnDBlib
{
    /// <summary>
    /// Class contains methods to manage MAIN_PROJECTS table
    /// </summary>
    public static class ProjectHandler
    {
        /// <summary>
        /// Check if function exist in database
        /// </summary>
        /// <param name="attributes">String contain project name and project prefix </param>
        public static void newProject(string attributes)
        {
            string sql = "INSERT INTO MAIN_PROJECTS (NAME, PREFIX) VALUES " + " (" + attributes + ")";
            Connection.ExecuteNonQuery(sql);
        }
    }
}
