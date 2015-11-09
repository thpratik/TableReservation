using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableReservation.Common
{
    public class ReservationsUpdatedEvent : CompositePresentationEvent<Guid>
    {
        private static readonly EventAggregator _eventAggregator;
        private static readonly ReservationsUpdatedEvent _event;

        static ReservationsUpdatedEvent()
        {
            _eventAggregator = new EventAggregator();
            _event = _eventAggregator.GetEvent<ReservationsUpdatedEvent>();
        }

        public static ReservationsUpdatedEvent Instance
        {
            get { return _event; }
        }
    }
}
