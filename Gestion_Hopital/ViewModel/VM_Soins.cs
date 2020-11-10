using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gestion_Hopital.Model;

namespace Gestion_Hopital.ViewModel
{
    public class VM_Soins : BasePropriete
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
        private C_t_soins _soinSelectionne;
        public C_t_soins SoinSelectionne
        {
            get { return _soinSelectionne; }
            set { AssignerChamp<C_t_soins>(ref _soinSelectionne, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }
        #endregion
        #region Données extérieures
        private VM_UnSoin _unSoin;
        public VM_UnSoin UnSoin
        {
            get { return _unSoin; }
            set { AssignerChamp<VM_UnSoin>(ref _unSoin, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }
        private ObservableCollection<C_t_soins> _listSoins = new ObservableCollection<C_t_soins>();
        public ObservableCollection<C_t_soins> ListSoins
        {
            get { return _listSoins; }
            set { _listSoins = value; }
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
        public VM_Soins()
        {
            UnSoin = new VM_UnSoin();
            UnSoin.ID = 24;
            UnSoin.Nom = "Donner une piqure";
            UnSoin.Prix = 25;
            ListSoins = ChargerSoins(chConnexion);
            ActiverUneFiche = false;
            cConfirmer = new BaseCommande(Confirmer);
            cAnnuler = new BaseCommande(Annuler);
            cAjouter = new BaseCommande(Ajouter);
            cModifier = new BaseCommande(Modifier);
            cSupprimer = new BaseCommande(Supprimer);
            cEssaiSelMult = new BaseCommande(EssaiSelMult);
        }
        private ObservableCollection<C_t_soins> ChargerSoins(string chConn)
        {
            ObservableCollection<C_t_soins> rep = new ObservableCollection<C_t_soins>();
            List<C_t_soins> lTmp = new Model.G_t_soins(chConn).Lire("NomSoin");
            foreach (C_t_soins Tmp in lTmp)
                rep.Add(Tmp);
            return rep;
        }
        public void Confirmer()
        {
            if (nAjout == -1)
            {
                UnSoin.ID = new Model.G_t_soins(chConnexion).Ajouter(UnSoin.Nom, UnSoin.Prix);
                ListSoins.Add(new C_t_soins(UnSoin.ID, UnSoin.Nom, UnSoin.Prix));
            }
            else
            {
                new Model.G_t_soins(chConnexion).Modifier(UnSoin.ID, UnSoin.Nom, UnSoin.Prix);
                ListSoins[nAjout] = new C_t_soins(UnSoin.ID, UnSoin.Nom, UnSoin.Prix);
            }
            ActiverUneFiche = false;
        }
        public void Annuler()
        { ActiverUneFiche = false; }
        public void Ajouter()
        {
            UnSoin = new VM_UnSoin();
            nAjout = -1;
            ActiverUneFiche = true;
        }
        public void Modifier()
        {
            if (SoinSelectionne != null)
            {
                C_t_soins Tmp = new G_t_soins(chConnexion).Lire_ID(SoinSelectionne.IDSoin);
                UnSoin = new VM_UnSoin();
                UnSoin.ID = Tmp.IDSoin;
                UnSoin.Nom = Tmp.NomSoin;
                UnSoin.Prix = Tmp.PrixSoin;
                nAjout = ListSoins.IndexOf(SoinSelectionne);
                ActiverUneFiche = true;
            }
        }
        public void Supprimer()
        {
            if (SoinSelectionne != null)
            {
                new Model.G_t_soins(chConnexion).Supprimer(SoinSelectionne.IDSoin);
                ListSoins.Remove(SoinSelectionne);
            }
        }
        public void EssaiSelMult(object lListe)
        {
            System.Collections.IList lTmp = (System.Collections.IList)lListe;
            foreach (C_t_patients p in lTmp)
            { string s = p.NomPat; }
            int nTmp = lTmp.Count;
        }
        public void SoinsSelectionnes2UnClient()
        {
            UnSoin.ID = SoinSelectionne.IDSoin;
            UnSoin.Nom = SoinSelectionne.NomSoin;
            UnSoin.Prix = SoinSelectionne.PrixSoin;
        }
        public class VM_UnSoin : BasePropriete
        {
            #region Donnees
            private int _id, _prix;
            private string _nom;
            #endregion
            #region Accesseurs
            public int ID
            {
                get => _id;
                // réassigne la valeur à la variable lors d'un événement (ici sélection d'un autre client)
                set { AssignerChamp<int>(ref _id, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
            }
            public string Nom
            {
                get => _nom;
                set { AssignerChamp<string>(ref _nom, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
            }
            public int Prix
            {
                get => _prix;
                set { AssignerChamp<int>(ref _prix, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
            }
            #endregion
        }

    }
}
