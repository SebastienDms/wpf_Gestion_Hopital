using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gestion_Hopital.Model;

namespace Gestion_Hopital.ViewModel
{
    public class VM_Occuper : BasePropriete
    {
        #region Données Écran
        private string chConnexion = @"Data Source=DESKTOP-GES02KU;Initial Catalog=BD_Hopital;Integrated Security=True";
        private int nAjout;
        private bool _ActiverUneFiche;
        public bool ActiverUneFiche
        {
            get { return _ActiverUneFiche; }
            set
            {
                AssignerChamp<bool>(ref _ActiverUneFiche, value, System.Reflection.MethodBase.GetCurrentMethod().Name);
                ActiverBcpFiche = !ActiverUneFiche;
            }
        }
        private bool _ActiverBcpFiche;
        public bool ActiverBcpFiche
        {
            get { return _ActiverBcpFiche; }
            set { AssignerChamp<bool>(ref _ActiverBcpFiche, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }
        private C_t_occuper _OccupationSelectionnee;
        public C_t_occuper OccupationSelectionnee
        {
            get { return _OccupationSelectionnee; }
            set { AssignerChamp<C_t_occuper>(ref _OccupationSelectionnee, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }
        #endregion
        #region Données extérieures
        private VM_UneOccupation _UneOccupation;
        public VM_UneOccupation UneOccupation
        {
            get { return _UneOccupation; }
            set { AssignerChamp<VM_UneOccupation>(ref _UneOccupation, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }
        private ObservableCollection<C_t_occuper> _listOccupations = new ObservableCollection<C_t_occuper>();
        public ObservableCollection<C_t_occuper> ListOccupations
        {
            get { return _listOccupations; }
            set { _listOccupations = value; }
        }
        #endregion
        #region Commandes
        public BaseCommande cConfirmer { get; set; }
        public BaseCommande cAnnuler { get; set; }
        public BaseCommande cAjouter { get; set; }
        public BaseCommande cModifier { get; set; }
        public BaseCommande cSupprimer { get; set; }
        public BaseCommande cEssaiSelMult { get; set; }
        #endregion
        #region Constructeur VM_Occuper

        public VM_Occuper()
        {
            UneOccupation = new VM_UneOccupation();
            UneOccupation.ID = 24;
            UneOccupation.IDPat = 2;
            UneOccupation.IDCha = 2;
            UneOccupation.DateEntree = new DateTime(2020, 10, 20);
            UneOccupation.DateSortie = new DateTime(2020, 10, 23);
            UneOccupation.PrixJournalier = 49;
            ListOccupations = ChargerOccupation(chConnexion);
            ActiverUneFiche = false;
            cConfirmer = new BaseCommande(Confirmer);
            cAnnuler = new BaseCommande(Annuler);
            cAjouter = new BaseCommande(Ajouter);
            cModifier = new BaseCommande(Modifier);
            cSupprimer = new BaseCommande(Supprimer);
            cEssaiSelMult = new BaseCommande(EssaiSelMult);
        }


        #endregion
        private ObservableCollection<C_t_occuper> ChargerOccupation(string chConn)
        {
            ObservableCollection<C_t_occuper> rep = new ObservableCollection<C_t_occuper>();
            List<C_t_occuper> lTmp = new Model.G_t_occuper(chConn).Lire("IDOcc");
            foreach (C_t_occuper Tmp in lTmp)
                rep.Add(Tmp);
            return rep;
        }
        #region Fonctions boutons
        public void Confirmer()
        {
            if (nAjout == -1)
            {
                UneOccupation.ID = new Model.G_t_occuper(chConnexion).Ajouter(UneOccupation.IDPat, UneOccupation.IDCha, UneOccupation.DateEntree, UneOccupation.DateSortie, UneOccupation.PrixJournalier);
                ListOccupations.Add(new C_t_occuper(UneOccupation.ID, UneOccupation.IDPat, UneOccupation.IDCha, UneOccupation.DateEntree, UneOccupation.DateSortie, UneOccupation.PrixJournalier));
            }
            else
            {
                new Model.G_t_occuper(chConnexion).Modifier(UneOccupation.ID, UneOccupation.IDPat, UneOccupation.IDCha, UneOccupation.DateEntree, UneOccupation.DateSortie, UneOccupation.PrixJournalier);
                ListOccupations[nAjout] = new C_t_occuper(UneOccupation.ID, UneOccupation.IDPat, UneOccupation.IDCha, UneOccupation.DateEntree, UneOccupation.DateSortie, UneOccupation.PrixJournalier);
            }
            ActiverUneFiche = false;
        }
        public void Annuler()
        { ActiverUneFiche = false; }
        public void Ajouter()
        {
            UneOccupation.ID = new int();
            //UneOccupation.IDPat = 2;
            //UneOccupation.IDCha = 2;
            UneOccupation.DateEntree = new DateTime();
            UneOccupation.DateSortie = new DateTime();
            UneOccupation.PrixJournalier = new int();

            //UneOccupation = new VM_UneOccupation();
            nAjout = -1;
            ActiverUneFiche = true;
        }
        public void Modifier()
        {
            if (OccupationSelectionnee != null)
            {
                C_t_occuper Tmp = new Model.G_t_occuper(chConnexion).Lire_ID(OccupationSelectionnee.IDOcc);
                //UneOccupation = new VM_UneOccupation();
                UneOccupation.ID = Tmp.IDOcc;
                UneOccupation.IDPat = Tmp.IDPat;
                UneOccupation.IDCha = Tmp.IDCha;
                UneOccupation.DateEntree = Tmp.DateEntree;
                UneOccupation.DateSortie = Tmp.DateSortie;
                UneOccupation.PrixJournalier = Tmp.PrixJournalier;
                nAjout = ListOccupations.IndexOf(OccupationSelectionnee);
                ActiverUneFiche = true;
            }
        }
        public void Supprimer()
        {
            if (OccupationSelectionnee != null)
            {
                new Model.G_t_occuper(chConnexion).Supprimer(OccupationSelectionnee.IDOcc);
                ListOccupations.Remove(OccupationSelectionnee);
            }
        }
        public void EssaiSelMult(object lListe)
        {
            System.Collections.IList lTmp = (System.Collections.IList)lListe;
            foreach (C_t_patients p in lTmp)
            { string s = p.NomPat; }
            int nTmp = lTmp.Count;
        }


        #endregion
        public void OccupationsSelectionnee2UneOccupation()
        {
            UneOccupation.ID = OccupationSelectionnee.IDOcc;
            UneOccupation.IDPat = OccupationSelectionnee.IDPat;
            UneOccupation.IDCha = OccupationSelectionnee.IDCha;
            UneOccupation.DateEntree = OccupationSelectionnee.DateEntree;
            UneOccupation.DateSortie = OccupationSelectionnee.DateSortie;
            UneOccupation.PrixJournalier = OccupationSelectionnee.PrixJournalier;
        }
        public class VM_UneOccupation : BasePropriete
        {
            #region Donnees
            private int _id, _idPat, _idCha, _prixJournalier;
            private DateTime _dateEntree, _dateSortie;
            #endregion
            #region Accesseurs
            public int ID
            {
                get => _id;
                // réassigne la valeur à la variable lors d'un événement (ici sélection d'un autre client)
                set { AssignerChamp<int>(ref _id, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
            }
            public int IDPat
            {
                get => _idPat;
                set { AssignerChamp<int>(ref _idPat, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
            }
            public int IDCha
            {
                get => _idCha;
                set { AssignerChamp<int>(ref _idCha, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
            }

            public int PrixJournalier
            {
                get => _prixJournalier;
                set { AssignerChamp<int>(ref _prixJournalier, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
            }
            public DateTime DateEntree
            {
                get => _dateEntree;
                set { AssignerChamp<DateTime>(ref _dateEntree, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
            }
            public DateTime DateSortie
            {
                get => _dateSortie;
                set { AssignerChamp<DateTime>(ref _dateSortie, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
            }
            #endregion

            public VM_UneOccupation()
            {

            }
        }

    }
}
