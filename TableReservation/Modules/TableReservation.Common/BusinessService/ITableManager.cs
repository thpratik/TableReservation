using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using TableReservation.Common.Models;

namespace TableReservation.Common.BusinessServices
{
    public interface ITableManager
    {
        bool Save(Table table);

        bool Delete(Table table);

        Table Get(Guid tableId);

        ObservableCollection<Table> GetAll();

        bool SaveAll(List<Table> tables);

        bool ImportTables(string fileName);
    }
}
