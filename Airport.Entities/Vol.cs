using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Airport.Entities
{
    /// <summary>
    /// Cette classe definie un vol
    /// </summary>
    public class Vol
    {
        public int idVol { get; set; }
        /// <summary>
        /// Nom de la compagnie
        /// </summary>
        public string Cie {get; set; }
        public string Ligne{get;set;}
        public int JourExploitation { get; set; }
        public StatutVol Statut { get; set; }
        public string TypeAvion { get; set; }
        public string Immatriculation { get; set; }
        public string Parking { get; set; }
        public DateTime DernierHoraire { get; set; }
        public List<string> Itineraire { get; set; }
        public List<string> Banques { get; set; }

//       public Chronogramme Chronogramme { get; set; }
    }

    public class CriteresVol
    {

    }
   
}
