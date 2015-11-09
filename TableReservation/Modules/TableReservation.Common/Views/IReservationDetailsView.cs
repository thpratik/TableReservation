using TableReservation.Common.ViewModel;

namespace TableReservation.Common.View
{
    public interface IReservationDetailsView
    {
        IReservationDetailsViewModel ViewModel { get; }
    }
}
