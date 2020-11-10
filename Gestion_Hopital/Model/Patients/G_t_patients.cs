#region Ressources extérieures

using System.Collections.Generic;

#endregion

namespace Gestion_Hopital.Model
{
 /// <summary>
 /// Couche intermédiaire de gestion (Business Layer)
 /// </summary>
 public class G_t_patients : G_Base
 {
  #region Constructeurs
  public G_t_patients()
   : base()
  { }
  public G_t_patients(string sChaineConnexion)
   : base(sChaineConnexion)
  { }
  #endregion
  public int Ajouter(string NomPat, string PrenomPat, string AdressePat, int GSMPat, string MailPat)
  { return new A_t_patients(ChaineConnexion).Ajouter(NomPat, PrenomPat, AdressePat, GSMPat, MailPat); }
  public int Modifier(int IDPat, string NomPat, string PrenomPat, string AdressePat, int GSMPat, string MailPat)
  { return new A_t_patients(ChaineConnexion).Modifier(IDPat, NomPat, PrenomPat, AdressePat, GSMPat, MailPat); }
  public List<C_t_patients> Lire(string Index)
  { return new A_t_patients(ChaineConnexion).Lire(Index); }
  public C_t_patients Lire_ID(int IDPat)
  { return new A_t_patients(ChaineConnexion).Lire_ID(IDPat); }
  public int Supprimer(int IDPat)
  { return new A_t_patients(ChaineConnexion).Supprimer(IDPat); }
 }
}
