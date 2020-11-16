using System;
using System.Collections.Generic;
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
using Microsoft.Win32;

namespace Gestion_Hopital.View
{
    /// <summary>
    /// Logique d'interaction pour MailPub.xaml
    /// </summary>
    public partial class MailPub : Window
    {
        private VM_MailPub _vmMailPub;
        private string _attachmentPath = "";
        private string _serverName = "SMTP.office365.com";
        private int _serverPort = 587;
        public MailPub()
        {
            InitializeComponent();
            VmMailPub = new VM_MailPub();
            DataContext = VmMailPub;
        }
        public string AttachmentPath
        {
            get => _attachmentPath;
            set => _attachmentPath = value;
        }

        public VM_MailPub VmMailPub
        {
            get => _vmMailPub;
            set => _vmMailPub = value;
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


        private void dgPatientsMail_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BtnAttachment_OnClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            var res = openFileDialog.ShowDialog();

            if (res == true)
            {
                AttachmentPath = openFileDialog.FileName;
            }
        }

        private void BtnSendMail_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtpClient = new SmtpClient(ServerName, ServerPort);

                mail.From = new MailAddress(tbLogin.Text);
                mail.To.Add(tbTo.Text);
                mail.Subject = tbObject.Text;
                mail.Body = new TextRange(rtbBody.Document.ContentStart, rtbBody.Document.ContentEnd).Text;

                if (!string.IsNullOrEmpty(AttachmentPath))
                {
                    Attachment attachmentMail = new Attachment(AttachmentPath);
                    mail.Attachments.Add(attachmentMail);
                }

                smtpClient.Credentials = new NetworkCredential(tbLogin.Text,tbPassword.Password);
                smtpClient.EnableSsl = true;

                smtpClient.Send(mail);
                MessageBox.Show("Mail envoyé!");
            }
            catch (Exception)
            {
                MessageBox.Show("Erreur dans les paramètres...");
            }
        }
    }
}
