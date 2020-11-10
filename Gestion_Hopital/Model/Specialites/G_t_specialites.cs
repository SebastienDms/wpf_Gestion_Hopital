#region Ressources extérieures

using System.Collections.Generic;

#endregion

namespace Gestion_Hopital.Model
{
 /// <summary>
 /// Couche intermédiaire de gestion (Business Layer)
 /// </summary>
 public class G_t_specialites : G_Base
 {
  #region Constructeurs
  public G_t_specialites()
   : base()
  { }
  public G_t_specialites(string sChaineConnexion)
   : base(sChaineConnexion)
  { }
  #endregion
  public int Ajouter(string NomSpe)
  { return new A_t_specialites(ChaineConnexion).Ajouter(NomSpe); }
  public int Modifier(int IDSpe, string NomSpe)
  { return new A_t_specialites(ChaineConnexion).Modifier(IDSpe, NomSpe); }
  public List<C_t_specialites> Lire(string Index)
  { return new A_t_specialites(ChaineConnexion).Lire(Index); }
  public C_t_specialites Lire_ID(int IDSpe)
  { return new A_t_specialites(ChaineConnexion).Lire_ID(IDSpe); }
  public int Supprimer(int IDSpe)
  { return new A_t_specialites(ChaineConnexion).Supprimer(IDSpe); }
 }
}
