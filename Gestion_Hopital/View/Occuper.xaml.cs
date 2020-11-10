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
    /// Logique d'interaction pour Occuper.xaml
    /// </summary>
    public partial class Occuper : Window
    {
        private VM_Occuper _localOccuper;

        public VM_Occuper LocalOccuper
        {
            get => _localOccuper;
            set => _localOccuper = value;
        }
        public Occuper()
        {
            InitializeComponent();
            LocalOccuper = new VM_Occuper();
            DataContext = LocalOccuper;
            /* Prépare le document */
            FlowDocument fd = new FlowDocument();
            Paragraph p = new Paragraph();
            p.Inlines.Add(new Bold(new Run("Titre de document")));
            p.Inlines.Add(new LineBreak());
            p.Inlines.Add(new Run("Liste des personnes encodées"));
            fd.Blocks.Add(p);
            List l = new List();
            foreach (Model.C_t_occuper cp in LocalOccuper.ListOccupations)
            {
                Paragraph pl = new Paragraph(new Run(cp.IDPat + " " + cp.IDCha
                                                     + " (" + cp.DateEntree + cp.DateSortie + cp.PrixJournalier + ")"));
                l.ListItems.Add(new ListItem(pl));
            }
            fd.Blocks.Add(l);
            rtbDoc.Document = fd;
            FileStream fs = new FileStream(@"d:\essai.rtf", FileMode.Create);
            TextRange tr = new TextRange(rtbDoc.Document.ContentStart, rtbDoc.Document.ContentEnd);
            tr.Save(fs, System.Windows.DataFormats.Rtf);
        }
        private void dgOccupations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { if (dgOccupations.SelectedIndex >= 0) LocalOccuper.OccupationsSelectionnee2UneOccupation(); }
    }
}
