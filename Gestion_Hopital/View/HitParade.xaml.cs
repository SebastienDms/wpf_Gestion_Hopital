using System.Windows;
using Gestion_Hopital.ViewModel;

namespace Gestion_Hopital.View
{
    /// <summary>
    /// Logique d'interaction pour HitParade.xaml
    /// </summary>
    public partial class HitParade : Window
    {
        #region Données
        private VM_HitParade _vmHitParade;
        #endregion

        #region Accesseurs
        public VM_HitParade VmHitParade
        {
            get => _vmHitParade;
            set => _vmHitParade = value;
        }
        #endregion
        public HitParade()
        {
            InitializeComponent();
            VmHitParade = new VM_HitParade();
            dgHitParade.DataContext = VmHitParade;
        }

    }
}
