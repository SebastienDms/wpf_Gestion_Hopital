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
    /// Logique d'interaction pour OccupationChambre.xaml
    /// </summary>
    public partial class OccupationChambre : Window
    {
        private VM_Chambres _mwChambres;
        private VM_DispoChambre _mwDispoChambre;
        public OccupationChambre()
        {
            InitializeComponent();
            MwChambres = new VM_Chambres();
            dgChambres.DataContext = MwChambres;
        }

        public VM_Chambres MwChambres
        {
            get => _mwChambres;
            set => _mwChambres = value;
        }

        public VM_DispoChambre MwDispoChambre
        {
            get => _mwDispoChambre;
            set => _mwDispoChambre = value;
        }

        private void dgChambres_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgChambres.SelectedIndex >= 0)
            {
                MwChambres.ChambresSelectionnee2UneChambre();
                MwDispoChambre = new VM_DispoChambre(MwChambres.UneChambre.ID);
                dgDispoChambres.DataContext = MwDispoChambre;
            }
        }
    }
}
