using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableReservation.Common.Base.Models;

namespace TableReservation.Models
{
    public class ReservationHour : ModelBase
    {
        public ReservationHour(int hour)
        {
            this.Hour = hour;
        }

        private int _hour;
        public int Hour
        {
            get
            {
                return this._hour;
            }

            set
            {
                this._hour = value;
            }
        }

        public string DisplayText
        {
            get
            {
                return DateTime.MinValue.AddHours(this._hour).ToString("h tt");
            }
        }
    }
}
