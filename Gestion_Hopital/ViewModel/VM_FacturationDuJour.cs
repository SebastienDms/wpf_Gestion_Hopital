using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Gestion_Hopital.Model;
using System.Windows.Documents;

namespace Gestion_Hopital.ViewModel
{
    public class VM_FacturationDuJour : BasePropriete
    {
        #region Données Écran
        private string chConnexion = @"Data Source=DESKTOP-GES02KU;Initial Catalog=BD_Hopital;Integrated Security=True";
        private bool _activerBouton;
        #endregion
        #region Données extérieures
        private ObservableCollection<FacturationDuJour> _listFacturesDuJour = new ObservableCollection<FacturationDuJour>();
        public ObservableCollection<FacturationDuJour> ListFacturesDuJour
        {
            get { return _listFacturesDuJour; }
            set { _listFacturesDuJour = value; }
        }
        #endregion
        public BaseCommande cFacturer { get; set; }

        public bool ActiverBouton
        {
            get => _activerBouton;
            set { AssignerChamp<bool>(ref _activerBouton, value, System.Reflection.MethodBase.GetCurrentMethod().Name);}
        }

        public VM_FacturationDuJour()
        {
            ListFacturesDuJour = AFacturer();
            cFacturer = new BaseCommande(Facturer);
            if (ListFacturesDuJour.Count == 0)
            {
                ActiverBouton = false;
            }
            else
            {
                ActiverBouton = true;
            }
        }
        private ObservableCollection<FacturationDuJour> AFacturer()
        {
            ObservableCollection<FacturationDuJour> _listFacture = new ObservableCollection<FacturationDuJour>();
            FacturationDuJour factureClient = new FacturationDuJour();
            var lTmpPat = new G_t_patients(chConnexion).Lire("IDPat");
            var lTmpCha = new G_t_chambres(chConnexion).Lire("IDCha");
            var lTmpSoi = new G_t_soigner(chConnexion).Lire("IDSoi");
            var lTmpOcc = new G_t_occuper(chConnexion).Lire("IDOcc");
            var lTmpMedi = new G_t_medicaments(chConnexion).Lire("IDMedi");
            var lTmpSoin = new G_t_soins(chConnexion).Lire("IDSoin");

            // Recherche et affiche les séjours à l'hopital terminé ce jour et à facturer \\
            foreach (C_t_occuper o in lTmpOcc)
            {
                if (o.DateSortie == DateTime.Today /*|| o.DateSortie==DateTime.Today.AddDays(-1)*/)
                {
                    factureClient.DateEntree = o.DateEntree.ToShortDateString();
                    factureClient.DateSortie = o.DateSortie.ToShortDateString();
                    factureClient.PrixJournalier = o.PrixJournalier;
                    factureClient.NbrJour = (o.DateEntree - o.DateSortie).Days;
                    factureClient.PrixSejour = factureClient.NbrJour * factureClient.PrixJournalier;

                    foreach (C_t_patients p in lTmpPat)
                    {
                        if (o.IDPat == p.IDPat)
                        {
                            factureClient.NomPrenomPat = p.NomPat + " " + p.PrenomPat;
                            factureClient.AdressePat = p.AdressePat;

                            foreach (C_t_soigner soig in lTmpSoi)
                            {
                                if (soig.DateOperation >= o.DateEntree && soig.DateOperation <= o.DateSortie && soig.IDPat == p.IDPat)
                                {
                                    foreach (var soin in lTmpSoin)
                                    {
                                        if (soig.IDTyp == soin.IDSoin)
                                        {
                                            SoinsRecu tmpSoinsRecu = new SoinsRecu();
                                            tmpSoinsRecu.Nom = soin.NomSoin;
                                            tmpSoinsRecu.Prix = soin.PrixSoin;
                                            factureClient.ListSoinsRecus.Add(tmpSoinsRecu);
                                        }
                                    }

                                    foreach (var medicament in lTmpMedi)
                                    {
                                        if (soig.IDMedi==medicament.IDMedi)
                                        {
                                            MedicamentsRecu tmpMedicamentsRecu = new MedicamentsRecu();
                                            tmpMedicamentsRecu.Nom = medicament.NomMedi;
                                            tmpMedicamentsRecu.Prix = medicament.PrixMedi;
                                            factureClient.ListMedicamentsRecus.Add(tmpMedicamentsRecu);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    foreach (C_t_chambres c in lTmpCha)
                    {
                        if (o.IDCha == c.IDCha)
                        {
                            factureClient.NumChambre = c.NomCha.ToString();
                        }
                    }

                    _listFacture.Add(factureClient);
                }
            }
            return _listFacture;
        }

        private void Facturer()
        {
            Thread tFacture = new Thread(GenererFacture);
            tFacture.Start();
        }

        private void GenererFacture()
        {
            foreach (var factureClient in ListFacturesDuJour)
            {
                int sousTotalSoins = 0, sousTotalMedicaments=0;
                /* Création du fichier */
                FlowDocument fd = new FlowDocument();
                Paragraph p = new Paragraph();
                p.Inlines.Add(new Bold(new Run("Facture:")));
                p.Inlines.Add(new LineBreak());
                p.Inlines.Add(new Run(factureClient.NomPrenomPat + " " +factureClient.AdressePat));
                p.Inlines.Add(new LineBreak());
                p.Inlines.Add(new Run("Séjour du " + factureClient.DateEntree + " au " + factureClient.DateSortie + "."));
                p.Inlines.Add(new Run(factureClient.NbrJour + " prix par jour " + factureClient.PrixJournalier + " total: " + factureClient.PrixSejour + "."));
                fd.Blocks.Add(p);
                Paragraph p2 = new Paragraph();
                foreach (var soinsRecus in factureClient.ListSoinsRecus)
                {
                    p2.Inlines.Add(new Run(soinsRecus.Nom + " : " + soinsRecus.Prix.ToString()));
                    p.Inlines.Add(new LineBreak());
                    sousTotalSoins += soinsRecus.Prix;
                }
                fd.Blocks.Add(p2);
                Paragraph p3 = new Paragraph(new Run("Prix total des soins : " + sousTotalSoins.ToString()));
                fd.Blocks.Add(p3);
                Paragraph p4 = new Paragraph();
                foreach (var medicamentsRecus in factureClient.ListMedicamentsRecus)
                {
                    p4.Inlines.Add(new Run(medicamentsRecus.Nom + " : " + medicamentsRecus.Prix.ToString()));
                    p.Inlines.Add(new LineBreak());
                    sousTotalMedicaments += medicamentsRecus.Prix;
                }
                fd.Blocks.Add(p4);
                Paragraph p5 = new Paragraph(new Run("Prix total des médicaments : " + sousTotalMedicaments.ToString()));
                fd.Blocks.Add(p5);
                Paragraph p6 = new Paragraph(new Run("Prix total : " + (sousTotalSoins+sousTotalMedicaments).ToString()));
                fd.Blocks.Add(p6);
                /* Sauvegarde du ficher */
                FileStream fs = new FileStream(@"d:\essai.rtf", FileMode.Create);
                TextRange tr = new TextRange(fd.ContentStart, fd.ContentEnd);
                tr.Save(fs, System.Windows.DataFormats.Rtf);
            }
        }
        public class FacturationDuJour
        {
            private string _nomPrenomPat, _adressePat, _dateEntree, _dateSortie, _numChambre;
            private int _nbrJour, _prixSejour, _prixJournalier;
            private List<SoinsRecu> _listSoinsRecus;
            private List<MedicamentsRecu> _listMedicamentsRecus;

            public string NomPrenomPat
            {
                get => _nomPrenomPat;
                set => _nomPrenomPat = value;
            }

            public string AdressePat
            {
                get => _adressePat;
                set => _adressePat = value;
            }

            public string DateEntree
            {
                get => _dateEntree;
                set => _dateEntree = value;
            }

            public string DateSortie
            {
                get => _dateSortie;
                set => _dateSortie = value;
            }

            public int PrixJournalier
            {
                get => _prixJournalier;
                set => _prixJournalier = value;
            }

            public int PrixSejour
            {
                get => _prixSejour;
                set => _prixSejour = value;
            }

            public int NbrJour
            {
                get => _nbrJour;
                set => _nbrJour = value;
            }

            public List<SoinsRecu> ListSoinsRecus
            {
                get => _listSoinsRecus;
                set => _listSoinsRecus = value;
            }

            public List<MedicamentsRecu> ListMedicamentsRecus
            {
                get => _listMedicamentsRecus;
                set => _listMedicamentsRecus = value;
            }

            public string NumChambre
            {
                get => _numChambre;
                set => _numChambre = value;
            }
        }
        public class SoinsRecu
        {
            private string _nom;
            private int _prix;

            public string Nom
            {
                get => _nom;
                set => _nom = value;
            }

            public int Prix
            {
                get => _prix;
                set => _prix = value;
            }
        }
        public class MedicamentsRecu
        {
            private string _nom;
            private int _prix;

            public string Nom
            {
                get => _nom;
                set => _nom = value;
            }

            public int Prix
            {
                get => _prix;
                set => _prix = value;
            }
        }
    }
}
