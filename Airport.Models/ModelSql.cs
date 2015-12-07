using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Airport.Entities;


namespace Airport.Models
{
    public class ModelSql : AbstractModel
    {
        private string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=CDG1_EXPLOIT_ADP;Integrated Security=True";
        private string selectVols = "select e.ID_EMPORT, cie.CODE_CIE, e.LIG, e.JEX, e.STA, e.TYP," 
            + "e.IMM, e.PKG, e.DHC from EMPORT_VOLS e inner join cie on cie.ID_CIE = e.ID_CIE "
            + " where e.dhc between @dateDebut and DATEADD(d, 1, @dateDebut)";
        private string select_vol_properties = "SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME='VOL' ";
    

        override public Dictionary<string, string> SelectVolsFields()
        {
            Dictionary<string, string> res = new Dictionary<string, string>();
            using (SqlConnection cnx = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(select_vol_properties, cnx);
                    cnx.Open();
                   
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            if(sdr[3].ToString() == "ID_VOL")
                                res.Add(sdr[3].ToString(), "Identifiant du vol");
                            if(sdr[3].ToString() == "ID_CIE")
                                  res.Add(sdr[3].ToString(), "Cie");
                            if(sdr[3].ToString() == "LIG")
                                  res.Add(sdr[3].ToString(), "Ligne");
                            if(sdr[3].ToString() == "JEX")
                                  res.Add(sdr[3].ToString(), "Jour d'exploitation");
                             if(sdr[3].ToString() == "QRF")
                                  res.Add(sdr[3].ToString(), "retour au sol");
                            if(sdr[3].ToString() == "DHC")
                                  res.Add(sdr[3].ToString(), "DHC");
                            if(sdr[3].ToString() == "SPK")
                                  res.Add(sdr[3].ToString(), "SPK");
                            if(sdr[3].ToString() == "STA")
                                  res.Add(sdr[3].ToString(), "STA");
                            if(sdr[3].ToString() == "DOA")
                                  res.Add(sdr[3].ToString(), "DOA");
                            if(sdr[3].ToString() == "PKG")
                                res.Add(sdr[3].ToString(), "Parking");
                            if(sdr[3].ToString() == "EST_VOL_DEP")
                                  res.Add(sdr[3].ToString(), "Est vol de depart");
                            if(sdr[3].ToString() == "TDH")
                                res.Add(sdr[3].ToString(), "TDH");
                            if(sdr[3].ToString() == "NAT")
                                res.Add(sdr[3].ToString(), "NAT");
                            if (sdr[3].ToString() == "REP")
                                res.Add(sdr[3].ToString(), "REP");
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
        
        
        public List<Entities.Vol> SelectVols()
        {
            List<Vol> res = new List<Vol>();
            using (SqlConnection cnx = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(selectVols, cnx);
                    cmd.Parameters.Add("@dateDebut", SqlDbType.DateTime);
                    cmd.Parameters[0].Value = "20/09/2013";
                    cnx.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            Vol vol = new Vol();
                            vol.Banques = null;
                            vol.Cie = sdr[1].ToString();
                            vol.DernierHoraire = sdr.GetDateTime(8);
                            vol.idVol = (int)sdr["id_emport"];
                            vol.Immatriculation = sdr[6] is DBNull? string.Empty: sdr[6].ToString();
                            vol.Itineraire = null;
                            vol.JourExploitation = Int32.Parse(sdr["jex"].ToString());
                            vol.Ligne = sdr["lig"] as string;
                            vol.Parking = sdr["pkg"] is DBNull ? string.Empty : sdr["pkg"].ToString();
                            res.Add(vol);
                        }
                    }
                }
                catch(Exception e)
                {
                    e.GetBaseException();
                }
            }
            return res;

        }

