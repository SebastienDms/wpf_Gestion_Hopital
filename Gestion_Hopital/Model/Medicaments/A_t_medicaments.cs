using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Gestion_Hopital.Model
{
    public class A_t_medicaments : ADBase
    {
        public A_t_medicaments(string sChaineConnexion) : base(sChaineConnexion)
        {
        }
        public int Ajouter(string NomMedi, int QuantiteMedi, int PrixMedi)
        {
            CreerCommande("Ajoutert_medicaments");
            int res = 0;
            Commande.Parameters.Add("IDMedi", SqlDbType.Int);
            Direction("IDMedi", ParameterDirection.Output);
            Commande.Parameters.AddWithValue("@NomMedi", NomMedi);
            Commande.Parameters.AddWithValue("@QuantiteMedi", QuantiteMedi);
            Commande.Parameters.AddWithValue("@PrixMedi", PrixMedi);
            Commande.Connection.Open();
            Commande.ExecuteNonQuery();
            res = int.Parse(LireParametre("IDMedi"));
            Commande.Connection.Close();
            return res;
        }
        public int Modifier(int IDMedi, string NomMedi, int QuantiteMedi, int PrixMedi)
        {
            CreerCommande("Modifiert_medicaments");
            int res = 0;
            Commande.Parameters.AddWithValue("@IDMedi", IDMedi);
            Commande.Parameters.AddWithValue("@NomMedi", NomMedi);
            Commande.Parameters.AddWithValue("@QuantiteMedi", QuantiteMedi);
            Commande.Parameters.AddWithValue("@PrixMedi", PrixMedi);
            Commande.Connection.Open();
            Commande.ExecuteNonQuery();
            Commande.Connection.Close();
            return res;
        }
        public List<C_t_medicaments> Lire(string Index)
        {
            CreerCommande("Selectionnert_medicaments");
            Commande.Parameters.AddWithValue("@Index", Index);
            Commande.Connection.Open();
            SqlDataReader dr = Commande.ExecuteReader();
            List<C_t_medicaments> res = new List<C_t_medicaments>();
            while (dr.Read())
            {
                C_t_medicaments tmp = new C_t_medicaments();
                tmp.IDMedi = int.Parse(dr["IDMedi"].ToString());
                tmp.NomMedi = dr["NomMedi"].ToString();
                tmp.QuantiteMedi = int.Parse(dr["QuantiteMedi"].ToString());
                tmp.PrixMedi = int.Parse(dr["PrixMedi"].ToString());
                res.Add(tmp);
            }
            dr.Close();
            Commande.Connection.Close();
            return res;
        }
        public C_t_medicaments Lire_ID(int IDMedi)
        {
            CreerCommande("Selectionnert_medicaments_ID");
            Commande.Parameters.AddWithValue("@IDMedi", IDMedi);
            Commande.Connection.Open();
            SqlDataReader dr = Commande.ExecuteReader();
            C_t_medicaments res = new C_t_medicaments();
            while (dr.Read())
            {
                res.IDMedi = int.Parse(dr["IDMedi"].ToString());
                res.NomMedi = dr["NomMedi"].ToString();
                res.QuantiteMedi = int.Parse(dr["QuantiteMedi"].ToString());
                res.PrixMedi = int.Parse(dr["PrixMedi"].ToString());
            }
            dr.Close();
            Commande.Connection.Close();
            return res;
        }
        public int Supprimer(int IDMedi)
        {
            CreerCommande("Supprimert_medicaments");
            int res = 0;
            Commande.Parameters.AddWithValue("@IDMedi", IDMedi);
            Commande.Connection.Open();
            res = Commande.ExecuteNonQuery();
            Commande.Connection.Close();
            return res;
        }

    }
}
