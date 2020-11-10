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
 public class A_t_specialites : ADBase
 {
  #region Constructeurs
  public A_t_specialites(string sChaineConnexion)
  	: base(sChaineConnexion)
  { }
  #endregion
  public int Ajouter(string NomSpe)
  {
   CreerCommande("Ajoutert_specialites");
   int res = 0;
   Commande.Parameters.Add("IDSpe", SqlDbType.Int);
   Direction("IDSpe", ParameterDirection.Output);
   Commande.Parameters.AddWithValue("@NomSpe", NomSpe);
   Commande.Connection.Open();
   Commande.ExecuteNonQuery();
   res = int.Parse(LireParametre("IDSpe"));
   Commande.Connection.Close();
   return res;
  }
  public int Modifier(int IDSpe, string NomSpe)
  {
   CreerCommande("Modifiert_specialites");
   int res = 0;
   Commande.Parameters.AddWithValue("@IDSpe", IDSpe);
   Commande.Parameters.AddWithValue("@NomSpe", NomSpe);
   Commande.Connection.Open();
   Commande.ExecuteNonQuery();
   Commande.Connection.Close();
   return res;
  }
  public List<C_t_specialites> Lire(string Index)
  {
   CreerCommande("Selectionnert_specialites");
   Commande.Parameters.AddWithValue("@Index", Index);
   Commande.Connection.Open();
   SqlDataReader dr = Commande.ExecuteReader();
   List<C_t_specialites> res = new List<C_t_specialites>();
   while (dr.Read())
   {
    C_t_specialites tmp = new C_t_specialites();
    tmp.IDSpe = int.Parse(dr["IDSpe"].ToString());
    tmp.NomSpe = dr["NomSpe"].ToString();
    res.Add(tmp);
			}
			dr.Close();
			Commande.Connection.Close();
			return res;
		}
  public C_t_specialites Lire_ID(int IDSpe)
  {
   CreerCommande("Selectionnert_specialites_ID");
   Commande.Parameters.AddWithValue("@IDSpe", IDSpe);
   Commande.Connection.Open();
   SqlDataReader dr = Commande.ExecuteReader();
   C_t_specialites res = new C_t_specialites();
   while (dr.Read())
   {
    res.IDSpe = int.Parse(dr["IDSpe"].ToString());
    res.NomSpe = dr["NomSpe"].ToString();
   }
			dr.Close();
			Commande.Connection.Close();
			return res;
		}
  public int Supprimer(int IDSpe)
  {
   CreerCommande("Supprimert_specialites");
   int res = 0;
   Commande.Parameters.AddWithValue("@IDSpe", IDSpe);
   Commande.Connection.Open();
   res = Commande.ExecuteNonQuery();
			Commande.Connection.Close();
			return res;
		}
 }
}
