using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gestion_Hopital.Model;

namespace Gestion_Hopital.ViewModel
{
    public class VM_Medicaments : BasePropriete
    {
        #region Données Écran
        private string chConnexion = @"Data Source=DESKTOP-KJ2N7R1;Initial Catalog=BD_Hopital_2;Integrated Security=True";
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
        private C_t_medicaments _MedicamentSelectionne;
        public C_t_medicaments MedicamentSelectionne
        {
            get { return _MedicamentSelectionne; }
            set { AssignerChamp<C_t_medicaments>(ref _MedicamentSelectionne, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }
        #endregion
        #region Données extérieures
        private VM_UnMedicament _UnMedicament;
        public VM_UnMedicament UnMedicament
        {
            get { return _UnMedicament; }
            set { AssignerChamp<VM_UnMedicament>(ref _UnMedicament, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }
        private ObservableCollection<C_t_medicaments> _listMedicaments = new ObservableCollection<C_t_medicaments>();
        public ObservableCollection<C_t_medicaments> ListMedicaments
        {
            get { return _listMedicaments; }
            set { _listMedicaments = value; }
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
        public VM_Medicaments()
        {
            UnMedicament = new VM_UnMedicament();
            UnMedicament.ID = 2;
            UnMedicament.Nom = "Dafalgan";
            UnMedicament.Quantite = 10;
            UnMedicament.Prix = 29;
            ListMedicaments = ChargerMedicaments(chConnexion);
            ActiverUneFiche = false;
            cConfirmer = new BaseCommande(Confirmer);
            cAnnuler = new BaseCommande(Annuler);
            cAjouter = new BaseCommande(Ajouter);
            cModifier = new BaseCommande(Modifier);
            cSupprimer = new BaseCommande(Supprimer);
            cEssaiSelMult = new BaseCommande(EssaiSelMult);
        }
        private ObservableCollection<C_t_medicaments> ChargerMedicaments(string chConn)
        {
            ObservableCollection<C_t_medicaments> rep = new ObservableCollection<C_t_medicaments>();
            List<C_t_medicaments> lTmp = new Model.G_t_medicaments(chConn).Lire("NomMedi");
            foreach (C_t_medicaments Tmp in lTmp)
                rep.Add(Tmp);
            return rep;
        }
        public void Confirmer()
        {
            if (nAjout == -1)
            {
                UnMedicament.ID = new Model.G_t_medicaments(chConnexion).Ajouter(UnMedicament.Nom, UnMedicament.Quantite, UnMedicament.Prix);
                ListMedicaments.Add(new C_t_medicaments(UnMedicament.ID, UnMedicament.Nom, UnMedicament.Quantite, UnMedicament.Prix));
            }
            else
            {
                new Model.G_t_medicaments(chConnexion).Modifier(UnMedicament.ID, UnMedicament.Nom, UnMedicament.Quantite, UnMedicament.Prix);
                ListMedicaments[nAjout] = new C_t_medicaments(UnMedicament.ID, UnMedicament.Nom, UnMedicament.Quantite, UnMedicament.Prix);
            }
            ActiverUneFiche = false;
        }
        public void Annuler()
        { ActiverUneFiche = false; }
        public void Ajouter()
        {
            UnMedicament = new VM_UnMedicament();
            nAjout = -1;
            ActiverUneFiche = true;
        }
        public void Modifier()
        {
            if (MedicamentSelectionne != null)
            {
                C_t_medicaments Tmp = new Model.G_t_medicaments(chConnexion).Lire_ID(MedicamentSelectionne.IDMedi);
                UnMedicament = new VM_UnMedicament();
                UnMedicament.ID = Tmp.IDMedi;
                UnMedicament.Nom = Tmp.NomMedi;
                UnMedicament.Quantite = Tmp.QuantiteMedi;
                UnMedicament.Prix = Tmp.PrixMedi;
                nAjout = ListMedicaments.IndexOf(MedicamentSelectionne);
                ActiverUneFiche = true;
            }
        }
        public void Supprimer()
        {
            if (MedicamentSelectionne != null)
            {
                new Model.G_t_medicaments(chConnexion).Supprimer(MedicamentSelectionne.IDMedi);
                ListMedicaments.Remove(MedicamentSelectionne);
            }
        }
        public void EssaiSelMult(object lListe)
        {
            System.Collections.IList lTmp = (System.Collections.IList)lListe;
            foreach (C_t_medicaments p in lTmp)
            { string s = p.NomMedi; }
            int nTmp = lTmp.Count;
        }
        public void MedicamentsSelectionnee2UnMedicament()
        {
            UnMedicament.ID = MedicamentSelectionne.IDMedi;
            UnMedicament.Nom = MedicamentSelectionne.NomMedi;
            UnMedicament.Quantite = MedicamentSelectionne.QuantiteMedi;
            UnMedicament.Prix = MedicamentSelectionne.PrixMedi;
        }
        public class VM_UnMedicament : BasePropriete
        {
            #region Donnees
            private int _id, _quantite, _prix;
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
            public int Quantite
            {
                get => _quantite;
                set { AssignerChamp<int>(ref _quantite, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
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
