#region Ressources extérieures

using System;
using System.Collections.Generic;

#endregion

namespace Gestion_Hopital.Model
{
 /// <summary>
 /// Couche intermédiaire de gestion (Business Layer)
 /// </summary>
 public class G_t_medecins : G_Base
 {
    #region Constructeurs
    public G_t_medecins()
    {
    }

     public G_t_medecins(string sChaineConnexion)
         : base(sChaineConnexion)
     {
     }
    #endregion
    #region Methodes
    public int Ajouter(
     string NomMed,
     string PrenomMed,
     int GSMMed,
     int IDSpe,
     DateTime? DateNaisMed)
    {
         return new A_t_medecins(this.ChaineConnexion).Ajouter(NomMed, PrenomMed, GSMMed, IDSpe, DateNaisMed);
    }

     public int Modifier(int IDMed, string NomMed, string PrenomMed, int GSMMed, int IDSpe, DateTime? DateNaisMed)
     {
         return new A_t_medecins(this.ChaineConnexion).Modifier(IDMed, NomMed, PrenomMed, GSMMed, IDSpe, DateNaisMed);
     }

     public List<C_t_medecins> Lire(string Index) => new A_t_medecins(this.ChaineConnexion).Lire(Index);

     public C_t_medecins Lire_ID(int IDMed) => new A_t_medecins(this.ChaineConnexion).Lire_ID(IDMed);

     public int Supprimer(int IDMed) => new A_t_medecins(this.ChaineConnexion).Supprimer(IDMed);
    #endregion
    }
}
