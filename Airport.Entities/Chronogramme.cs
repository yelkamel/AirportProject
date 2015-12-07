using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Entities
{
    public class Chronogramme
    {
        public string Name { get; set; }
        public int Anticipe { get; set; }
        public int ATemps { get; set; }
        public int DerniereMinutes { get; set; }
        public int Rate { get; set; }
    }
}
