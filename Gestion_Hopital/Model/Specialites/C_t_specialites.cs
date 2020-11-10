#region Ressources extérieures

#endregion

namespace Gestion_Hopital.Model
{
 /// <summary>
 /// Classe de définition des données
 /// </summary>
 public class C_t_specialites
 {
  #region Données membres
  private int _IDSpe;
  private string _NomSpe;
  #endregion
  #region Constructeurs
  public C_t_specialites()
  { }
  public C_t_specialites(string NomSpe_)
  {
   NomSpe = NomSpe_;
  }
  public C_t_specialites(int IDSpe_, string NomSpe_)
   : this(NomSpe_)
  {
   IDSpe = IDSpe_;
  }
  #endregion
  #region Accesseurs
  public int IDSpe
  {
   get { return _IDSpe; }
   set { _IDSpe = value; }
  }
  public string NomSpe
  {
   get { return _NomSpe; }
   set { _NomSpe = value; }
  }
  #endregion
 }
}
