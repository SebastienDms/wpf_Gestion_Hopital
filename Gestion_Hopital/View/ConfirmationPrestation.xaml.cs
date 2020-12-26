using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
            p.Inlines.Add(new Run("Madame,Monsieur,"));
            fd.Blocks.Add(p);
            Paragraph p2 = new Paragraph(new Run(patient.Nom + " " + patient.Prenom + "."));
            fd.Blocks.Add(p2);
            Paragraph p3 = new Paragraph(new Run("Votre entrée en hopital le " + patient.DateRdv.ToShortDateString() + " est confirmée."));
            fd.Blocks.Add(p3);
            rtbDoc.Document = fd;
        }

        private void BtnSendMail_OnClick(object sender, RoutedEventArgs e)
        {
            int i = 1;
            foreach (var patient in VmConfirmationPrestation.ListPatientAConfirmer)
            {
                FlowDocument fd = new FlowDocument();
                Paragraph p = new Paragraph();
                p.Inlines.Add(new Bold(new Run("Confirmation de votre Rendez-vous")));
                p.Inlines.Add(new LineBreak());
                p.Inlines.Add(new Run("Madame,Monsieur,"));
                fd.Blocks.Add(p);
                Paragraph p2 = new Paragraph(new Run(patient.Nom + " " + patient.Prenom + "."));
                fd.Blocks.Add(p2);
                Paragraph p3 = new Paragraph(new Run("Votre entrée en hopital le " + patient.DateRdv.ToShortDateString() + " est confirmée."));
                fd.Blocks.Add(p3);
                rtbDoc.Document = fd;

                FileStream fs = new FileStream(@"d:\confirmation_" + i.ToString() + "_" + patient.Nom + "_" + patient.Prenom +".rtf", FileMode.Create);
                TextRange tr = new TextRange(rtbDoc.Document.ContentStart, rtbDoc.Document.ContentEnd);

                if (patient.Mail != String.Empty)
                {
                    try
                    {
                        MailMessage mail = new MailMessage();
                        SmtpClient smtpClient = new SmtpClient(ServerName, ServerPort);

                        mail.From = new MailAddress(tbLogin.Text);
                        mail.To.Add(patient.Mail);
                        mail.Subject = "Confirmation de votre rendez-vous.";
                        mail.Body = tr.Text;

                        smtpClient.Credentials = new NetworkCredential(tbLogin.Text, tbPassword.Password);
                        smtpClient.EnableSsl = true;

                        smtpClient.Send(mail);
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message);
                    }
                }

                tr.Save(fs, System.Windows.DataFormats.Rtf);
                i++;
            }

            MessageBox.Show("Confirmations envoyées.");
        }
    }
}
