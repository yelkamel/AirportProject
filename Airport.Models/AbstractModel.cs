using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Airport.Entities;

namespace Airport.Models
{
    public abstract class AbstractModel
    {
        #region Vols
        abstract public Vol GetDetailsVol(int idVol);
        abstract public void AddVol(Vol vol);
        abstract public List<Vol> GetVols();
        abstract public List<Vol> GetVols(string cie, string line, DateTime debut, DateTime fin);
        abstract public List<Vol> GetVols(CriteresVol criteres);
        abstract public Dictionary<string, string> SelectVolsFields();
        abstract public List<Bagage> GetBagages();
        //abstract public List<Bagage> GetBagages(CriteresBagage critere);
        #endregion
    }
}
