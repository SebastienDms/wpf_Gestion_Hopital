using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Hopital.Model
{
    public class C_t_soins
    {
        #region Données
        private int _idSoin;
        private string _nomSoin;
        private int _prixSoin;
        #endregion
        #region Accesseurs
        public int IDSoin
        {
            get => _idSoin;
            set => _idSoin = value;
        }

        public string NomSoin
        {
            get => _nomSoin;
            set => _nomSoin = value;
        }

        public int PrixSoin
        {
            get => _prixSoin;
            set => _prixSoin = value;
        }

        #endregion
        #region Constructeurs
        public C_t_soins()
        {}

        public C_t_soins(string NomSoin, int PrixSoin)
        {
            this.NomSoin = NomSoin;
            this.PrixSoin = PrixSoin;
        }

        public C_t_soins(int IdSoin, string NomSoin, int PrixSoin)
            : this(NomSoin, PrixSoin)
        {
            IDSoin = IdSoin;
        }
        #endregion
    }
}
