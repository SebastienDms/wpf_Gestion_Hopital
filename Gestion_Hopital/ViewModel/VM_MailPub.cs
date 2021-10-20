using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gestion_Hopital.Model;

namespace Gestion_Hopital.ViewModel
{
    public class VM_MailPub
    {
        #region Données
        private string chConnexion = @"Data Source=DESKTOP-KJ2N7R1;Initial Catalog=BD_Hopital_2;Integrated Security=True";
        private List<AdresseMail> _listAdresseMails;

        #endregion
        public List<AdresseMail> ListAdresseMails
        {
            get => _listAdresseMails;
            set => _listAdresseMails = value;
        }

        public VM_MailPub()
        {
            ListAdresseMails = CreateListMail();
        }

        private List<AdresseMail> CreateListMail()
        {
            List<AdresseMail> tmpList = new List<AdresseMail>();
            var lTmpPatients = new G_t_patients(chConnexion).Lire("IDPat");
            foreach (var patient in lTmpPatients)
            {
                if (patient.MailPat != String.Empty)
                {
                    AdresseMail tmpAdresseMail = new AdresseMail();
                    tmpAdresseMail.Mail = patient.MailPat;
                    tmpAdresseMail.NomPrenomPatient = patient.NomPat + " " + patient.PrenomPat;
                    tmpList.Add(tmpAdresseMail);
                }
            }

            return tmpList;
        }
        public class AdresseMail
        {
            private string _mail, _nomPrenomPatient;
            public string Mail
            {
                get => _mail;
                set => _mail = value;
            }

            public string NomPrenomPatient
            {
                get => _nomPrenomPatient;
                set => _nomPrenomPatient = value;
            }
        }

    }
}
