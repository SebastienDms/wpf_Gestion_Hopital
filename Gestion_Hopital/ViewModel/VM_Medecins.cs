using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gestion_Hopital.Model;

namespace Gestion_Hopital.ViewModel
{
    public class VM_Medecins : BasePropriete
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
        private C_t_medecins _MedecinSelectionne;
        public C_t_medecins MedecinSelectionne
        {
            get { return _MedecinSelectionne; }
            set { AssignerChamp<C_t_medecins>(ref _MedecinSelectionne, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }
        #endregion
        #region Données extérieures
        private VM_UnMedecin _UnMedecin;
        public VM_UnMedecin UnMedecin
        {
            get { return _UnMedecin; }
            set { AssignerChamp<VM_UnMedecin>(ref _UnMedecin, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }
        private ObservableCollection<C_t_medecins> _listMedecins = new ObservableCollection<C_t_medecins>();
        public ObservableCollection<C_t_medecins> ListMedecins
        {
            get { return _listMedecins; }
            set { _listMedecins = value; }
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
        public VM_Medecins()
        {
            UnMedecin = new VM_UnMedecin();
            UnMedecin.ID = 24;
            UnMedecin.Nom = "Winch";
            UnMedecin.Pre = "Largo";
            UnMedecin.Tel = 0495123456;
            UnMedecin.IdSpe = 1;
            UnMedecin.DateNais = new DateTime(1980, 01, 26);
            ListMedecins = ChargerMedecins(chConnexion);
            ActiverUneFiche = false;
            cConfirmer = new BaseCommande(Confirmer);
            cAnnuler = new BaseCommande(Annuler);
            cAjouter = new BaseCommande(Ajouter);
            cModifier = new BaseCommande(Modifier);
            cSupprimer = new BaseCommande(Supprimer);
            cEssaiSelMult = new BaseCommande(EssaiSelMult);
        }
        private ObservableCollection<C_t_medecins> ChargerMedecins(string chConn)
        {
            ObservableCollection<C_t_medecins> rep = new ObservableCollection<C_t_medecins>();
            List<C_t_medecins> lTmp = new Model.G_t_medecins(chConn).Lire("NomMed");
            foreach (C_t_medecins Tmp in lTmp)
                rep.Add(Tmp);
            return rep;
        }
        public void Confirmer()
        {
            if (nAjout == -1)
            {
                UnMedecin.ID = new Model.G_t_medecins(chConnexion).Ajouter(UnMedecin.Nom, UnMedecin.Pre, UnMedecin.Tel, UnMedecin.IdSpe, UnMedecin.DateNais);
                ListMedecins.Add(new C_t_medecins(UnMedecin.ID, UnMedecin.Nom, UnMedecin.Pre, UnMedecin.Tel, UnMedecin.IdSpe, UnMedecin.DateNais));
            }
            else
            {
                new Model.G_t_medecins(chConnexion).Modifier(UnMedecin.ID, UnMedecin.Nom, UnMedecin.Pre, UnMedecin.Tel, UnMedecin.IdSpe, UnMedecin.DateNais);
                ListMedecins[nAjout] = new C_t_medecins(UnMedecin.ID, UnMedecin.Nom, UnMedecin.Pre, UnMedecin.Tel, UnMedecin.IdSpe, UnMedecin.DateNais);
            }
            ActiverUneFiche = false;
        }
        public void Annuler()
        { ActiverUneFiche = false; }
        public void Ajouter()
        {
            UnMedecin = new VM_UnMedecin();
            nAjout = -1;
            ActiverUneFiche = true;
        }
        public void Modifier()
        {
            if (MedecinSelectionne != null)
            {
                C_t_medecins Tmp = new Model.G_t_medecins(chConnexion).Lire_ID(MedecinSelectionne.IDMed);
                UnMedecin = new VM_UnMedecin();
                UnMedecin.ID = Tmp.IDMed;
                UnMedecin.Nom = Tmp.NomMed;
                UnMedecin.Pre = Tmp.PrenomMed;
                UnMedecin.Tel = Tmp.GSMMed;
                UnMedecin.IdSpe = Tmp.IDSpe;
                UnMedecin.DateNais = Tmp.DateNaisMed;
                nAjout = ListMedecins.IndexOf(MedecinSelectionne);
                ActiverUneFiche = true;
            }
        }
        public void Supprimer()
        {
            if (MedecinSelectionne != null)
            {
                new Model.G_t_patients(chConnexion).Supprimer(MedecinSelectionne.IDMed);
                ListMedecins.Remove(MedecinSelectionne);
            }
        }
        public void EssaiSelMult(object lListe)
        {
            System.Collections.IList lTmp = (System.Collections.IList)lListe;
            foreach (C_t_patients p in lTmp)
            { string s = p.NomPat; }
            int nTmp = lTmp.Count;
        }
        public void MedecinsSelectionnee2UnMedecin()
        {
            UnMedecin.ID = MedecinSelectionne.IDMed;
            UnMedecin.Nom = MedecinSelectionne.NomMed;
            UnMedecin.Pre = MedecinSelectionne.PrenomMed;
            UnMedecin.Tel = MedecinSelectionne.GSMMed;
            UnMedecin.IdSpe = MedecinSelectionne.IDSpe;
            UnMedecin.DateNais = MedecinSelectionne.DateNaisMed;
        }
        public class VM_UnMedecin : BasePropriete
        {
            #region Donnees
            private int _id, _tel, _idSpe;
            private string _nom, _pre;
            private DateTime? _dateNais;
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

            public string Pre
            {
                get => _pre;
                set { AssignerChamp<string>(ref _pre, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
            }

            public int Tel
            {
                get => _tel;
                set { AssignerChamp<int>(ref _tel, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
            }

            public int IdSpe
            {
                get => _idSpe;
                set { AssignerChamp<int>(ref _idSpe, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
            }

            public DateTime? DateNais
            {
                get => _dateNais;
                set { AssignerChamp<DateTime?>(ref _dateNais, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
            }

            #endregion
        }
    }
}
