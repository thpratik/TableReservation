using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using TableReservation.Common.Base.Models;

namespace TableReservation.Common.Models
{
    public class Reservation : ModelBase
    {
        private Guid _reservationId;
        private string _customerName;
        private string _contactNumber;
        private string _email;
        private ushort _noOfPersons;
        private ushort _timeFrom;
        private ushort _timeTo;
        private ObservableCollection<Guid> _reservedTableIds;

        public Reservation()
        {
            this._reservationId = Guid.NewGuid();
            this._reservedTableIds = new ObservableCollection<Guid>();
        }

        public Guid ReservationId 
        {
            get
            {
                return this._reservationId;
            }

            set
            {
                this._reservationId = value;
                this.NotifyPropertyChange("ReservationId");
            }
        }

        public string CustomerName 
        {
            get
            {
                return this._customerName;
            }

            set
            {
                this._customerName = value;
                this.NotifyPropertyChange("CustomerName");
            }
        }

        public string ContactNumber 
        {
            get
            {
                return this._contactNumber;
            }

            set
            {
                this._contactNumber = value;
                this.NotifyPropertyChange("ContactNumber");
            }
        }

        public string EMail
        {
            get
            {
                return this._email;
            }

            set
            {
                this._email = value;
                this.NotifyPropertyChange("EMail");
            }
        }

        public ushort NoOfPersons
        {
            get
            {
                return this._noOfPersons;
            }

            set
            {
                this._noOfPersons = value;
                this.NotifyPropertyChange("NoOfPersons");
            }
        }

        public ushort TimeFrom
        {
            get
            {
                return this._timeFrom;
            }

            set
            {
                this._timeFrom = value;
                this.NotifyPropertyChange("TimeFrom");
            }
        }

        public ushort TimeTo
        {
            get
            {
                return this._timeTo;
            }

            set
            {
                this._timeTo = value;
                this.NotifyPropertyChange("TimeTo");
            }
        }

        public ObservableCollection<Guid> ReservedTableIds
        {
            get
            {
                return this._reservedTableIds;
            }

            set
            {
                this._reservedTableIds = value;
                this.NotifyPropertyChange("ReservedTableIds");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this._reservedTableIds != null))
            {
                this._reservedTableIds.Clear();
                this._reservedTableIds = null;
            }

            base.Dispose(disposing);
        }
    }
}
