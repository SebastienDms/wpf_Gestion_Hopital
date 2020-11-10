using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gestion_Hopital.Model;

namespace Gestion_Hopital.Dashboard
{
    public static class GestionDispoMedecin
    {
        private static string chConnexion = @"Data Source=DESKTOP-GES02KU;Initial Catalog=BD_Hopital;Integrated Security=True";

        public static List<DateTime> JoursoccupeList(int idMed)
        {
            List<DateTime> jour_operation = new List<DateTime>();

            if (idMed != 0)
            {
                int Annee_Comp = DateTime.Today.Year;

                List<C_t_soigner> lTmp_Soi = new G_t_soigner(chConnexion).Lire("ID");
                foreach (C_t_soigner o in lTmp_Soi)
                {
                    if (o.IDMed == idMed && Annee_Comp == o.DateOperation.Date.Year)
                    {
                        jour_operation.Add(o.DateOperation);
                    }
                }
            }

            return jour_operation;
        }
    }
}
