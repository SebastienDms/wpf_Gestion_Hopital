using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Gestion_Hopital.Model;

namespace Gestion_Hopital.ViewModel
{
    public class VM_Patients : BasePropriete
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
        private C_t_patients _PersonneSelectionnee;
        public C_t_patients PersonneSelectionnee
        {
            get { return _PersonneSelectionnee; }
            set { AssignerChamp<C_t_patients>(ref _PersonneSelectionnee, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }
        #endregion
        #region Données extérieures
        private VM_UnClient _UnClient;
        public VM_UnClient UnClient
        {
            get { return _UnClient; }
            set { AssignerChamp<VM_UnClient>(ref _UnClient, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }
        private ObservableCollection<C_t_patients> _listClients = new ObservableCollection<C_t_patients>();
        public ObservableCollection<C_t_patients> ListClients
        {
            get { return _listClients; }
            set { _listClients = value; }
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
        public VM_Patients()
        {
            UnClient = new VM_UnClient();
            UnClient.ID = 24;
            UnClient.Nom = "Winch";
            UnClient.Pre = "Largo";
            UnClient.Adr = "Avenue des BD, 7 - 1000 Bruxelles";
            UnClient.Tel = 0495123456;
            UnClient.Mail = "largo.winch@gmail.com";
            ListClients = ChargerPersonnes(chConnexion);
            ActiverUneFiche = false;
            cConfirmer = new BaseCommande(Confirmer);
            cAnnuler = new BaseCommande(Annuler);
            cAjouter = new BaseCommande(Ajouter);
            cModifier = new BaseCommande(Modifier);
            cSupprimer = new BaseCommande(Supprimer);
            cEssaiSelMult = new BaseCommande(EssaiSelMult);
        }
        private ObservableCollection<C_t_patients> ChargerPersonnes(string chConn)
        {
            ObservableCollection<C_t_patients> rep = new ObservableCollection<C_t_patients>();
            List<C_t_patients> lTmp = new Model.G_t_patients(chConn).Lire("NomPat");
            foreach (C_t_patients Tmp in lTmp)
                rep.Add(Tmp);
            return rep;
        }
        public void Confirmer()
        {
            if (nAjout == -1)
            {
                UnClient.ID = new Model.G_t_patients(chConnexion).Ajouter(UnClient.Nom, UnClient.Pre, UnClient.Adr, UnClient.Tel, UnClient.Mail);
                ListClients.Add(new C_t_patients(UnClient.ID, UnClient.Nom, UnClient.Pre, UnClient.Adr, UnClient.Tel, UnClient.Mail));
            }
            else
            {
                new Model.G_t_patients(chConnexion).Modifier(UnClient.ID, UnClient.Nom, UnClient.Pre, UnClient.Adr, UnClient.Tel, UnClient.Mail);
                ListClients[nAjout] = new C_t_patients(UnClient.ID, UnClient.Nom, UnClient.Pre, UnClient.Adr, UnClient.Tel, UnClient.Mail);
            }
            ActiverUneFiche = false;
        }
        public void Annuler()
        { ActiverUneFiche = false; }
        public void Ajouter()
        {
            UnClient = new VM_UnClient();
            nAjout = -1;
            ActiverUneFiche = true;
        }
        public void Modifier()
        {
            if (PersonneSelectionnee != null)
            {
                C_t_patients Tmp = new Model.G_t_patients(chConnexion).Lire_ID(PersonneSelectionnee.IDPat);
                UnClient = new VM_UnClient();
                UnClient.ID = Tmp.IDPat;
                UnClient.Nom = Tmp.NomPat;
                UnClient.Pre = Tmp.PrenomPat;
                UnClient.Adr = Tmp.AdressePat;
                UnClient.Tel = Tmp.GSMPat;
                UnClient.Mail = Tmp.MailPat;
                nAjout = ListClients.IndexOf(PersonneSelectionnee);
                ActiverUneFiche = true;
            }
        }
        public void Supprimer()
        {
            if (PersonneSelectionnee != null)
            {
                new Model.G_t_patients(chConnexion).Supprimer(PersonneSelectionnee.IDPat);
                ListClients.Remove(PersonneSelectionnee);
            }
        }
        public void EssaiSelMult(object lListe)
        {
            System.Collections.IList lTmp = (System.Collections.IList)lListe;
            foreach (C_t_patients p in lTmp)
            { string s = p.NomPat; }
            int nTmp = lTmp.Count;
        }
        public void ClientsSelectionnee2UnClient()
        {
            UnClient.ID = PersonneSelectionnee.IDPat;
            UnClient.Nom = PersonneSelectionnee.NomPat;
            UnClient.Pre = PersonneSelectionnee.PrenomPat;
            UnClient.Adr = PersonneSelectionnee.AdressePat;
            UnClient.Tel = PersonneSelectionnee.GSMPat;
            //if (PersonneSelectionnee.MailPat == "")
            //{
            //    UnClient.Mail = "";
            //}
            //else
            //{
            UnClient.Mail = PersonneSelectionnee.MailPat; //"test";
            //}
        }
        public class VM_UnClient : BasePropriete
        {
            #region Donnees
            private int _id, _tel;
            private string _nom, _pre, _adr, _mail;
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

            public string Adr
            {
                get => _adr;
                set { AssignerChamp<string>(ref _adr, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
            }

            public int Tel
            {
                get => _tel;
                set { AssignerChamp<int>(ref _tel, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
            }

            public string Mail
            {
                get => _mail;
                set { AssignerChamp<string>(ref _mail, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
            }
            #endregion
        }
    }
}
