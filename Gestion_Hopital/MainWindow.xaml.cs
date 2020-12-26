using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Gestion_Hopital.View;
using Gestion_Hopital.ViewModel;
using Gestion_Hopital.Dashboard;

namespace Gestion_Hopital
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Donnees
        private VM_Medecins _mwMedecins;
        private VM_Patients _mwPatients;
        private VM_Chambres _mwChambres;
        private VM_DispoChambre _mwDispoChambre;
        private VM_Occuper _mwOccuper;
        private VM_Soigner _mwSoigner;
        private VM_Medicaments _mwMedicaments;
        private VM_Soins _mwSoins;
        private BaseCommande cAjouterOccuper;
        #endregion
        #region Accesseurs
        public VM_Medecins MwMedecins
        {
            get => _mwMedecins;
            set => _mwMedecins = value;
        }
        public VM_Patients MwPatients
        {
            get => _mwPatients;
            set => _mwPatients = value;
        }
        public VM_Chambres MwChambres
        {
            get => _mwChambres;
            set => _mwChambres = value;
        }
        public VM_DispoChambre MmDispoChambre
        {
            get => _mwDispoChambre;
            set => _mwDispoChambre = value;
        }
        public VM_Occuper MwOccuper
        {
            get => _mwOccuper;
            set => _mwOccuper = value;
        }
        public VM_Soigner MwSoigner
        {
            get => _mwSoigner;
            set => _mwSoigner = value;
        }
        public VM_Medicaments MwMedicaments
        {
            get => _mwMedicaments;
            set => _mwMedicaments = value;
        }
        public VM_Soins MwSoins
        {
            get => _mwSoins;
            set => _mwSoins = value;
        }

        public BaseCommande CAjouterOccuper
        {
            get => cAjouterOccuper;
            set => cAjouterOccuper = value;
        }
        #endregion
        public MainWindow()
        {
            InitializeComponent();
            CalendarMedecin.DisplayDateStart=DateTime.Today;
            MwMedecins = new VM_Medecins();
            MwPatients = new VM_Patients();
            MwChambres = new VM_Chambres();
            MmDispoChambre = new VM_DispoChambre();
            MwOccuper = new VM_Occuper();
            MwSoigner = new VM_Soigner();
            MwMedicaments = new VM_Medicaments();
            MwSoins = new VM_Soins();
            dgMedecins.DataContext = MwMedecins;
            dgPatients.DataContext = MwPatients;
            dgChambres.DataContext = MwChambres;
            dgDispoChambres.DataContext = MmDispoChambre;
            dgMedicaments.DataContext = MwMedicaments;
            dgSoins.DataContext = MwSoins;
            FicheOccuper.DataContext = MwOccuper;
            FicheSoigner.DataContext = MwSoigner;
        }
        private void dgMedecins_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgMedecins.SelectedIndex >= 0)
            {
                MwMedecins.MedecinsSelectionnee2UnMedecin();
                /* Mise à jour du calendrier du médecin sélectionné */
                CalendarMedecin.BlackoutDates.Clear();
                CalendarMedecin.BlackoutDates.AddDatesInPast();
                List<DateTime> dateRange = GestionDispoMedecin.JoursoccupeList(MwMedecins.UnMedecin.ID);
                foreach (var date in dateRange)
                {
                    CalendarMedecin.BlackoutDates.Add(new CalendarDateRange(date, date));
                }
            }
        }
        private void dgPatients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { if (dgPatients.SelectedIndex >= 0) MwPatients.ClientsSelectionnee2UnClient(); }
        private void dgChambres_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgChambres.SelectedIndex >= 0)
            {
                MwChambres.ChambresSelectionnee2UneChambre();
                MmDispoChambre = new VM_DispoChambre(MwChambres.UneChambre.ID);
                dgDispoChambres.DataContext = MmDispoChambre;
            }
        }
        private void dgMedicaments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { if (dgMedicaments.SelectedIndex >= 0) MwMedicaments.MedicamentsSelectionnee2UnMedicament(); }
        private void dgSoins_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { if (dgMedicaments.SelectedIndex >= 0) MwSoins.SoinsSelectionnes2UnClient(); }

        #region Fenetres Boutons Menu
        public void btnMedecins_Click(object sender, RoutedEventArgs e)
        {
            Medecins fenMed = new Medecins();
            fenMed.ShowDialog();
        }
        public void btnPatients_Click(object sender, RoutedEventArgs e)
        {
            Patients fenPat = new Patients();
            fenPat.ShowDialog();
        }
        public void btnSpecialites_Click(object sender, RoutedEventArgs e)
        {
            Specialites fenSpe = new Specialites();
            fenSpe.ShowDialog();
        }
        public void btnChambres_Click(object sender, RoutedEventArgs e)
        {
            Chambres fenCha = new Chambres();
            fenCha.ShowDialog();
        }
        public void btnMedicaments_Click(object sender, RoutedEventArgs e)
        {
            Medicaments fenMedi = new Medicaments();
            fenMedi.ShowDialog();
        }
        public void btnSoins_Click(object sender, RoutedEventArgs e)
        {
            Soins fenTyp = new Soins();
            fenTyp.ShowDialog();
        }
        public void btnOccupations_Click(object sender, RoutedEventArgs e)
        {
            Occuper fenOcc = new Occuper();
            fenOcc.ShowDialog();
        }
        public void btnSoigner_Click(object sender, RoutedEventArgs e)
        {
            Soigner fenSoi = new Soigner();
            fenSoi.ShowDialog();
        }
        private void btnHitParade_Click(object sender, RoutedEventArgs e)
        {
            HitParade fenHitParade = new HitParade();
            fenHitParade.ShowDialog();
        }
        private void btnFacturation_Click(object sender, RoutedEventArgs e)
        {
            FacturationDuJour fenFacturationDuJour = new FacturationDuJour();
            fenFacturationDuJour.ShowDialog();
        }
        private void btnOccupationsHebdo_Click(object sender, RoutedEventArgs e)
        {
            OccupationChambre fenOccupationChambre = new OccupationChambre();
            fenOccupationChambre.ShowDialog();
        }
        private void btnConfirmatinPrestation_Click(object sender, RoutedEventArgs e)
        {
            ConfirmationPrestation fenConfirmationPrestation = new ConfirmationPrestation();
            fenConfirmationPrestation.ShowDialog();
        }

        private void btnMail_Click(object sender, RoutedEventArgs e)
        {
            MailPub fenMailPub = new MailPub();
            fenMailPub.ShowDialog();
        }

        public void btnQuitter_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        #endregion

        #region Bouton OnClicks
        #region FicheOccuper
        private void BConfirmer_OnClick(object sender, RoutedEventArgs e)
        {
            /* Permet d'envoyer les valeurs encodées dans la fiche Occuper */
            /* Car les éléments sont déjà bindé avec les différents DG */
            bool okIDPat, okIDCha;
            int resIDPat, resIDCha;
            okIDPat = int.TryParse(tbIDPatOcc.Text, out resIDPat);
            okIDCha = int.TryParse(tbIDCha.Text, out resIDCha);
            if (okIDPat && okIDCha)
            {
                MwOccuper.UneOccupation.IDPat = resIDPat;
                MwOccuper.UneOccupation.IDCha = resIDCha;
                MwOccuper.UneOccupation.DateEntree = DateTime.Parse(DateEntreePicker.Text);
                MwOccuper.UneOccupation.DateSortie = DateTime.Parse(DateSortiePicker.Text);
            }
            ViderFicheOccuper();
        }
        private void BAnnuler_OnClick(object sender, RoutedEventArgs e)
        { ViderFicheOccuper(); }
        #endregion

        #region FicheSoigner
        private void BConfirmerS_OnClick(object sender, RoutedEventArgs e)
        {
            /* Permet d'envoyer les valeurs encodées dans la fiche Soigner */
            /* Car les éléments sont déjà bindé avec les différents DG */
            bool okIDSoig, okIDPat, okIDMed, okIDTyp, okIDMedi;
            int resIdSoing, resIdPat, okIdMed, okIdTyp, okIdMedi;
            okIDSoig = int.TryParse(tbIDSoig.Text, out resIdSoing);
            okIDPat = int.TryParse(tbIDPat.Text, out resIdPat);
            okIDMed = int.TryParse(tbIDMed.Text, out okIdMed);
            okIDTyp = int.TryParse(tbIDType.Text, out okIdTyp);
            okIDMedi = int.TryParse(tbIDMedi.Text, out okIdMedi);
            if (okIDSoig && okIDPat && okIDMed && okIDTyp && okIDMedi)
            {
                MwSoigner.UnSoigner.ID = resIdSoing;
                MwSoigner.UnSoigner.IDPat = resIdPat;
                MwSoigner.UnSoigner.IDMed = okIdMed;
                MwSoigner.UnSoigner.IDType = okIdTyp;
                MwSoigner.UnSoigner.IDMedi = okIdMedi;
                MwSoigner.UnSoigner.DateOperation = DateTime.Parse(DateOperationPicker.Text);
            }
            ViderFicheSoigner();
        }

        private void BAnnulerS_OnClick(object sender, RoutedEventArgs e)
        {
            ViderFicheSoigner();
        }
        #endregion
        #endregion

        #region Vider TextBox
        private void ViderFicheOccuper()
        { DateEntreePicker.Text = DateSortiePicker.Text =tbIDOcc.Text = tbIDPatOcc.Text = tbIDCha.Text = tbPrixJour.Text = string.Empty; }

        private void ViderFicheSoigner()
        { DateOperationPicker.Text = tbIDSoig.Text = tbIDPat.Text = tbIDMed.Text = tbIDType.Text = tbIDMedi.Text = string.Empty; }
        #endregion

    }
}
