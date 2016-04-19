using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnDBlib
{
    public static class ProjectHandler
    {

        public static void newProject(string attributes)
        {
            string sql = "INSERT INTO MAIN_PROJECTS (NAME, PREFIX) VALUES " + " (" + attributes + ")";
            Connection.ExecuteNonQuery(sql);
        }
    }
}
