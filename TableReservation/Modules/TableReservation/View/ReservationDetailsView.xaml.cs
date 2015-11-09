using Microsoft.Practices.Unity;
using System.Windows.Controls;
using TableReservation.Common.View;
using TableReservation.Common.ViewModel;

namespace TableReservation.View
{
    /// <summary>
    /// Interaction logic for ReservationView.xaml
    /// </summary>
    public partial class ReservationDetailsView : UserControl, IReservationDetailsView
    {
        public ReservationDetailsView()
        {
            InitializeComponent();
        }

        [Dependency]
        public IReservationDetailsViewModel ViewModel
        {
            get
            {
                return this.DataContext as IReservationDetailsViewModel;
            }

            set
            {
                this.DataContext = value;
            }
        }
    }
}
