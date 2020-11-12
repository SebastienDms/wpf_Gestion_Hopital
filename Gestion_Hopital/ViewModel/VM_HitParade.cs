using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gestion_Hopital.Model;

namespace Gestion_Hopital.ViewModel
{
    public class VM_HitParade : BasePropriete
    {
        #region Données Écran
        private string chConnexion = @"Data Source=DESKTOP-GES02KU;Initial Catalog=BD_Hopital;Integrated Security=True";
        #endregion
        #region Données extérieures
        private ObservableCollection<HitParade> _listHitParade = new ObservableCollection<HitParade>();
        public ObservableCollection<HitParade> ListHitParade
        {
            get { return _listHitParade; }
            set { _listHitParade = value; }
        }
        #endregion
        public VM_HitParade()
        {
            ListHitParade = OrderByDescending(ChargerHitParades());
        }
        private ObservableCollection<HitParade> ChargerHitParades()
        {
            ObservableCollection<HitParade> lstHitParades = new ObservableCollection<HitParade>();
            var lTmpMed = new G_t_medecins(chConnexion).Lire("IDMed");

            foreach (var medecin in lTmpMed)
            {
                HitParade itemHitParade = new HitParade();

                itemHitParade.NomPrenomMed = medecin.NomMed + " " + medecin.PrenomMed;
                itemHitParade.NombreOperationMed = CountOperation(medecin.IDMed);
                lstHitParades.Add(itemHitParade);
            }

            return lstHitParades;
        }
        private int CountOperation(int idMed)
        {
            int nbrOperation = 0;
            var lTmpSoi = new G_t_soigner(chConnexion).Lire("IDSoi");

            foreach (var soigner in lTmpSoi)
            {
                if (idMed == soigner.IDMed)
                    nbrOperation++;
            }

            return nbrOperation;
        }
        private ObservableCollection<HitParade> OrderByDescending(ObservableCollection<HitParade> lHitParades)
        {
            var lTmp = lHitParades.OrderByDescending(El => El.NombreOperationMed).ToList();
            lHitParades.Clear();
            foreach (var hitParade in lTmp)
            {
                lHitParades.Add(hitParade);
            }

            return lHitParades;
        }
        public class HitParade
        {
            private string _nomPrenomMed;
            private int _nombreOperationMed;

            public string NomPrenomMed
            {
                get => _nomPrenomMed;
                set => _nomPrenomMed = value;
            }
            public int NombreOperationMed
            {
                get => _nombreOperationMed;
                set => _nombreOperationMed = value;
            }
        }
    }
}
