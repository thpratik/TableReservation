using Microsoft.Practices.Unity;
using System.Windows.Controls;
using TableReservation.Common.View;
using TableReservation.Common.ViewModel;

namespace TableReservation.View
{
    /// <summary>
    /// Interaction logic for ReservationView.xaml
    /// </summary>
    public partial class ReservationDashBoardView : UserControl, IReservationDashBoardView
    {
        public ReservationDashBoardView()
        {
            InitializeComponent();
        }

        [Dependency]
        public IReservationDashBoardViewModel ViewModel
        {
            get
            {
                return this.DataContext as IReservationDashBoardViewModel;
            }

            set
            {
                this.DataContext = value;
            }
        }
    }
}
