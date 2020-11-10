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
    /// Logique d'interaction pour Medecins.xaml
    /// </summary>
    public partial class Medecins : Window
    {
        private VM_Medecins _localMedecin;

        public VM_Medecins LocalMedecin
        {
            get => _localMedecin; set => _localMedecin = value;

        }
        public Medecins()
        {
            /* Initialisation de la vue */
            InitializeComponent();
            LocalMedecin = new VM_Medecins();
            DataContext = LocalMedecin;
            /* Création du document fiche médecins */
            FlowDocument fd = new FlowDocument();
            Paragraph p = new Paragraph();
            p.Inlines.Add(new Bold(new Run("Titre de document")));
            p.Inlines.Add(new LineBreak());
            p.Inlines.Add(new Run("Liste des personnes encodées"));
            fd.Blocks.Add(p);
            List l = new List();
            foreach (Model.C_t_medecins cp in LocalMedecin.ListMedecins)
            {
                Paragraph pl = new Paragraph(new Run(cp.NomMed + " " + cp.PrenomMed
                                                     + " (" + cp.IDSpe.ToString() + cp.GSMMed.ToString() + cp.DateNaisMed + ")"));
                l.ListItems.Add(new ListItem(pl));
            }
            fd.Blocks.Add(l);
            rtbDoc.Document = fd;
            FileStream fs = new FileStream(@"d:\essai.rtf", FileMode.Create);
            TextRange tr = new TextRange(rtbDoc.Document.ContentStart, rtbDoc.Document.ContentEnd);
            tr.Save(fs, System.Windows.DataFormats.Rtf);
        }
        private void dgMedecins_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { if (dgMedecins.SelectedIndex >= 0) LocalMedecin.MedecinsSelectionnee2UnMedecin(); }

    }
}
