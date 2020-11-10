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
    /// Logique d'interaction pour Soigner.xaml
    /// </summary>
    public partial class Soigner : Window
    {
        private VM_Soigner _localSoigner;

        public VM_Soigner LocalSoigner
        {
            get => _localSoigner;
            set => _localSoigner = value;
        }
        public Soigner()
        {
            InitializeComponent();
            LocalSoigner = new VM_Soigner();
            DataContext = LocalSoigner;
            /* Prépare le document */
            FlowDocument fd = new FlowDocument();
            Paragraph p = new Paragraph();
            p.Inlines.Add(new Bold(new Run("Titre de document")));
            p.Inlines.Add(new LineBreak());
            p.Inlines.Add(new Run("Liste des soins prodigués"));
            fd.Blocks.Add(p);
            List l = new List();
            foreach (Model.C_t_soigner cp in LocalSoigner.ListSoigners)
            {
                Paragraph pl = new Paragraph(new Run(cp.IDMed + " " + cp.IDPat
                                                     + " (" + cp.IDTyp + cp.IDMedi + cp.DateOperation.ToShortDateString() + ")"));
                l.ListItems.Add(new ListItem(pl));
            }
            fd.Blocks.Add(l);
            rtbDoc.Document = fd;
            FileStream fs = new FileStream(@"d:\essai.rtf", FileMode.Create);
            TextRange tr = new TextRange(rtbDoc.Document.ContentStart, rtbDoc.Document.ContentEnd);
            tr.Save(fs, System.Windows.DataFormats.Rtf);
        }
        private void dgSoigners_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { if (dgSoigners.SelectedIndex >= 0) LocalSoigner.SoignersSelectionnee2UnSoigner(); }

    }
}
