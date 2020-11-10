using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gestion_Hopital.Model;

namespace Gestion_Hopital.ViewModel
{
    public class VM_Soigner : BasePropriete
    {
        #region Données Écran
        private string chConnexion = @"Data Source=DESKTOP-GES02KU;Initial Catalog=BD_Hopital;Integrated Security=True";
        private int nAjout;
        private bool _ActiverUneFicheSoi;
        public bool ActiverUneFicheSoi
        {
            get { return _ActiverUneFicheSoi; }
            set
            {
                AssignerChamp<bool>(ref _ActiverUneFicheSoi, value, System.Reflection.MethodBase.GetCurrentMethod().Name);
                ActiverBcpFicheSoi = !ActiverUneFicheSoi;
            }
        }
        private bool _ActiverBcpFicheSoi;
        public bool ActiverBcpFicheSoi
        {
            get { return _ActiverBcpFicheSoi; }
            set { AssignerChamp<bool>(ref _ActiverBcpFicheSoi, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }
        private C_t_soigner _SoignerSelectionne;
        public C_t_soigner SoignerSelectionne
        {
            get { return _SoignerSelectionne; }
            set { AssignerChamp<C_t_soigner>(ref _SoignerSelectionne, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }
        #endregion
        #region Données extérieures
        private VM_UnSoigner _UnSoigner;
        public VM_UnSoigner UnSoigner
        {
            get { return _UnSoigner; }
            set { AssignerChamp<VM_UnSoigner>(ref _UnSoigner, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }
        private ObservableCollection<C_t_soigner> _listSoigners = new ObservableCollection<C_t_soigner>();
        public ObservableCollection<C_t_soigner> ListSoigners
        {
            get { return _listSoigners; }
            set { _listSoigners = value; }
        }
        #endregion
        #region Commandes
        public BaseCommande cConfirmerSoi { get; set; }
        public BaseCommande cAnnulerSoi { get; set; }
        public BaseCommande cAjouterSoi { get; set; }
        public BaseCommande cModifierSoi { get; set; }
        public BaseCommande cSupprimerSoi { get; set; }
        public BaseCommande cEssaiSelMultSoi { get; set; }
        #endregion
        public VM_Soigner()
        {
            UnSoigner = new VM_UnSoigner();
            UnSoigner.ID = 24;
            UnSoigner.IDMed = 3;
            UnSoigner.IDPat = 3;
            UnSoigner.IDType = 1;
            UnSoigner.IDMedi= 1;
            UnSoigner.DateOperation = new DateTime(2020, 10, 22);
            ListSoigners = ChargerSoigner(chConnexion);
            ActiverUneFicheSoi = false;
            cConfirmerSoi = new BaseCommande(Confirmer);
            cAnnulerSoi = new BaseCommande(Annuler);
            cAjouterSoi = new BaseCommande(Ajouter);
            cModifierSoi = new BaseCommande(Modifier);
            cSupprimerSoi = new BaseCommande(Supprimer);
            cEssaiSelMultSoi = new BaseCommande(EssaiSelMult);
        }
        private ObservableCollection<C_t_soigner> ChargerSoigner(string chConn)
        {
            ObservableCollection<C_t_soigner> rep = new ObservableCollection<C_t_soigner>();
            List<C_t_soigner> lTmp = new Model.G_t_soigner(chConn).Lire("IDSoi");
            foreach (C_t_soigner Tmp in lTmp)
                rep.Add(Tmp);
            return rep;
        }
        public void Confirmer()
        {
            if (nAjout == -1)
            {
                UnSoigner.ID = new Model.G_t_soigner(chConnexion).Ajouter(UnSoigner.IDMed, UnSoigner.IDPat, UnSoigner.IDType, UnSoigner.IDMedi, UnSoigner.DateOperation);
                ListSoigners.Add(new C_t_soigner(UnSoigner.ID, UnSoigner.IDMed, UnSoigner.IDPat, UnSoigner.IDType, UnSoigner.IDMedi, UnSoigner.DateOperation));
            }
            else
            {
                new Model.G_t_soigner(chConnexion).Modifier(UnSoigner.ID, UnSoigner.IDMed, UnSoigner.IDPat, UnSoigner.IDType, UnSoigner.IDMedi, UnSoigner.DateOperation);
                ListSoigners[nAjout] = new C_t_soigner(UnSoigner.ID, UnSoigner.IDMed, UnSoigner.IDPat, UnSoigner.IDType, UnSoigner.IDMedi, UnSoigner.DateOperation);
            }
            ActiverUneFicheSoi = false;
        }
        public void Annuler()
        { ActiverUneFicheSoi = false; }
        public void Ajouter()
        {
            UnSoigner = new VM_UnSoigner();
            nAjout = -1;
            ActiverUneFicheSoi = true;
        }
        public void Modifier()
        {
            if (SoignerSelectionne != null)
            {
                C_t_soigner Tmp = new Model.G_t_soigner(chConnexion).Lire_ID(SoignerSelectionne.IDSoi);
                UnSoigner = new VM_UnSoigner();
                UnSoigner.ID = Tmp.IDSoi;
                UnSoigner.IDMed = Tmp.IDMed;
                UnSoigner.IDPat = Tmp.IDPat;
                UnSoigner.IDType = Tmp.IDTyp;
                UnSoigner.IDMedi = Tmp.IDMedi;
                UnSoigner.DateOperation = Tmp.DateOperation;
                nAjout = ListSoigners.IndexOf(SoignerSelectionne);
                ActiverUneFicheSoi = true;
            }
        }
        public void Supprimer()
        {
            if (SoignerSelectionne != null)
            {
                new Model.G_t_soigner(chConnexion).Supprimer(SoignerSelectionne.IDMed);
                ListSoigners.Remove(SoignerSelectionne);
            }
        }
        public void EssaiSelMult(object lListe)
        {
            System.Collections.IList lTmp = (System.Collections.IList)lListe;
            foreach (C_t_soigner p in lTmp)
            { int s = p.IDSoi; }
            int nTmp = lTmp.Count;
        }
        public void SoignersSelectionnee2UnSoigner()
        {
            UnSoigner.ID = SoignerSelectionne.IDSoi;
            UnSoigner.IDMed = SoignerSelectionne.IDMed;
            UnSoigner.IDPat = SoignerSelectionne.IDPat;
            UnSoigner.IDType = SoignerSelectionne.IDTyp;
            UnSoigner.IDMedi = SoignerSelectionne.IDMedi;
            UnSoigner.DateOperation = SoignerSelectionne.DateOperation;
        }
        public class VM_UnSoigner : BasePropriete
        {
            #region Donnees
            private int _id, _idMed, _idPat, _idType, _idMedi;
            private DateTime _dateOperation;
            #endregion
            #region Accesseurs
            public int ID
            {
                get => _id;
                // réassigne la valeur à la variable lors d'un événement (ici sélection d'un autre client)
                set { AssignerChamp<int>(ref _id, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
            }
            public int IDMed
            {
                get => _idMed;
                set { AssignerChamp<int>(ref _idMed, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
            }

            public int IDPat
            {
                get => _idPat;
                set { AssignerChamp<int>(ref _idPat, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
            }
            public int IDType
            {
                get => _idType;
                set { AssignerChamp<int>(ref _idType, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
            }
            public int IDMedi
            {
                get => _idMedi;
                set { AssignerChamp<int>(ref _idMedi, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
            }

            public DateTime DateOperation
            {
                get => _dateOperation;
                set { AssignerChamp<DateTime>(ref _dateOperation, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
            }

            #endregion
        }

    }
}
