using System.Collections.ObjectModel;
using System.Linq;

namespace TableReservation.Utilities
{
    public class MappedValueCollection : ObservableCollection<MappedValue>
    {
        public MappedValueCollection()
        {
        }

        public bool Exist(object ColumnBinding, object RowBinding)
        {
            return this.Count(x => x.RowBinding == RowBinding && x.ColumnBinding == ColumnBinding) > 0;
        }

        public MappedValue ReturnIfExistAddIfNot(object ColumnBinding, object RowBinding)
        {
            MappedValue value = null;

            if (Exist(ColumnBinding, RowBinding))
            {
                return this.Where(x => x.RowBinding == RowBinding && x.ColumnBinding == ColumnBinding).Single();
            }
            else
            {
                value = new MappedValue();
                value.ColumnBinding = ColumnBinding;
                value.RowBinding = RowBinding;
                this.Add(value);
            }
            return value; 
        }

        public void RemoveByColumn(object ColumnBinding)
        {
            foreach (var item in this.Where(x => x.ColumnBinding == ColumnBinding).ToList())
                this.Remove(item); 
        }

        public void RemoveByRow(object RowBinding)
        {
            foreach (var item in this.Where(x => x.RowBinding == RowBinding).ToList())
                this.Remove(item); 
        }
    }
}
