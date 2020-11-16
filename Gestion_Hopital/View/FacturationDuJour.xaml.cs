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
    /// Logique d'interaction pour FacturationDuJour.xaml
    /// </summary>
    public partial class FacturationDuJour : Window
    {
        #region Données
        private VM_FacturationDuJour _vmFacturationDuJour;
        #endregion
        #region Accesseurs
        public VM_FacturationDuJour VmFacturationDuJour
        {
            get => _vmFacturationDuJour;
            set => _vmFacturationDuJour = value;
        }
        #endregion
        public FacturationDuJour()
        {
            InitializeComponent();
            VmFacturationDuJour = new VM_FacturationDuJour();
            DataContext = VmFacturationDuJour;
        }

    }
}
