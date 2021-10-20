using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gestion_Hopital.Model;

namespace Gestion_Hopital.ViewModel
{
    public class VM_ConfirmationPrestation
    {
        #region Données
        private string chConnexion = @"Data Source=DESKTOP-KJ2N7R1;Initial Catalog=BD_Hopital_2;Integrated Security=True";
        private List<PatientAConfirmer> _listPatientAConfirmer;
        private List<PatientAConfirmer> _listPatientAConfirmerMail;

        #endregion
        public List<PatientAConfirmer> ListPatientAConfirmer
        {
            get => _listPatientAConfirmer;
            set => _listPatientAConfirmer = value;
        }
        public List<PatientAConfirmer> ListPatientAConfirmerMail
        {
            get => _listPatientAConfirmerMail;
            set => _listPatientAConfirmerMail = value;
        }

        public VM_ConfirmationPrestation()
        {
            ListPatientAConfirmer = CreateList();
            ListPatientAConfirmerMail = CreateListMail(ListPatientAConfirmer);
        }

        private List<PatientAConfirmer> CreateList()
        {
            List<PatientAConfirmer> tmpList = new List<PatientAConfirmer>();
            var lTmpPatients = new G_t_patients(chConnexion).Lire("IDPat");
            var lTmpOccupers = new G_t_occuper(chConnexion).Lire("IDOcc");
            foreach (var patient in lTmpPatients)
            {
                foreach (var occuper in lTmpOccupers)
                {
                    if (patient.IDPat == occuper.IDPat && DateTime.Compare(occuper.DateEntree.Date, DateTime.Now.AddDays(7).Date) == 0)
                    {
                        PatientAConfirmer tmpPatient = new PatientAConfirmer();
                        tmpPatient.Mail = patient.MailPat;
                        tmpPatient.Nom = patient.NomPat;
                        tmpPatient.Prenom = patient.PrenomPat;
                        tmpPatient.DateRdv = occuper.DateEntree;
                        tmpList.Add(tmpPatient);
                    }
                }
            }

            return tmpList;
        }

        private List<PatientAConfirmer> CreateListMail(List<PatientAConfirmer> tmpList)
        {
            List<PatientAConfirmer> tmpListMail = new List<PatientAConfirmer>();

            foreach (var patient in tmpList)
            {
                if (patient.Mail != String.Empty)
                {
                    PatientAConfirmer tmpPatient = new PatientAConfirmer();
                    tmpPatient.Mail = patient.Mail;
                    tmpPatient.Nom = patient.Nom;
                    tmpPatient.Prenom = patient.Prenom;
                    tmpListMail.Add(tmpPatient);
                }
            }
            return tmpListMail;
        }
        public class PatientAConfirmer
        {
            private string _mail, _nom, _prenom;
            private DateTime _dateRdv;
            public string Mail
            {
                get => _mail;
                set => _mail = value;
            }

            public string Nom
            {
                get => _nom;
                set => _nom = value;
            }

            public string Prenom
            {
                get => _prenom;
                set => _prenom = value;
            }

            public DateTime DateRdv
            {
                get => _dateRdv;
                set => _dateRdv = value;
            }
        }

    }
}
