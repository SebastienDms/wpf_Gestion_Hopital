#region Ressources extérieures

using System;
using System.Collections.Generic;

#endregion

namespace Gestion_Hopital.Model
{
 /// <summary>
 /// Couche intermédiaire de gestion (Business Layer)
 /// </summary>
 public class G_t_occuper : G_Base
 {
  #region Constructeurs
  public G_t_occuper()
   : base()
  { }
  public G_t_occuper(string sChaineConnexion)
   : base(sChaineConnexion)
  { }
  #endregion
  public int Ajouter(int IDPat, int IDCha, DateTime DateEntree, DateTime DateSortie, int PrixJournalier)
  { return new A_t_occuper(ChaineConnexion).Ajouter(IDPat, IDCha, DateEntree, DateSortie, PrixJournalier); }
  public int Modifier(int IDOcc, int IDPat, int IDCha, DateTime DateEntree, DateTime DateSortie, int PrixJournalier)
  { return new A_t_occuper(ChaineConnexion).Modifier(IDOcc, IDPat, IDCha, DateEntree, DateSortie, PrixJournalier); }
  public List<C_t_occuper> Lire(string Index)
  { return new A_t_occuper(ChaineConnexion).Lire(Index); }
  public C_t_occuper Lire_ID(int IDOcc)
  { return new A_t_occuper(ChaineConnexion).Lire_ID(IDOcc); }
  public int Supprimer(int IDOcc)
  { return new A_t_occuper(ChaineConnexion).Supprimer(IDOcc); }
 }
}
