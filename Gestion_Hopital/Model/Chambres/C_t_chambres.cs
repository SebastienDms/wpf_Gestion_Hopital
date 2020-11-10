#region Ressources extérieures

#endregion

namespace Gestion_Hopital.Model
{
 /// <summary>
 /// Classe de définition des données
 /// </summary>
 public class C_t_chambres
 {
  #region Données membres
  private int _IDCha;
  private int _NomCha;
  private int _QuantiteLits;
  private string _TypeCha;
  private int _EtageCha;
  private string _ServiceCha;
  #endregion
  #region Constructeurs
  public C_t_chambres()
  { }
  public C_t_chambres(int NomCha_, int QuantiteLits_, string TypeCha_, int EtageCha_, string ServiceCha_)
  {
   NomCha = NomCha_;
   QuantiteLits = QuantiteLits_;
   TypeCha = TypeCha_;
   EtageCha = EtageCha_;
   ServiceCha = ServiceCha_;
  }
  public C_t_chambres(int IDCha_, int NomCha_, int QuantiteLits_, string TypeCha_, int EtageCha_, string ServiceCha_)
   : this(NomCha_, QuantiteLits_, TypeCha_, EtageCha_, ServiceCha_)
  {
   IDCha = IDCha_;
  }
  #endregion
  #region Accesseurs
  public int IDCha
  {
   get { return _IDCha; }
   set { _IDCha = value; }
  }
  public int NomCha
  {
   get { return _NomCha; }
   set { _NomCha = value; }
  }
  public int QuantiteLits
  {
   get { return _QuantiteLits; }
   set { _QuantiteLits = value; }
  }
  public string TypeCha
  {
   get { return _TypeCha; }
   set { _TypeCha = value; }
  }
  public int EtageCha
  {
   get { return _EtageCha; }
   set { _EtageCha = value; }
  }
  public string ServiceCha
  {
   get { return _ServiceCha; }
   set { _ServiceCha = value; }
  }
  #endregion
 }
}
