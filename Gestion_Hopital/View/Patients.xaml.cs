using System;
using System.Collections.Generic;
using System.IO;
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
    /// Logique d'interaction pour Patients.xaml
    /// </summary>
    public partial class Patients : Window
    {
        private VM_Patients _localPersonne;
        public VM_Patients LocalPersonne
        {
            get => _localPersonne;
            set => _localPersonne = value;
        }
        public Patients()
        {
            /* prépare l'affichage */
            InitializeComponent();
            LocalPersonne = new VM_Patients();
            DataContext = LocalPersonne;
            /* Prépare le document */
            FlowDocument fd = new FlowDocument();
            Paragraph p = new Paragraph();
            p.Inlines.Add(new Bold(new Run("Titre de document")));
            p.Inlines.Add(new LineBreak());
            p.Inlines.Add(new Run("Liste des personnes encodées"));
            fd.Blocks.Add(p);
            List l = new List();
            foreach (Model.C_t_patients cp in LocalPersonne.ListClients)
            {
                Paragraph pl = new Paragraph(new Run(cp.NomPat + " " + cp.PrenomPat
                                                     + " (" + cp.AdressePat + cp.GSMPat.ToString() + cp.MailPat + ")"));
                l.ListItems.Add(new ListItem(pl));
            }
            fd.Blocks.Add(l);
            rtbDoc.Document = fd;
            FileStream fs = new FileStream(@"d:\essai.rtf", FileMode.Create);
            TextRange tr = new TextRange(rtbDoc.Document.ContentStart, rtbDoc.Document.ContentEnd);
            tr.Save(fs, System.Windows.DataFormats.Rtf);
        }
        private void dgPatients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { if (dgClients.SelectedIndex >= 0) LocalPersonne.ClientsSelectionnee2UnClient(); }
    }
}
