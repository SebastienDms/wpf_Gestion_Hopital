using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gestion_Hopital.Model;


namespace Gestion_Hopital.ViewModel
{
    public class VM_Chambres : BasePropriete
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
        private C_t_chambres _ChambreSelectionnee;
        public C_t_chambres ChambreSelectionnee
        {
            get { return _ChambreSelectionnee; }
            set { AssignerChamp<C_t_chambres>(ref _ChambreSelectionnee, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }
        #endregion
        #region Données extérieures
        private VM_UneChambre _uneChambre;
        public VM_UneChambre UneChambre
        {
            get { return _uneChambre; }
            set { AssignerChamp<VM_UneChambre>(ref _uneChambre, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }
        private ObservableCollection<C_t_chambres> _listChambres = new ObservableCollection<C_t_chambres>();
        public ObservableCollection<C_t_chambres> ListChambres
        {
            get { return _listChambres; }
            set { _listChambres = value; }
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
        public VM_Chambres()
        {
            UneChambre = new VM_UneChambre();
            UneChambre.ID = 24;
            UneChambre.Nom = 001;
            UneChambre.Type = "Individuelle";
            UneChambre.QuantiteLits = 1;
            UneChambre.Etage = 0;
            UneChambre.Service = "Urgences";
            ListChambres = ChargerChambres(chConnexion);
            ActiverUneFiche = false;
            cConfirmer = new BaseCommande(Confirmer);
            cAnnuler = new BaseCommande(Annuler);
            cAjouter = new BaseCommande(Ajouter);
            cModifier = new BaseCommande(Modifier);
            cSupprimer = new BaseCommande(Supprimer);
            cEssaiSelMult = new BaseCommande(EssaiSelMult);
        }
        private ObservableCollection<C_t_chambres> ChargerChambres(string chConn)
        {
            ObservableCollection<C_t_chambres> rep = new ObservableCollection<C_t_chambres>();
            List<C_t_chambres> lTmp = new Model.G_t_chambres(chConn).Lire("NomCha");
            foreach (C_t_chambres Tmp in lTmp)
                rep.Add(Tmp);
            return rep;
        }
        public void Confirmer()
        {
            if (nAjout == -1)
            {
                UneChambre.ID = new Model.G_t_chambres(chConnexion).Ajouter(UneChambre.Nom, UneChambre.QuantiteLits, UneChambre.Type, UneChambre.Etage, UneChambre.Service);
                ListChambres.Add(new C_t_chambres(UneChambre.ID, UneChambre.Nom, UneChambre.QuantiteLits, UneChambre.Type, UneChambre.Etage, UneChambre.Service));
            }
            else
            {
                new Model.G_t_chambres(chConnexion).Modifier(UneChambre.ID, UneChambre.Nom, UneChambre.QuantiteLits, UneChambre.Type, UneChambre.Etage, UneChambre.Service);
                ListChambres[nAjout] = new C_t_chambres(UneChambre.ID, UneChambre.Nom, UneChambre.QuantiteLits, UneChambre.Type, UneChambre.Etage, UneChambre.Service);
            }
            ActiverUneFiche = false;
        }
        public void Annuler()
        { ActiverUneFiche = false; }
        public void Ajouter()
        {
            UneChambre = new VM_UneChambre();
            nAjout = -1;
            ActiverUneFiche = true;
        }
        public void Modifier()
        {
            if (ChambreSelectionnee != null)
            {
                C_t_chambres Tmp = new Model.G_t_chambres(chConnexion).Lire_ID(ChambreSelectionnee.IDCha);
                UneChambre = new VM_UneChambre();
                UneChambre.ID = Tmp.IDCha;
                UneChambre.Nom = Tmp.NomCha;
                UneChambre.Type = Tmp.TypeCha;
                UneChambre.QuantiteLits = Tmp.QuantiteLits;
                UneChambre.Etage = Tmp.EtageCha;
                UneChambre.Service = Tmp.ServiceCha;
                nAjout = ListChambres.IndexOf(ChambreSelectionnee);
                ActiverUneFiche = true;
            }
        }
        public void Supprimer()
        {
            if (ChambreSelectionnee != null)
            {
                new Model.G_t_chambres(chConnexion).Supprimer(ChambreSelectionnee.IDCha);
                ListChambres.Remove(ChambreSelectionnee);
            }
        }
        public void EssaiSelMult(object lListe)
        {
            System.Collections.IList lTmp = (System.Collections.IList)lListe;
            foreach (C_t_chambres p in lTmp)
            { int s = p.NomCha; }
            int nTmp = lTmp.Count;
        }
        public void ChambresSelectionnee2UneChambre()
        {
            UneChambre.ID = ChambreSelectionnee.IDCha;
            UneChambre.Nom = ChambreSelectionnee.NomCha;
            UneChambre.Type = ChambreSelectionnee.TypeCha;
            UneChambre.QuantiteLits = ChambreSelectionnee.QuantiteLits;
            UneChambre.Etage = ChambreSelectionnee.EtageCha;
            UneChambre.Service = ChambreSelectionnee.ServiceCha;
        }
        public class VM_UneChambre : BasePropriete
        {
            #region Donnees
            private int _id, _nom, _quantiteLits, _etage;
            private string  _type, _service;
            #endregion
            #region Accesseurs
            public int ID
            {
                get => _id;
                // réassigne la valeur à la variable lors d'un événement (ici sélection d'un autre client)
                set { AssignerChamp<int>(ref _id, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
            }
            public int Nom
            {
                get => _nom;
                set { AssignerChamp<int>(ref _nom, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
            }
            public int QuantiteLits
            {
                get => _quantiteLits;
                set { AssignerChamp<int>(ref _quantiteLits, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
            }
            public int Etage
            {
                get => _etage;
                set { AssignerChamp<int>(ref _etage, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
            }
            public string Type
            {
                get => _type;
                set { AssignerChamp<string>(ref _type, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
            }
            public string Service
            {
                get => _service;
                set { AssignerChamp<string>(ref _service, value, System.Reflection.MethodBase.GetCurrentMethod().Name); }
            }
            #endregion
        }
    }
}
