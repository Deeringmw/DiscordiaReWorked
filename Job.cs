using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordiaReWorked
{
    public class Job
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public string job { get; set; }
        public string name { get; set; }
        public string avatar { get; set; }
        public int item_level { get; set; }
        public string updated_at { get; set; }
        public string created_at { get; set; }
    }
}
