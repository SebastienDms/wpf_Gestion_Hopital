using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Gestion_Hopital.Model
{
    class A_t_soins : ADBase
    {
        public A_t_soins(string sChaineConnexion) : base(sChaineConnexion)
        {
        }
        public int Ajouter(string NomSoin, int PrixSoin)
        {
            CreerCommande("Ajoutert_soins");
            int res = 0;
            Commande.Parameters.Add("IdSoin", SqlDbType.Int);
            Direction("IdSoin", ParameterDirection.Output);
            Commande.Parameters.AddWithValue("@NomSoin", NomSoin);
            Commande.Parameters.AddWithValue("@PrixSoin", PrixSoin);
            Commande.Connection.Open();
            Commande.ExecuteNonQuery();
            res = int.Parse(LireParametre("IdSoin"));
            Commande.Connection.Close();
            return res;
        }
        public int Modifier(int IdSoin, string NomSoin, int PrixSoin)
        {
            CreerCommande("Modifiert_soins");
            int res = 0;
            Commande.Parameters.AddWithValue("@IdSoin", IdSoin);
            Commande.Parameters.AddWithValue("@NomSoin", NomSoin);
            Commande.Parameters.AddWithValue("@PrixSoin", PrixSoin);
            Commande.Connection.Open();
            Commande.ExecuteNonQuery();
            Commande.Connection.Close();
            return res;
        }
        public List<C_t_soins> Lire(string Index)
        {
            CreerCommande("Selectionnert_soins");
            Commande.Parameters.AddWithValue("@Index", Index);
            Commande.Connection.Open();
            SqlDataReader dr = Commande.ExecuteReader();
            List<C_t_soins> res = new List<C_t_soins>();
            while (dr.Read())
            {
                C_t_soins tmp = new C_t_soins();
                tmp.IDSoin = int.Parse(dr["IdSoin"].ToString());
                tmp.NomSoin = dr["NomSoin"].ToString();
                tmp.PrixSoin = int.Parse(dr["PrixSoin"].ToString());
                res.Add(tmp);
            }
            dr.Close();
            Commande.Connection.Close();
            return res;
        }
        public C_t_soins Lire_ID(int IDMedi)
        {
            CreerCommande("Selectionnert_soins_ID");
            Commande.Parameters.AddWithValue("@IdSoin", IDMedi);
            Commande.Connection.Open();
            SqlDataReader dr = Commande.ExecuteReader();
            C_t_soins res = new C_t_soins();
            while (dr.Read())
            {
                res.IDSoin = int.Parse(dr["IdSoin"].ToString());
                res.NomSoin = dr["NomSoin"].ToString();
                res.PrixSoin = int.Parse(dr["PrixSoin"].ToString());
            }
            dr.Close();
            Commande.Connection.Close();
            return res;
        }
        public int Supprimer(int IdSoin)
        {
            CreerCommande("Supprimert_soins");
            int res = 0;
            Commande.Parameters.AddWithValue("@IdSoin", IdSoin);
            Commande.Connection.Open();
            res = Commande.ExecuteNonQuery();
            Commande.Connection.Close();
            return res;
        }

    }
}
