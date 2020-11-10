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
    /// Logique d'interaction pour Soins.xaml
    /// </summary>
    public partial class Soins : Window
    {
        private VM_Soins _localSoins;

        public VM_Soins LocalSoins
        {
            get => _localSoins;
            set => _localSoins = value;
        }
        public Soins()
        {
            InitializeComponent();
            InitializeComponent();
            LocalSoins = new VM_Soins();
            DataContext = LocalSoins;
            /* Prépare le document */
            FlowDocument fd = new FlowDocument();
            Paragraph p = new Paragraph();
            p.Inlines.Add(new Bold(new Run("Titre de document")));
            p.Inlines.Add(new LineBreak());
            p.Inlines.Add(new Run("Liste des personnes encodées"));
            fd.Blocks.Add(p);
            List l = new List();
            foreach (Model.C_t_soins cp in LocalSoins.ListSoins)
            {
                Paragraph pl = new Paragraph(new Run(cp.NomSoin + " " + cp.PrixSoin));
                l.ListItems.Add(new ListItem(pl));
            }
            fd.Blocks.Add(l);
            rtbDoc.Document = fd;
            FileStream fs = new FileStream(@"d:\essai.rtf", FileMode.Create);
            TextRange tr = new TextRange(rtbDoc.Document.ContentStart, rtbDoc.Document.ContentEnd);
            tr.Save(fs, System.Windows.DataFormats.Rtf);
        }
        private void dgSoins_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { if (dgSoins.SelectedIndex >= 0) LocalSoins.SoinsSelectionnes2UnClient(); }

    }
}
