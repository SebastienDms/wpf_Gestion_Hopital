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
    /// Logique d'interaction pour Medicaments.xaml
    /// </summary>
    public partial class Medicaments : Window
    {
        private VM_Medicaments _localMedicament;

        public VM_Medicaments LocalMedicament
        {
            get => _localMedicament;
            set => _localMedicament = value;
        }
        public Medicaments()
        {
            InitializeComponent();
            LocalMedicament = new VM_Medicaments();
            DataContext = LocalMedicament;

        }
        private void dgMedicaments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { if (dgMedicaments.SelectedIndex >= 0) LocalMedicament.MedicamentsSelectionnee2UnMedicament(); }
    }
}
