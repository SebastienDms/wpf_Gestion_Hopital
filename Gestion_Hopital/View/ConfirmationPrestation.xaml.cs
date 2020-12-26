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
using System.Windows.Shapes;
using Gestion_Hopital.ViewModel;

namespace Gestion_Hopital.View
{
    /// <summary>
    /// Logique d'interaction pour ConfirmationPrestation.xaml
    /// </summary>
    public partial class ConfirmationPrestation : Window
    {
        #region données
        private VM_ConfirmationPrestation _vmConfirmationPrestation;
        private string _attachmentPath = "";
        private string _serverName = "SMTP.office365.com";
        private int _serverPort = 587;
        #endregion

        #region Accesseurs

        public string AttachmentPath
        {
            get => _attachmentPath;
            set => _attachmentPath = value;
        }

        public string ServerName
        {
            get => _serverName;
            set => _serverName = value;
        }

        public int ServerPort
        {
            get => _serverPort;
            set => _serverPort = value;
        }

        public VM_ConfirmationPrestation VmConfirmationPrestation
        {
            get => _vmConfirmationPrestation;
            set => _vmConfirmationPrestation = value;
        }

        #endregion

        public ConfirmationPrestation()
        {
            InitializeComponent();
            VmConfirmationPrestation = new VM_ConfirmationPrestation();
            DataContext = VmConfirmationPrestation;
        }

        private void dgPatientsAConfirmer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            VM_ConfirmationPrestation.PatientAConfirmer patient =
                (VM_ConfirmationPrestation.PatientAConfirmer) dgPatientsAConfirmer.SelectedItem;

            FlowDocument fd = new FlowDocument();
            Paragraph p = new Paragraph();
            p.Inlines.Add(new Bold(new Run("Confirmation de votre Rendez-vous")));
            p.Inlines.Add(new LineBreak());
            p.Inlines.Add(new Run("Liste des personnes encodées"));
            fd.Blocks.Add(p);
            Paragraph p2 = new Paragraph(new Run(patient.Nom + " " + patient.Prenom + " " + patient.DateRdv + "."));
            fd.Blocks.Add(p2);
            Paragraph p3 = new Paragraph(new Run("Votre entrée en hopital le " + patient.DateRdv + " est confirmée."));
            fd.Blocks.Add(p3);
            rtbDoc.Document = fd;
        }

        private void BtnSendMail_OnClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
