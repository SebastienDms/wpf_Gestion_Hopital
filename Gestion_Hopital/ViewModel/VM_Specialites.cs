using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gestion_Hopital.Model;

namespace Gestion_Hopital.ViewModel
{
    public class VM_Specialites : BasePropriete
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
        private C_t_specialites _SpecialiteSelectionnee;
        public C_t_specialites SpecialiteSelectionnee
        {
            get { return _SpecialiteSelectionnee; }
            set { AssignerChamp<C_t_specialites>(ref _SpecialiteSelectionnee, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }
        #endregion
        #region Données extérieures
        private VM_UneSpecialite _UneSpecialite;
        public VM_UneSpecialite UneSpecialite
        {
            get { return _UneSpecialite; }
            set { AssignerChamp<VM_UneSpecialite>(ref _UneSpecialite, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }
        private ObservableCollection<C_t_specialites> _listSpecialites = new ObservableCollection<C_t_specialites>();
        public ObservableCollection<C_t_specialites> ListSpecialites
        {
            get { return _listSpecialites; }
            set { _listSpecialites = value; }
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
        public VM_Specialites()
        {
            UneSpecialite = new VM_UneSpecialite();
            UneSpecialite.ID = 24;
            UneSpecialite.Nom = "Chirurgien";
            ListSpecialites = ChargerSpecialites(chConnexion);
            ActiverUneFiche = false;
            cConfirmer = new BaseCommande(Confirmer);
            cAnnuler = new BaseCommande(Annuler);
            cAjouter = new BaseCommande(Ajouter);
            cModifier = new BaseCommande(Modifier);
            cSupprimer = new BaseCommande(Supprimer);
            cEssaiSelMult = new BaseCommande(EssaiSelMult);
        }
        private ObservableCollection<C_t_specialites> ChargerSpecialites(string chConn)
        {
            ObservableCollection<C_t_specialites> rep = new ObservableCollection<C_t_specialites>();
            List<C_t_specialites> lTmp = new Model.G_t_specialites(chConn).Lire("NomSpe");
            foreach (C_t_specialites Tmp in lTmp)
                rep.Add(Tmp);
            return rep;
        }
        public void Confirmer()
        {
            if (nAjout == -1)
            {
                UneSpecialite.ID = new Model.G_t_specialites(chConnexion).Ajouter(UneSpecialite.Nom);
                ListSpecialites.Add(new C_t_specialites(UneSpecialite.ID, UneSpecialite.Nom));
            }
            else
            {
                new Model.G_t_specialites(chConnexion).Modifier(UneSpecialite.ID, UneSpecialite.Nom);
                ListSpecialites[nAjout] = new C_t_specialites(UneSpecialite.ID, UneSpecialite.Nom);
            }
            ActiverUneFiche = false;
        }
        public void Annuler()
        { ActiverUneFiche = false; }
        public void Ajouter()
        {
            UneSpecialite = new VM_UneSpecialite();
            nAjout = -1;
            ActiverUneFiche = true;
        }
        public void Modifier()
        {
            if (SpecialiteSelectionnee != null)
            {
                C_t_specialites Tmp = new Model.G_t_specialites(chConnexion).Lire_ID(SpecialiteSelectionnee.IDSpe);
                UneSpecialite = new VM_UneSpecialite();
                UneSpecialite.ID = Tmp.IDSpe;
                UneSpecialite.Nom = Tmp.NomSpe;
                nAjout = ListSpecialites.IndexOf(SpecialiteSelectionnee);
                ActiverUneFiche = true;
            }
        }
        public void Supprimer()
        {
            if (SpecialiteSelectionnee != null)
            {
                new Model.G_t_specialites(chConnexion).Supprimer(SpecialiteSelectionnee.IDSpe);
                ListSpecialites.Remove(SpecialiteSelectionnee);
            }
        }
        public void EssaiSelMult(object lListe)
        {
            System.Collections.IList lTmp = (System.Collections.IList)lListe;
            foreach (C_t_specialites p in lTmp)
            { string s = p.NomSpe; }
            int nTmp = lTmp.Count;
        }
        public void SpecialitesSelectionnee2UneSpecialite()
        {
            UneSpecialite.ID = SpecialiteSelectionnee.IDSpe;
            UneSpecialite.Nom = SpecialiteSelectionnee.NomSpe;
        }
        public class VM_UneSpecialite : BasePropriete
        {
            #region Donnees
            private int _id;
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
            #endregion
        }
    }
}
