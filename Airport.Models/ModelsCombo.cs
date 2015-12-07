using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Airport.Entities;
using System.Data;
using System.Data.SqlClient;

namespace Airport.Models
{
    public class ModelsCombo
    {
        private string connectionString = "Data Source=RAMATOU\\SQLEXPRESS;Initial Catalog=CDG1_EXPLOIT_ADP;Integrated Security=True";
        private string selectVols = "select ID_VOL, LIG from VOL";
        private string list_sortie_tri = "select ID_RES, NOM_RES  from RES_SORTIE_TRI";
        private string list_typ_task = "select ID_TACHE, STATUT_TEMPOREL from TAC_GRP_BAGS";

        //recupere liste des vols
        public List<vol_search> SelectVols()
        {
            List<vol_search> res = new List<vol_search>();
            using (SqlConnection cnx = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(selectVols, cnx);
                    cnx.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        vol_search vol1 = new vol_search();
                        vol1.id = 1;
                        vol1.ligne = "Tous";
                        res.Add(vol1);
                        while (sdr.Read())
                        {
                            vol_search vol = new vol_search();
                            vol.id = (int)sdr["ID_VOL"];
                            vol.ligne = sdr["LIG"] as string;
                            res.Add(vol);
                        }
                    }
                }
                catch (Exception e)
                {
                    e.GetBaseException();
                }
            }
            return res;
        }

          /*recupere liste de sortie de tri*/
        public List<SortieTri> SelectSortieTri()
        {
            List<SortieTri> res = new List<SortieTri>();
            using (SqlConnection cnx = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(list_sortie_tri, cnx);
                    cnx.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        SortieTri sort = new SortieTri();
                        sort.idSortie = 1;
                        sort.nom_res = "Tous";
                        res.Add(sort);
                        while (sdr.Read())
                        {
                            SortieTri sortie_tri = new SortieTri();
                            sortie_tri.idSortie = (int)sdr["ID_RES"];
                            sortie_tri.nom_res = sdr["NOM_RES"] as string;
                            res.Add(sortie_tri);
                        }
                    }
                }
                catch (Exception e)
                {
                    e.GetBaseException();
                }
            }
            return res;

        }


        /*recupere liste des taches*/
        public List<typeTaskBag> SelectTypeTask()
        {
            List<typeTaskBag> res = new List<typeTaskBag>();
            using (SqlConnection cnx = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(list_typ_task, cnx);
                    cnx.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        typeTaskBag sortie_task1 = new typeTaskBag();
                        sortie_task1.idtask = 1;
                        sortie_task1.statut_temporel = "Tous";
                        res.Add(sortie_task1);
                        while (sdr.Read())
                        {
                            typeTaskBag sortie_task = new typeTaskBag();
                            sortie_task.idtask = (int)sdr["ID_TACHE"];
                            sortie_task.statut_temporel = sdr["STATUT_TEMPOREL"] as string;
                            res.Add(sortie_task);
                        }
                    }
                }
                catch (Exception e)
                {
                    e.GetBaseException();
                }
            }
            return res;
        }


    }


    
 
}

