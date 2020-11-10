#region Ressources extérieures

using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

#endregion

namespace Gestion_Hopital.Model
{
 /// <summary>
 /// Couche d'accès aux données (Data Access Layer)
 /// </summary>
 public class A_t_chambres : ADBase
 {
  #region Constructeurs
  public A_t_chambres(string sChaineConnexion)
  	: base(sChaineConnexion)
  { }
  #endregion
  public int Ajouter(int NomCha, int QuantiteLits, string TypeCha, int EtageCha, string ServiceCha)
  {
   CreerCommande("Ajoutert_chambres");
   int res = 0;
   Commande.Parameters.Add("IDCha", SqlDbType.Int);
   Direction("IDCha", ParameterDirection.Output);
   Commande.Parameters.AddWithValue("@NomCha", NomCha);
   Commande.Parameters.AddWithValue("@QuantiteLits", QuantiteLits);
   Commande.Parameters.AddWithValue("@TypeCha", TypeCha);
   Commande.Parameters.AddWithValue("@EtageCha", EtageCha);
   Commande.Parameters.AddWithValue("@ServiceCha", ServiceCha);
   Commande.Connection.Open();
   Commande.ExecuteNonQuery();
   res = int.Parse(LireParametre("IDCha"));
   Commande.Connection.Close();
   return res;
  }
  public int Modifier(int IDCha, int NomCha, int QuantiteLits, string TypeCha, int EtageCha, string ServiceCha)
  {
   CreerCommande("Modifiert_chambres");
   int res = 0;
   Commande.Parameters.AddWithValue("@IDCha", IDCha);
   Commande.Parameters.AddWithValue("@NomCha", NomCha);
   Commande.Parameters.AddWithValue("@QuantiteLits", QuantiteLits);
   Commande.Parameters.AddWithValue("@TypeCha", TypeCha);
   Commande.Parameters.AddWithValue("@EtageCha", EtageCha);
   Commande.Parameters.AddWithValue("@ServiceCha", ServiceCha);
   Commande.Connection.Open();
   Commande.ExecuteNonQuery();
   Commande.Connection.Close();
   return res;
  }
  public List<C_t_chambres> Lire(string Index)
  {
   CreerCommande("Selectionnert_chambres");
   Commande.Parameters.AddWithValue("@Index", Index);
   Commande.Connection.Open();
   SqlDataReader dr = Commande.ExecuteReader();
   List<C_t_chambres> res = new List<C_t_chambres>();
   while (dr.Read())
   {
    C_t_chambres tmp = new C_t_chambres();
    tmp.IDCha = int.Parse(dr["IDCha"].ToString());
    tmp.NomCha = int.Parse(dr["NomCha"].ToString());
    tmp.QuantiteLits = int.Parse(dr["QuantiteLits"].ToString());
    tmp.TypeCha = dr["TypeCha"].ToString();
    tmp.EtageCha = int.Parse(dr["EtageCha"].ToString());
    tmp.ServiceCha = dr["ServiceCha"].ToString();
    res.Add(tmp);
			}
			dr.Close();
			Commande.Connection.Close();
			return res;
		}
  public C_t_chambres Lire_ID(int IDCha)
  {
   CreerCommande("Selectionnert_chambres_ID");
   Commande.Parameters.AddWithValue("@IDCha", IDCha);
   Commande.Connection.Open();
   SqlDataReader dr = Commande.ExecuteReader();
   C_t_chambres res = new C_t_chambres();
   while (dr.Read())
   {
    res.IDCha = int.Parse(dr["IDCha"].ToString());
    res.NomCha = int.Parse(dr["NomCha"].ToString());
    res.QuantiteLits = int.Parse(dr["QuantiteLits"].ToString());
    res.TypeCha = dr["TypeCha"].ToString();
    res.EtageCha = int.Parse(dr["EtageCha"].ToString());
    res.ServiceCha = dr["ServiceCha"].ToString();
   }
			dr.Close();
			Commande.Connection.Close();
			return res;
		}
  public int Supprimer(int IDCha)
  {
   CreerCommande("Supprimert_chambres");
   int res = 0;
   Commande.Parameters.AddWithValue("@IDCha", IDCha);
   Commande.Connection.Open();
   res = Commande.ExecuteNonQuery();
			Commande.Connection.Close();
			return res;
		}
 }
}