        public List<Vol> GetVolBagage(int Id)
        {
            List<Vol> res = new List<Vol>();
            string vol_associe_request = "Select * from VOL v INNER JOIN BSM b ON b.ID_VOL = v.ID_VOL INNER JOIN " +
             " OCCURENCE_BAGAGE og ON og.ID_BSM = b.ID_BSM WHERE og.ID_BAGAGE = @id";
            using (SqlConnection cnx = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(vol_associe_request, cnx);
                    SqlParameter id_bagage = new SqlParameter("@id", SqlDbType.Int);
                    id_bagage.Value = Id;
                    cmd.Parameters.Add(id_bagage);
                    cnx.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            Vol vol = new Vol();
                            if (!DBNull.Value.Equals(sdr["ID_VOL"]))
                            {
                                vol.idVol = (int)sdr["ID_VOL"];             
                            }
                            if (!DBNull.Value.Equals(sdr["ID_CIE"]))
                            {
                                vol.Cie = sdr["ID_CIE"].ToString();
                            }
                            if (!DBNull.Value.Equals(sdr["LIG"]))
                            {
                                vol.Ligne = sdr["LIG"].ToString();
                            }
                            if (!DBNull.Value.Equals(sdr["JEX"]))
                            {
                                vol.JourExploitation = (int)sdr["JEX"];
                            }
                            if (!DBNull.Value.Equals(sdr["DHC"]))
                            {
                                vol.DernierHoraire = Convert.ToDateTime(sdr["DHC"]);
                            }
                            if (!DBNull.Value.Equals(sdr["PKG"]))
                            {
                                vol.Parking = sdr["PGK"].ToString();
                            }

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

        public Bagage GetBagageId(int Id)
        {
            Bagage bagage = new Bagage();
            string request = "select * from OCCURENCE_BAGAGE where ID_BAGAGE = @id ";
            using (SqlConnection cnx = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(request, cnx);
                    SqlParameter id_bagage = new SqlParameter("@id", SqlDbType.Int);
                    id_bagage.Value = Id;
                    cmd.Parameters.Add(id_bagage);
                    cnx.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            if (!DBNull.Value.Equals(sdr["ID_BAGAGE"]))
                            {
                                bagage.Id_bagage = (int)sdr["ID_BAGAGE"];
                            }
                            if (!DBNull.Value.Equals(sdr["CODE_IATA"]))
                            {
                                bagage.CodeIata = sdr["CODE_IATA"].ToString();
                            }

                            if (!DBNull.Value.Equals(sdr["CIE"]))
                            {
                                bagage.Compagnie = sdr["CIE"].ToString();
                            }

                            if (!DBNull.Value.Equals(sdr["LIG"]))
                            {
                                bagage.Ligne = sdr["LIG"].ToString();
                            }

                            if (!DBNull.Value.Equals(sdr["TENTREE_TRI"]))
                            {
                                bagage.DateCreation = (DateTime)sdr["TENTREE_TRI"];
                            }

                            if (!DBNull.Value.Equals(sdr["ID_BSM"]))
                            {
                                bagage.IdTache = (int)sdr["ID_BSM"];
                            }

                            if (!DBNull.Value.Equals(sdr["JEX"]))
                            {
                                bagage.jour_exploitation = sdr["JEX"].ToString();
                            }

                            if (!DBNull.Value.Equals(sdr["STATUT_SURETE_EJECTION"]))
                            {
                                bagage.status_ejection_surete = (int)sdr["STATUT_SURETE_EJECTION"];
                            }

                            if (!DBNull.Value.Equals(sdr["CLEF_GLOBALE"]))
                            {
                                bagage.cle_global = sdr["CLEF_GLOBALE"].ToString();
                            } 
                        }
                    }
                }
                catch (Exception e)
                {
                    e.GetBaseException();
                }
            }
            return bagage;
        }


        //get tracabilites bagage
        public List<Trace>  GetTracabilite(int id_bagage)
        {
            List<Trace> trace_bagage = new List<Trace>();
            string request = "select * from TRACE_BAGAGE where ID_BAGAGE = @id ";
            using (SqlConnection cnx = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(request, cnx);
                    SqlParameter id = new SqlParameter("@id", SqlDbType.Int);
                    id.Value = id_bagage;
                    cmd.Parameters.Add(id);
                    cnx.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            Trace trace = new Trace();
                            if (!DBNull.Value.Equals(sdr["TTRACE"]))
                            {
                                trace.Horodate = Convert.ToDateTime(sdr["TTRACE"]);
                            }
                            if (!DBNull.Value.Equals(sdr["INFO"]))
                            {
                                trace.Information = sdr["INFO"].ToString();
                            }

                            if (!DBNull.Value.Equals(sdr["STATUT"]))
                            {
                                trace.Statut = sdr["STATUT"].ToString();
                            }

                            if (!DBNull.Value.Equals(sdr["TYPE"]))
                            {
                                trace.TypeTrace = sdr["TYPE"].ToString();
                            }
                            Console.WriteLine(trace.Localisation);
                            trace_bagage.Add(trace);
                           
                        }
                    }
                }
                catch (Exception e)
                {
                    e.GetBaseException();
                }
            }
            return trace_bagage;
        }

        //selection de tous les bagage sans criteres de recherche
        override public List<Bagage> GetBagages()
        {
            List<Bagage> res = new List<Bagage>();
            string request = "select * from OCCURENCE_BAGAGE ";
            using (SqlConnection cnx = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(request, cnx);
                    cnx.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            Bagage bagage = new Bagage();
                            if (!DBNull.Value.Equals(sdr["ID_BAGAGE"]))
                            {
                                bagage.Id_bagage = (int)sdr["ID_BAGAGE"];
                            }
                            if (!DBNull.Value.Equals(sdr["CODE_IATA"]))
                            {
                                bagage.CodeIata = sdr["CODE_IATA"].ToString();
                            }

                            if (!DBNull.Value.Equals(sdr["CIE"]))
                            {
                                bagage.Compagnie = sdr["CIE"].ToString();
                            }

                            if (!DBNull.Value.Equals(sdr["LIG"]))
                            {
                                bagage.Ligne = sdr["LIG"].ToString();
                            }

                            if (!DBNull.Value.Equals(sdr["TENTREE_TRI"]))
                            {
                                bagage.DateCreation = (DateTime)sdr["TENTREE_TRI"];
                            }

                            if (!DBNull.Value.Equals(sdr["ID_BSM"]))
                            {
                                bagage.IdTache = (int)sdr["ID_BSM"];
                            }

                            if (!DBNull.Value.Equals(sdr["JEX"]))
                            {
                                bagage.jour_exploitation = sdr["JEX"].ToString();
                            }

                            if (!DBNull.Value.Equals(sdr["STATUT_SURETE_EJECTION"]))
                            {
                                bagage.status_ejection_surete = (int)sdr["STATUT_SURETE_EJECTION"];
                            }

                            if (!DBNull.Value.Equals(sdr["CLEF_GLOBALE"]))
                            {
                                bagage.cle_global = sdr["CLEF_GLOBALE"].ToString();
                            }
                            res.Add(bagage);
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

        
        /*select bagages  with parameters*/

        public List<Bagage> SelectBagagesParam(typeTaskBag type_task_,
            vol_search vol_id, SortieTri tri, DateTime depart_date, string ligne, string compagnie, string codeIata)
        {
            List<Bagage> bagage_list = new List<Bagage>();
            using (SqlConnection cnx = new SqlConnection(connectionString))
            {
               try
               {
                    string request = "select * from OCCURENCE_BAGAGE ";
                    if (compagnie != "")
                    {
                        request += " where CIE = @compagnie ";
                    }
                   if (depart_date != null)
                    {
                        if (compagnie == "")
                            request += "where ";
                        else
                            request += "and ";
                        request += " TENTREE_TRI between @dateDebut and DATEADD(d, 1, @dateDebut) ";          
                    } 
                    if (codeIata != "")
                    {
                        if (compagnie == "" && depart_date == null)
                            request += "where ";
                        else
                            request += "and ";
                        request += " CODE_IATA = @codeIata ";
                    }

                    if (ligne != "")
                    {
                        if (compagnie == "" && depart_date == null && codeIata == null)
                            request += "where ";
                        else
                            request += "and ";
                        request += " LIG = @ligne ";
                    } 

                    SqlCommand cmd = new SqlCommand(request, cnx);

                   if (compagnie != "")
                    {
                        SqlParameter compagnie_ = new SqlParameter("@compagnie", SqlDbType.VarChar);
                        compagnie_.Value = compagnie ;
                        cmd.Parameters.Add(compagnie_);
                    }
                    if (codeIata != "")
                    {
                        SqlParameter code_Iata = new SqlParameter("@codeIata", SqlDbType.VarChar);
                        code_Iata.Value = codeIata ;
                        cmd.Parameters.Add(code_Iata);
                    }

                    if (ligne != "")
                    {
                        SqlParameter ligne_param = new SqlParameter("@ligne", SqlDbType.Int);
                        ligne_param.Value = ligne;
                        cmd.Parameters.Add(ligne_param);
                    }
          
                    SqlParameter date_Debut = new SqlParameter("@dateDebut", SqlDbType.DateTime);
                    date_Debut.Value = depart_date;
                    cmd.Parameters.Add(date_Debut);


                    cnx.Open();
                    
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            /*on verifie d'abord si la valeur du champs n'est pas vide*/
                            Bagage bagage = new Bagage();
                            if (!DBNull.Value.Equals(sdr["ID_BAGAGE"]))
                            {
                                bagage.Id_bagage = (int) sdr["ID_BAGAGE"];
                            }
                            if (!DBNull.Value.Equals(sdr["CODE_IATA"]))
                            {
                                bagage.CodeIata = sdr["CODE_IATA"].ToString();
                            }

                            if (!DBNull.Value.Equals(sdr["CIE"]))
                            {
                                bagage.Compagnie = sdr["CIE"].ToString();
                            }

                            if (!DBNull.Value.Equals(sdr["LIG"]))
                            {
                                bagage.Ligne = sdr["LIG"].ToString();
                            }

                            if (!DBNull.Value.Equals(sdr["TENTREE_TRI"]))
                            {
                                bagage.DateCreation = (DateTime)sdr["TENTREE_TRI"];
                            }
                            
                            if(!DBNull.Value.Equals(sdr["ID_BSM"]))
                            {
                                bagage.IdTache = (int)sdr["ID_BSM"];
                            }

                            if (!DBNull.Value.Equals(sdr["JEX"]))
                            {
                                bagage.jour_exploitation = sdr["JEX"].ToString();
                            }

                            if (!DBNull.Value.Equals(sdr["STATUT_SURETE_EJECTION"]))
                            {
                                bagage.status_ejection_surete = (int) sdr["STATUT_SURETE_EJECTION"];
                            }

                            if (!DBNull.Value.Equals(sdr["CLEF_GLOBALE"]))
                            {
                                bagage.cle_global = sdr["CLEF_GLOBALE"].ToString();
                            }

                            bagage_list.Add(bagage); 
                        }
                        
                    }
                }
                catch (Exception e)
                {
                    e.GetBaseException();
                }   
            }
            return bagage_list;
        }
        override public List<Vol> GetVols(CriteresVol criteres)
        {
            return null;
        }

        override public List<Vol> GetVols(string cie, string line, DateTime debut, DateTime fin)
        {
            return null;
        }

        override public void AddVol(Vol vol)
        {

        }
        override public List<Vol> GetVols()
        {
            return null;
        }
        override public Vol GetDetailsVol(int idVol)
        {
            return null;
        }
    }
}
