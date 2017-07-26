using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordiaReWorked
{
    public class Event
    {
        public int id { get; set; }
        public string name { get; set; }
        public int? tank1 { get; set; }
        public int? tank2 { get; set; }
        public int? healer1 { get; set; }
        public int? healer2 { get; set; }
        public int? dps1 { get; set; }
        public int? dps2 { get; set; }
        public int? dps3 { get; set; }
        public int? dps4 { get; set; }
        public int min_ilvl { get; set; }
        public string start_time { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }
}
