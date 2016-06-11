using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalApp
{
    ///<summary>
    ///Help class to hold inforamtion about query log
    /// </summary>
    public class StatHolder
    {
        ///<summary>
        ///Id
        /// </summary>
        public int id_query { get; set; }
        ///<summary>
        ///Saved query
        /// </summary>
        public string sql { get; set; }
        ///<summary>
        ///Execution time
        /// </summary>
        public long time { get; set; }
        ///<summary>
        ///Saved comment
        /// </summary>
        public string comment { get; set; }
    }
}
