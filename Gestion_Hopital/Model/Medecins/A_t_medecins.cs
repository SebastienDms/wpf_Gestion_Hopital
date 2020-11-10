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
 public class A_t_medecins : ADBase
 {
    public A_t_medecins(string sChaineConnexion)
      : base(sChaineConnexion)
    {
    }

    public int Ajouter(string NomMed,
      string PrenomMed,
      int GSMMed,
      int IDSpe,
      DateTime? DateNaisMed)
    {
        this.CreerCommande("Ajoutert_medecins");
        this.Commande.Parameters.Add("IDMed", SqlDbType.Int);
        this.Direction("IDMed", ParameterDirection.Output);
        this.Commande.Parameters.AddWithValue("@NomMed", (object)NomMed);
        this.Commande.Parameters.AddWithValue("@PrenomMed", (object)PrenomMed);
        this.Commande.Parameters.AddWithValue("@GSMMed", (object)GSMMed);
        this.Commande.Parameters.AddWithValue("@IDSpe", (object)IDSpe);
        this.Commande.Parameters.AddWithValue("@DateNaisMed", (object)DateNaisMed);
        this.Commande.Connection.Open();
        this.Commande.ExecuteNonQuery();
        int num = int.Parse(this.LireParametre("IDMed"));
        this.Commande.Connection.Close();
        return num;
    }

    public int Modifier(
      int IDMed,
      string NomMed,
      string PrenomMed,
      int GSMMed,
      int IDSpe,
      DateTime? DateNaisMed)
    {
        this.CreerCommande("Modifiert_medecins");
        int num = 0;
        this.Commande.Parameters.AddWithValue("@IDMed", (object)IDMed);
        this.Commande.Parameters.AddWithValue("@NomMed", (object)NomMed);
        this.Commande.Parameters.AddWithValue("@PrenomMed", (object)PrenomMed);
        this.Commande.Parameters.AddWithValue("@GSMMed", (object)GSMMed);
        this.Commande.Parameters.AddWithValue("@IDSpe", (object)IDSpe);
        this.Commande.Parameters.AddWithValue("@DateNaisMed", (object)DateNaisMed);
        this.Commande.Connection.Open();
        this.Commande.ExecuteNonQuery();
        this.Commande.Connection.Close();
        return num;
    }

    public List<C_t_medecins> Lire(string Index)
    {
        this.CreerCommande("Selectionnert_medecins");
        this.Commande.Parameters.AddWithValue("@Index", (object)Index);
        this.Commande.Connection.Open();
        SqlDataReader sqlDataReader = this.Commande.ExecuteReader();
        List<C_t_medecins> cTMedecinsList = new List<C_t_medecins>();
        while (sqlDataReader.Read())
            cTMedecinsList.Add(new C_t_medecins()
            {
                IDMed = int.Parse(sqlDataReader["IDMed"].ToString()),
                NomMed = sqlDataReader["NomMed"].ToString(),
                PrenomMed = sqlDataReader["PrenomMed"].ToString(),
                GSMMed = int.Parse(sqlDataReader["GSMMed"].ToString()),
                IDSpe = int.Parse(sqlDataReader["IDSpe"].ToString()),
                DateNaisMed = new DateTime?(DateTime.Parse(sqlDataReader["DateNaisMed"].ToString()))
            });
        sqlDataReader.Close();
        this.Commande.Connection.Close();
        return cTMedecinsList;
    }

    public C_t_medecins Lire_ID(int IDMed)
    {
        this.CreerCommande("Selectionnert_medecins_ID");
        this.Commande.Parameters.AddWithValue("@IDMed", (object)IDMed);
        this.Commande.Connection.Open();
        SqlDataReader sqlDataReader = this.Commande.ExecuteReader();
        C_t_medecins cTMedecins = new C_t_medecins();
        while (sqlDataReader.Read())
        {
            cTMedecins.IDMed = int.Parse(sqlDataReader[nameof(IDMed)].ToString());
            cTMedecins.NomMed = sqlDataReader["NomMed"].ToString();
            cTMedecins.PrenomMed = sqlDataReader["PrenomMed"].ToString();
            cTMedecins.GSMMed = int.Parse(sqlDataReader["GSMMed"].ToString());
            cTMedecins.IDSpe = int.Parse(sqlDataReader["IDSpe"].ToString());
            cTMedecins.DateNaisMed = new DateTime?(DateTime.Parse(sqlDataReader["DateNaisMed"].ToString()));
        }
        sqlDataReader.Close();
        this.Commande.Connection.Close();
        return cTMedecins;
    }

    public int Supprimer(int IDMed)
    {
        this.CreerCommande("Supprimert_medecins");
        this.Commande.Parameters.AddWithValue("@IDMed", (object)IDMed);
        this.Commande.Connection.Open();
        int num = this.Commande.ExecuteNonQuery();
        this.Commande.Connection.Close();
        return num;
    }
 }
}
