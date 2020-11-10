using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Hopital.Model
{
    public class G_t_medicaments : G_Base
    {
        #region Constructeurs
        public G_t_medicaments()
            : base()
        { }
        public G_t_medicaments(string sChaineConnexion)
            : base(sChaineConnexion)
        { }
        #endregion
        public int Ajouter(string NomMedi, int QuantiteMedi, int PrixMedi)
        { return new A_t_medicaments(ChaineConnexion).Ajouter(NomMedi, QuantiteMedi, PrixMedi); }
        public int Modifier(int IDMedi, string NomMedi, int QuantiteMedi, int PrixMedi)
        { return new A_t_medicaments(ChaineConnexion).Modifier(IDMedi, NomMedi, QuantiteMedi, PrixMedi); }
        public List<C_t_medicaments> Lire(string Index)
        { return new A_t_medicaments(ChaineConnexion).Lire(Index); }
        public C_t_medicaments Lire_ID(int IDMedi)
        { return new A_t_medicaments(ChaineConnexion).Lire_ID(IDMedi); }
        public int Supprimer(int IDMedi)
        { return new A_t_medicaments(ChaineConnexion).Supprimer(IDMedi); }

    }
}
