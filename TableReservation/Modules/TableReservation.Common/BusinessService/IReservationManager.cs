using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using TableReservation.Common.Models;

namespace TableReservation.Common.BusinessServices
{
    public interface IReservationManager
    {
        bool Save(Reservation reservation);

        bool Delete(Reservation reservation);

        Reservation Get(Guid reservationId);

        ObservableCollection<Reservation> GetAll();

        bool SaveAll(List<Reservation> reservations);
    }
}
