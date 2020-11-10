#region Ressources extérieures

#endregion

namespace Gestion_Hopital.Model
{
 /// <summary>
 /// Classe de définition des données
 /// </summary>
 public class C_t_patients
 {
  #region Données membres
  private int _IDPat;
  private string _NomPat;
  private string _PrenomPat;
  private string _AdressePat;
  private int _GSMPat;
  private string _MailPat;
  #endregion
  #region Constructeurs
  public C_t_patients()
  { }
  public C_t_patients(string NomPat_, string PrenomPat_, string AdressePat_, int GSMPat_, string MailPat_)
  {
   NomPat = NomPat_;
   PrenomPat = PrenomPat_;
   AdressePat = AdressePat_;
   GSMPat = GSMPat_;
   MailPat = MailPat_;
  }
  public C_t_patients(int IDPat_, string NomPat_, string PrenomPat_, string AdressePat_, int GSMPat_, string MailPat_)
   : this(NomPat_,PrenomPat_,AdressePat_,GSMPat_,MailPat_)
  {
   IDPat = IDPat_;
  }
  #endregion
  #region Accesseurs
  public int IDPat
  {
   get { return _IDPat; }
   set { _IDPat = value; }
  }
  public string NomPat
  {
   get { return _NomPat; }
   set { _NomPat = value; }
  }
  public string PrenomPat
  {
   get { return _PrenomPat; }
   set { _PrenomPat = value; }
  }
  public string AdressePat
  {
   get { return _AdressePat; }
   set { _AdressePat = value; }
  }
  public int GSMPat
  {
   get { return _GSMPat; }
   set { _GSMPat = value; }
  }

  public string MailPat
  {
   get { return _MailPat;}
   set { _MailPat = value;}
  }

  #endregion
 }
}
