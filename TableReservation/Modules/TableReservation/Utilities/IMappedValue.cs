using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableReservation.Utilities
{
    public interface IMappedValue
    {
        object ColumnBinding { get; set; }
        object RowBinding { get; set; }
        object Value { get; set; }
    }
}
