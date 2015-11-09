using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TableReservation.Common.Base.Models;

namespace TableReservation.Common.Models
{
    public class Table : ModelBase
    {
        private Guid _tableId;
        private string _displayName;
        private ushort _maxOccupancy;

        public Table()
        {
            this._tableId = Guid.NewGuid();
        }

        public Guid TableId
        {
            get
            {
                return this._tableId;
            }

            set
            {
                this._tableId = value;
                this.NotifyPropertyChange("TableId");
            }
        }

        public string DisplayName
        {
            get
            {
                return this._displayName;
            }

            set
            {
                this._displayName = value;
                this.NotifyPropertyChange("DisplayName");
            }
        }

        public ushort MaxOccupancy
        {
            get
            {
                return this._maxOccupancy;
            }

            set
            {
                this._maxOccupancy = value;
                this.NotifyPropertyChange("MaxOccupancy");
            }
        }
    }
}
