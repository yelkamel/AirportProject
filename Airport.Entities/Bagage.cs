using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Entities
{
    public class Bagage
    {
        public int Id_bagage { get; set; }
        public string CodeIata { get; set; }
        public int IdTache { get; set; }
        public string Compagnie { get; set; }
        public string Ligne { get; set; }
        public DateTime DateCreation { get; set; }
        public int status_ejection_surete { get; set; }
        public string cle_global { get; set; }
        public string jour_exploitation { get; set; }
    }

    public class CriteresBagage
    {

    }
}
