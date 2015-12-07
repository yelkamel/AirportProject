using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Entities
{
    public class Trace
    {
        public DateTime Horodate { get; set; }
        public string TypeTrace { get; set; }
        public string Localisation { get; set; }
        public string Statut { get; set; }
        public string Information { get; set; }
    }
}
