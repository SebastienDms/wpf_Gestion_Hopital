using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Hopital.Model
{
    class G_t_soins : G_Base
    {
        #region Constructeurs
        public G_t_soins()
            : base()
        { }
        public G_t_soins(string sChaineConnexion)
            : base(sChaineConnexion)
        { }
        #endregion
        public int Ajouter(string NomSoin, int PrixSoin)
        { return new A_t_soins(ChaineConnexion).Ajouter(NomSoin, PrixSoin); }
        public int Modifier(int IdSoin, string NomSoin, int PrixSoin)
        { return new A_t_soins(ChaineConnexion).Modifier(IdSoin, NomSoin, PrixSoin); }
        public List<C_t_soins> Lire(string Index)
        { return new A_t_soins(ChaineConnexion).Lire(Index); }
        public C_t_soins Lire_ID(int IdSoin)
        { return new A_t_soins(ChaineConnexion).Lire_ID(IdSoin); }
        public int Supprimer(int IdSoin)
        { return new A_t_soins(ChaineConnexion).Supprimer(IdSoin); }

    }
}
