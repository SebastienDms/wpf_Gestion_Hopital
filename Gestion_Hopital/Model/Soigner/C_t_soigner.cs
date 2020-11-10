#region Ressources extérieures

using System;

#endregion

namespace Gestion_Hopital.Model
{
 /// <summary>
 /// Classe de définition des données
 /// </summary>
 public class C_t_soigner
 {
  #region Données membres
  private int _IDSoi;
  private int _IDMed;
  private int _IDPat;
  private int _idTyp;
  private int _idMedi;
  private DateTime _dateOperation;
  #endregion
  #region Constructeurs
  public C_t_soigner()
  { }
  public C_t_soigner(int IDMed_, int IDPat_, int IDTyp_, int IDMedi_, DateTime DateOperation_)
  {
   IDMed = IDMed_;
   IDPat = IDPat_;
   IDTyp = IDTyp_;
   IDMedi = IDMedi_;
   DateOperation = DateOperation_;
  }
  public C_t_soigner(int IDSoi_, int IDMed_, int IDPat_, int IDTyp_, int IDMedi_, DateTime DateOperation_)
   : this(IDMed_, IDPat_, IDTyp_, IDMedi_, DateOperation_)
  {
   IDSoi = IDSoi_;
  }
  #endregion
  #region Accesseurs
  public int IDSoi
  {
   get { return _IDSoi; }
   set { _IDSoi = value; }
  }
  public int IDMed
  {
   get { return _IDMed; }
   set { _IDMed = value; }
  }
  public int IDPat
  {
   get { return _IDPat; }
   set { _IDPat = value; }
  }
  public DateTime DateOperation
  {
   get { return _dateOperation; }
   set { _dateOperation = value; }
  }
  public int IDTyp
  {
      get => _idTyp;
      set => _idTyp = value;
  }
  public int IDMedi
  {
      get => _idMedi;
      set => _idMedi = value;
  }
  #endregion
 }
}
