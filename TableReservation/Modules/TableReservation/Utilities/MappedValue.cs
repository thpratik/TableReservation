using TableReservation.Common.Base.ViewModel;

namespace TableReservation.Utilities
{
    public class MappedValue : ViewModelBase, IMappedValue
    {
        object value; 
        public object ColumnBinding { get; set; }
        public object RowBinding { get; set; }
        public object Value
        {
            get
            {
                return value; 
            }
            set
            {
                if (this.value != value)
                {
                    this.value = value;
                    this.NotifyPropertyChange("Value");
                }
            }
        }
    }
}
