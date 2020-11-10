#region Ressources extérieures

using System;

#endregion

namespace Gestion_Hopital.Model
{
 /// <summary>
 /// Classe de définition des données
 /// </summary>
 public class C_t_occuper
 {
  #region Données membres
  private int _IDOcc;
  private int _IDPat;
  private int _IDCha;
  private DateTime _DateEntree;
  private DateTime _DateSortie;
  private int _PrixJournalier;
  #endregion
  #region Constructeurs
  public C_t_occuper()
  { }
  public C_t_occuper(int IDPat_, int IDCha_, DateTime DateEntree_, DateTime DateSortie_, int PrixJournalier_)
  {
   IDPat = IDPat_;
   IDCha = IDCha_;
   DateEntree = DateEntree_;
   DateSortie = DateSortie_;
   PrixJournalier = PrixJournalier_;
  }
  public C_t_occuper(int IDOcc_, int IDPat_, int IDCha_, DateTime DateEntree_, DateTime DateSortie_, int PrixJournalier_)
   : this(IDPat_, IDCha_, DateEntree_, DateSortie_, PrixJournalier_)
  {
   IDOcc = IDOcc_;
  }
  #endregion
  #region Accesseurs
  public int IDOcc
  {
   get { return _IDOcc; }
   set { _IDOcc = value; }
  }
  public int IDPat
  {
   get { return _IDPat; }
   set { _IDPat = value; }
  }
  public int IDCha
  {
   get { return _IDCha; }
   set { _IDCha = value; }
  }
  public DateTime DateEntree
  {
   get { return _DateEntree; }
   set { _DateEntree = value; }
  }
  public DateTime DateSortie
  {
   get { return _DateSortie; }
   set { _DateSortie = value; }
  }
  public int PrixJournalier
  {
   get { return _PrixJournalier; }
   set { _PrixJournalier = value; }
  }
  #endregion
 }
}
