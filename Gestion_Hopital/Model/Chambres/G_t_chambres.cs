#region Ressources extérieures

using System.Collections.Generic;

#endregion

namespace Gestion_Hopital.Model
{
 /// <summary>
 /// Couche intermédiaire de gestion (Business Layer)
 /// </summary>
 public class G_t_chambres : G_Base
 {
  #region Constructeurs
  public G_t_chambres()
   : base()
  { }
  public G_t_chambres(string sChaineConnexion)
   : base(sChaineConnexion)
  { }
  #endregion
  public int Ajouter(int NomCha, int QuantiteLits, string TypeCha, int EtageCha, string ServiceCha)
  { return new A_t_chambres(ChaineConnexion).Ajouter(NomCha, QuantiteLits, TypeCha, EtageCha, ServiceCha); }
  public int Modifier(int IDCha, int NomCha, int QuantiteLits, string TypeCha, int EtageCha, string ServiceCha)
  { return new A_t_chambres(ChaineConnexion).Modifier(IDCha, NomCha, QuantiteLits, TypeCha, EtageCha, ServiceCha); }
  public List<C_t_chambres> Lire(string Index)
  { return new A_t_chambres(ChaineConnexion).Lire(Index); }
  public C_t_chambres Lire_ID(int IDCha)
  { return new A_t_chambres(ChaineConnexion).Lire_ID(IDCha); }
  public int Supprimer(int IDCha)
  { return new A_t_chambres(ChaineConnexion).Supprimer(IDCha); }
 }
}
