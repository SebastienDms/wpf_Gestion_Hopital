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
 public class A_t_soigner : ADBase
 {
  #region Constructeurs
  public A_t_soigner(string sChaineConnexion)
  	: base(sChaineConnexion)
  { }
  #endregion
  public int Ajouter(int IDMed, int IDPat, int IDTyp, int IDMedi, DateTime DateOperation)
        {
   CreerCommande("Ajoutert_soigner");
   int res = 0;
   Commande.Parameters.Add("IDSoi", SqlDbType.Int);
   Direction("IDSoi", ParameterDirection.Output);
   Commande.Parameters.AddWithValue("@IDMed", IDMed);
   Commande.Parameters.AddWithValue("@IDPat", IDPat);
   Commande.Parameters.AddWithValue("@IDTyp", IDTyp);
   Commande.Parameters.AddWithValue("@IDMedi", IDMedi);
   Commande.Parameters.AddWithValue("@DateOperation", DateOperation);
   Commande.Connection.Open();
   Commande.ExecuteNonQuery();
   res = int.Parse(LireParametre("IDSoi"));
   Commande.Connection.Close();
   return res;
  }
  public int Modifier(int IDSoi, int IDMed, int IDPat, int IDTyp, int IDMedi, DateTime DateOperation)
  {
   CreerCommande("Modifiert_soigner");
   int res = 0;
   Commande.Parameters.AddWithValue("@IDSoi", IDSoi);
   Commande.Parameters.AddWithValue("@IDMed", IDMed);
   Commande.Parameters.AddWithValue("@IDPat", IDPat);
   Commande.Parameters.AddWithValue("@IDTyp", IDTyp);
   Commande.Parameters.AddWithValue("@IDMedi", IDMedi);
   Commande.Parameters.AddWithValue("@DateOperation", DateOperation);
   Commande.Connection.Open();
   Commande.ExecuteNonQuery();
   Commande.Connection.Close();
   return res;
  }
  public List<C_t_soigner> Lire(string Index)
  {
   CreerCommande("Selectionnert_soigner");
   Commande.Parameters.AddWithValue("@Index", Index);
   Commande.Connection.Open();
   SqlDataReader dr = Commande.ExecuteReader();
   List<C_t_soigner> res = new List<C_t_soigner>();
   while (dr.Read())
   {
       C_t_soigner tmp = new C_t_soigner();
       tmp.IDSoi = int.Parse(dr["IDSoi"].ToString());
       tmp.IDMed = int.Parse(dr["IDMed"].ToString());
       tmp.IDPat = int.Parse(dr["IDPat"].ToString());
       tmp.IDTyp = int.Parse(dr["IDTyp"].ToString());
       tmp.IDTyp = int.Parse(dr["IDMedi"].ToString());
       tmp.DateOperation = DateTime.Parse(dr["DateOperation"].ToString());
       res.Add(tmp);
   }
   dr.Close();
   Commande.Connection.Close();
   return res;
  }
  public C_t_soigner Lire_ID(int IDSoi)
  {
   CreerCommande("Selectionnert_soigner_ID");
   Commande.Parameters.AddWithValue("@IDSoi", IDSoi);
   Commande.Connection.Open();
   SqlDataReader dr = Commande.ExecuteReader();
   C_t_soigner res = new C_t_soigner();
   while (dr.Read())
   {
    res.IDSoi = int.Parse(dr["IDSoi"].ToString());
    res.IDMed = int.Parse(dr["IDMed"].ToString());
    res.IDPat = int.Parse(dr["IDPat"].ToString());
    res.IDTyp = int.Parse(dr["IDTyp"].ToString());
    res.IDTyp = int.Parse(dr["IDMedi"].ToString());
    res.DateOperation = DateTime.Parse(dr["DateOperation"].ToString());
   }
   dr.Close();
   Commande.Connection.Close();
   return res;
  }
  public int Supprimer(int IDSoi)
  {
   CreerCommande("Supprimert_soigner");
   int res = 0;
   Commande.Parameters.AddWithValue("@IDSoi", IDSoi);
   Commande.Connection.Open();
   res = Commande.ExecuteNonQuery();
   Commande.Connection.Close();
   return res;
  }
 }
}
