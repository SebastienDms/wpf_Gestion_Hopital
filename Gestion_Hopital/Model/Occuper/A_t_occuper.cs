#region Ressources extérieures

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

#endregion

namespace Gestion_Hopital.Model
{
 /// <summary>
 /// Couche d'accès aux données (Data Access Layer)
 /// </summary>
 public class A_t_occuper : ADBase
 {
  #region Constructeurs
  public A_t_occuper(string sChaineConnexion)
  	: base(sChaineConnexion)
  { }
  #endregion
  public int Ajouter(int IDPat, int IDCha, DateTime DateEntree, DateTime DateSortie, int PrixJournalier)
  {
   CreerCommande("Ajoutert_occuper");
   int res = 0;
   Commande.Parameters.Add("IDOcc", SqlDbType.Int);
   Direction("IDOcc", ParameterDirection.Output);
   Commande.Parameters.AddWithValue("@IDPat", IDPat);
   Commande.Parameters.AddWithValue("@IDCha", IDCha);
   Commande.Parameters.AddWithValue("@DateEntree", DateEntree);
   Commande.Parameters.AddWithValue("@DateSortie", DateSortie);
   Commande.Parameters.AddWithValue("@PrixJournalier", PrixJournalier);
   Commande.Connection.Open();
   Commande.ExecuteNonQuery();
   res = int.Parse(LireParametre("IDOcc"));
   Commande.Connection.Close();
   return res;
  }
  public int Modifier(int IDOcc, int IDPat, int IDCha, DateTime DateEntree, DateTime DateSortie, int PrixJournalier)
  {
   CreerCommande("Modifiert_occuper");
   int res = 0;
   Commande.Parameters.AddWithValue("@IDOcc", IDOcc);
   Commande.Parameters.AddWithValue("@IDPat", IDPat);
   Commande.Parameters.AddWithValue("@IDCha", IDCha);
   Commande.Parameters.AddWithValue("@DateEntree", DateEntree);
   Commande.Parameters.AddWithValue("@DateSortie", DateSortie);
   Commande.Parameters.AddWithValue("@PrixJournalier", PrixJournalier);
   Commande.Connection.Open();
   Commande.ExecuteNonQuery();
   Commande.Connection.Close();
   return res;
  }
  public List<C_t_occuper> Lire(string Index)
  {
   CreerCommande("Selectionnert_occuper");
   Commande.Parameters.AddWithValue("@Index", Index);
   Commande.Connection.Open();
   SqlDataReader dr = Commande.ExecuteReader();
   List<C_t_occuper> res = new List<C_t_occuper>();
   while (dr.Read())
   {
    C_t_occuper tmp = new C_t_occuper();
    tmp.IDOcc = int.Parse(dr["IDOcc"].ToString());
    tmp.IDPat = int.Parse(dr["IDPat"].ToString());
    tmp.IDCha = int.Parse(dr["IDCha"].ToString());
    tmp.DateEntree = DateTime.Parse(dr["DateEntree"].ToString());
    tmp.DateSortie = DateTime.Parse(dr["DateSortie"].ToString());
    tmp.PrixJournalier = int.Parse(dr["PrixJournalier"].ToString());
    res.Add(tmp);
   }
    dr.Close();
    Commande.Connection.Close();
    return res;
  }
  public C_t_occuper Lire_ID(int IDOcc)
  {
   CreerCommande("Selectionnert_occuper_ID");
   Commande.Parameters.AddWithValue("@IDOcc", IDOcc);
   Commande.Connection.Open();
   SqlDataReader dr = Commande.ExecuteReader();
   C_t_occuper res = new C_t_occuper();
   while (dr.Read())
   {
    res.IDOcc = int.Parse(dr["IDOcc"].ToString());
    res.IDPat = int.Parse(dr["IDPat"].ToString());
    res.IDCha = int.Parse(dr["IDCha"].ToString());
    res.DateEntree = DateTime.Parse(dr["DateEntree"].ToString());
    res.DateSortie = DateTime.Parse(dr["DateSortie"].ToString());
    res.PrixJournalier = int.Parse(dr["PrixJournalier"].ToString());
   }
	dr.Close();
	Commande.Connection.Close();
	return res;
  }
  public int Supprimer(int IDOcc)
  {
   CreerCommande("Supprimert_occuper");
   int res = 0;
   Commande.Parameters.AddWithValue("@IDOcc", IDOcc);
   Commande.Connection.Open();
   res = Commande.ExecuteNonQuery();
			Commande.Connection.Close();
			return res;
  }
 }
}
