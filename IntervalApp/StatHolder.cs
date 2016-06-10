using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalApp
{
    public class StatHolder
    {
        public int id_query { get; set; }
        public string sql { get; set; }
        public long time { get; set; }
        public string comment { get; set; }
    }
}
