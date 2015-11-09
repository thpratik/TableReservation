using Microsoft.Practices.Unity;
using System.Windows.Controls;
using TableReservation.Common.View;
using TableReservation.Common.ViewModel;

namespace TableReservation.View
{
    /// <summary>
    /// Interaction logic for ReservationView.xaml
    /// </summary>
    public partial class ReservationView : UserControl, IReservationView
    {
        public ReservationView()
        {
            InitializeComponent();
        }

        [Dependency]
        public IReservationViewModel ViewModel
        {
            get
            {
                return this.DataContext as IReservationViewModel;
            }

            set
            {
                this.DataContext = value;
            }
        }
    }
}
