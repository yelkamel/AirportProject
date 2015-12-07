using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Entities
{
    public class Tache
    {
        public int Id { get; set; }
        public DateTime Debut { get; set; }
        public DateTime Fin { get; set; }
        public String Ressource { get; set; }
        public int IdVol { get; set; }
        public TypeTache Type { get; set; }

        public enum TypeTache
        {
            ANT,
            TPS,
            DER,
            RAT,
        }
    }
}
