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
    /// Logique d'interaction pour Chambres.xaml
    /// </summary>
    public partial class Chambres : Window
    {
        private VM_Chambres _localChambre;

        public VM_Chambres LocalChambre
        {
            get => _localChambre;
            set => _localChambre = value;
        }
        public Chambres()
        {
            InitializeComponent();
            LocalChambre = new VM_Chambres();
            DataContext = LocalChambre;

        }
        private void dgChambres_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { if (dgChambres.SelectedIndex >= 0) LocalChambre.ChambresSelectionnee2UneChambre(); }
    }
}
