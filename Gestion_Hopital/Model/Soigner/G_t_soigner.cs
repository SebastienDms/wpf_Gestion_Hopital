#region Ressources extérieures

using System;
using System.Collections.Generic;

#endregion

namespace Gestion_Hopital.Model
{
 /// <summary>
 /// Couche intermédiaire de gestion (Business Layer)
 /// </summary>
 public class G_t_soigner : G_Base
 {
  #region Constructeurs
  public G_t_soigner()
   : base()
  { }
  public G_t_soigner(string sChaineConnexion)
   : base(sChaineConnexion)
  { }
  #endregion
  public int Ajouter(int IDMed, int IDPat, int IDTyp, int IDMedi, DateTime DateOperation)
  { return new A_t_soigner(ChaineConnexion).Ajouter(IDMed, IDPat, IDTyp, IDMedi, DateOperation); }
  public int Modifier(int IDSoi, int IDMed, int IDPat, int IDTyp, int IDMedi, DateTime DateOperation)
  { return new A_t_soigner(ChaineConnexion).Modifier(IDSoi, IDMed, IDPat, IDTyp, IDMedi, DateOperation); }
  public List<C_t_soigner> Lire(string Index)
  { return new A_t_soigner(ChaineConnexion).Lire(Index); }
  public C_t_soigner Lire_ID(int IDSoi)
  { return new A_t_soigner(ChaineConnexion).Lire_ID(IDSoi); }
  public int Supprimer(int IDSoi)
  { return new A_t_soigner(ChaineConnexion).Supprimer(IDSoi); }
 }
}
