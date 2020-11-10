using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gestion_Hopital.Model;
using Gestion_Hopital.Dashboard;

namespace Gestion_Hopital.ViewModel
{
    public class VM_DispoChambre : BasePropriete
    {
        #region Données Écran
        private string chConnexion = @"Data Source=DESKTOP-GES02KU;Initial Catalog=BD_Hopital;Integrated Security=True";
        private int nAjout, _i = 0;
        #endregion

        #region Données extérieures
        private ObservableCollection<SemaineDispo> _listDispo = new ObservableCollection<SemaineDispo>();
        public ObservableCollection<SemaineDispo> ListDispo
        {
            get { return _listDispo; }
            set { _listDispo = value; }
        }
        #endregion

        public VM_DispoChambre()
        {
            ListDispo = ChargerTitres(1);
        }
        public VM_DispoChambre(int idChambreSelectionnee)
        {
            ListDispo = ChargerTitres(idChambreSelectionnee);
        }
        private ObservableCollection<SemaineDispo> ChargerTitres(int idChambre)
        {
            ObservableCollection<SemaineDispo> titleCollection = new ObservableCollection<SemaineDispo>();

            for (int i = 0; i < 4; i++)
            {
                titleCollection.Add(ObtenirLitDispo(idChambre));
                _i++;
            }

            return titleCollection;
        }
        public class SemaineDispo
        {
            private int _lundi, _mardi, _mercredi, _jeudi, _vendredi, _samedi, _dimanche;
            
            public int Lundi
            {
                get => _lundi;
                set => _lundi = value;
            }
            public int Mardi
            {
                get => _mardi;
                set => _mardi = value;
            }
            public int Mercredi
            {
                get => _mercredi;
                set => _mercredi = value;
            }
            public int Jeudi
            {
                get => _jeudi;
                set => _jeudi = value;
            }
            public int Vendredi
            {
                get => _vendredi;
                set => _vendredi = value;
            }
            public int Samedi
            {
                get => _samedi;
                set => _samedi = value;
            }
            public int Dimanche
            {
                get => _dimanche;
                set => _dimanche = value;
            }
        }

        public SemaineDispo ObtenirLitDispo(int idChambre)
        {
            int nbrLit = 0;
            DateTime premier_J_Semaine = GestionDate.Date_Premier_J_Semaine(_i);
            SemaineDispo semaineDispo = new SemaineDispo();

            List<C_t_chambres> lTmp_Cha = new G_t_chambres(chConnexion).Lire("IDCha");
            foreach (C_t_chambres c in lTmp_Cha)
            {
                if (c.IDCha == idChambre)
                {
                    nbrLit = c.QuantiteLits;
                    break;
                }
            }

            if (idChambre != 0)
            {
                int[] Lit_occupe = new int[] {0, 0, 0, 0, 0, 0, 0};
                List<C_t_occuper> lTmp_Occ = new G_t_occuper(chConnexion).Lire("IDOcc");

                for (int j = 0; j < 7; j++)
                {
                    DateTime Date_Comp = premier_J_Semaine.AddDays(j);
                    foreach (C_t_occuper o in lTmp_Occ)
                    {
                        if (o.IDCha == idChambre &&
                            (Date_Comp.Date >= o.DateEntree.Date && Date_Comp.Date <= o.DateSortie.Date))
                        {
                            Lit_occupe[j] += 1;
                        }
                    }
                }

                semaineDispo.Lundi = (nbrLit - Lit_occupe[0]);
                semaineDispo.Mardi = (nbrLit - Lit_occupe[1]);
                semaineDispo.Mercredi = (nbrLit - Lit_occupe[2]);
                semaineDispo.Jeudi = (nbrLit - Lit_occupe[3]);
                semaineDispo.Vendredi = (nbrLit - Lit_occupe[4]);
                semaineDispo.Samedi = (nbrLit - Lit_occupe[5]);
                semaineDispo.Dimanche = (nbrLit - Lit_occupe[6]);
            }

            return semaineDispo;
        }
    }
}
