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
 public class A_t_patients : ADBase
 {
  #region Constructeurs
  public A_t_patients(string sChaineConnexion)
  	: base(sChaineConnexion)
  { }
  #endregion
  public int Ajouter(string NomPat, string PrenomPat, string AdressePat, int GSMPat, string MailPat)
  {
   CreerCommande("Ajoutert_patients");
   int res = 0;
   Commande.Parameters.Add("IDPat", SqlDbType.Int);
   Direction("IDPat", ParameterDirection.Output);
   Commande.Parameters.AddWithValue("@NomPat", NomPat);
   Commande.Parameters.AddWithValue("@PrenomPat", PrenomPat);
   Commande.Parameters.AddWithValue("@AdressePat", AdressePat);
   Commande.Parameters.AddWithValue("@GSMPat", GSMPat);
   Commande.Parameters.AddWithValue("@MailPat", MailPat);
   Commande.Connection.Open();
   Commande.ExecuteNonQuery();
   res = int.Parse(LireParametre("IDPat"));
   Commande.Connection.Close();
   return res;
  }
  public int Modifier(int IDPat, string NomPat, string PrenomPat, string AdressePat, int GSMPat, string MailPat)
  {
   CreerCommande("Modifiert_patients");
   int res = 0;
   Commande.Parameters.AddWithValue("@IDPat", IDPat);
   Commande.Parameters.AddWithValue("@NomPat", NomPat);
   Commande.Parameters.AddWithValue("@PrenomPat", PrenomPat);
   Commande.Parameters.AddWithValue("@AdressePat", AdressePat);
   Commande.Parameters.AddWithValue("@GSMPat", GSMPat);
   Commande.Parameters.AddWithValue("@MailPat", MailPat);
   Commande.Connection.Open();
   Commande.ExecuteNonQuery();
   Commande.Connection.Close();
   return res;
  }
  public List<C_t_patients> Lire(string Index)
  {
   CreerCommande("Selectionnert_patients");
   Commande.Parameters.AddWithValue("@Index", Index);
   Commande.Connection.Open();
   SqlDataReader dr = Commande.ExecuteReader();
   List<C_t_patients> res = new List<C_t_patients>();
   while (dr.Read())
   {
    C_t_patients tmp = new C_t_patients();
    tmp.IDPat = int.Parse(dr["IDPat"].ToString());
    tmp.NomPat = dr["NomPat"].ToString();
    tmp.PrenomPat = dr["PrenomPat"].ToString();
    tmp.AdressePat = dr["AdressePat"].ToString();
    tmp.GSMPat = int.Parse(dr["GSMPat"].ToString());
    tmp.MailPat = dr["MailPat"].ToString();
    res.Add(tmp);
	}
   dr.Close();
   Commande.Connection.Close();
   return res;
  }
  public C_t_patients Lire_ID(int IDPat)
  {
   CreerCommande("Selectionnert_patients_ID");
   Commande.Parameters.AddWithValue("@IDPat", IDPat);
   Commande.Connection.Open();
   SqlDataReader dr = Commande.ExecuteReader();
   C_t_patients res = new C_t_patients();
   while (dr.Read())
   {
    res.IDPat = int.Parse(dr["IDPat"].ToString());
    res.NomPat = dr["NomPat"].ToString();
    res.PrenomPat = dr["PrenomPat"].ToString();
    res.AdressePat = dr["AdressePat"].ToString();
    res.GSMPat = int.Parse(dr["GSMPat"].ToString());
    res.MailPat = dr["MailPat"].ToString();
   }
   dr.Close();
   Commande.Connection.Close();
   return res;
  }
  public int Supprimer(int IDPat)
  {
   CreerCommande("Supprimert_patients");
   int res = 0;
   Commande.Parameters.AddWithValue("@IDPat", IDPat);
   Commande.Connection.Open();
   res = Commande.ExecuteNonQuery();
			Commande.Connection.Close();
			return res;
		}
 }
}
