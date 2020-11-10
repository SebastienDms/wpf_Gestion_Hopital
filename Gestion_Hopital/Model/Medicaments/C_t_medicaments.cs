using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Hopital.Model
{
    public class C_t_medicaments
    {
        #region Données
        private int _idMedi;
        private string _nomMedi;
        private int _quantiteMedi;
        private int _prixMedi;
        #endregion
        #region Accesseurs
        public int IDMedi
        {
            get => _idMedi;
            set => _idMedi = value;
        }

        public string NomMedi
        {
            get => _nomMedi;
            set => _nomMedi = value;
        }

        public int QuantiteMedi
        {
            get => _quantiteMedi;
            set => _quantiteMedi = value;
        }

        public int PrixMedi
        {
            get => _prixMedi;
            set => _prixMedi = value;
        }
        #endregion
        #region Constructeurs
        public C_t_medicaments()
        {}
        public C_t_medicaments(string NomMedi, int QuantiteMedi, int PrixMedi)
        {
            this.NomMedi = NomMedi;
            this.QuantiteMedi = QuantiteMedi;
            this.PrixMedi = PrixMedi;
        }

        public C_t_medicaments(int IDMedi, string NomMedi, int QuantiteMedi, int PrixMedi)
            : this(NomMedi, QuantiteMedi, PrixMedi)
        {
            this.IDMedi = IDMedi;
        }
        #endregion
    }
}
