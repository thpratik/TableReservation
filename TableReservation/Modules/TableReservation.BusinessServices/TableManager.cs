using Microsoft.Practices.Prism.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Serialization;
using TableReservation.Common.BusinessServices;
using TableReservation.Common.DataServices;
using TableReservation.Common.Models;

namespace TableReservation.BusinessServices
{
    public class TableManager : ITableManager
    {
        private ITableDataService _tableDataService;
        private ILoggerFacade _logger;
        private bool _isDirty = true;
        private ObservableCollection<Table> _tableCollection;

        public TableManager(ILoggerFacade logger, ITableDataService tableDataService)
        {
            this._logger = logger;
            this._tableDataService = tableDataService;
            this._tableDataService.Serializer = new XmlSerializer(typeof(Table));
            this._tableCollection = new ObservableCollection<Table>();
        }

        public bool Save(Table table)
        {
            try
            {
                _isDirty = true;
                return this._tableDataService.SaveObject(table.TableId, table);
            }
            catch (Exception ex)
            {
                if (this._logger != null)
                {
                    this._logger.Log(ex.Message, Category.Exception, Priority.High);
                }
            }

            return false;
        }

        public bool Delete(Table table)
        {
            try
            {
                _isDirty = true;
                return _tableDataService.DeleteObject(table.TableId);
            }
            catch (Exception ex)
            {
                if (this._logger != null)
                {
                    this._logger.Log(ex.Message, Category.Exception, Priority.High);
                }
            }

            return false;
        }

        public Table Get(Guid tableId)
        {
            try
            {
                var nbmBoard = _tableDataService.GetObjectByID(tableId);
                if (nbmBoard != null)
                {
                    return (Table)nbmBoard;
                }
            }
            catch (Exception ex)
            {
                if (this._logger != null)
                {
                    this._logger.Log(ex.Message, Category.Exception, Priority.High);
                }
            }

            return null;
        }

        public ObservableCollection<Table> GetAll()
        {
            if (_isDirty)
            {
                this._tableCollection.Clear();
                try
                {
                    var allAvailableTables = this._tableDataService.GetAllObjects();
                    foreach (var table in allAvailableTables)
                    {
                        this._tableCollection.Add(table as Table);
                    }
                }
                catch (Exception ex)
                {
                    if (this._logger != null)
                    {
                        this._logger.Log(ex.Message, Category.Exception, Priority.High);
                    }
                }

                _isDirty = false;
            }

            return this._tableCollection;
        }

        public bool SaveAll(List<Table> tables)
        {
            try
            {
                this._tableDataService.DeleteAllObjects(tables.ToDictionary(tab => tab.TableId, tab => (object)tab));
                this._tableCollection.Clear();

                var allTables = new Dictionary<Guid, object>();
                foreach (var table in tables)
                {
                    this._tableCollection.Add(table);
                    allTables[table.TableId] = table;
                }

                return this._tableDataService.SaveAllObjects(allTables);
            }
            catch (Exception ex)
            {
                if (this._logger != null)
                {
                    this._logger.Log(ex.Message, Category.Exception, Priority.High);
                }
            }

            return false;
        }

        public bool ImportTables(string fileName)
        {
            bool returnValue = false;

            try
            {
                var xmlTables = this._tableDataService.GetXElements(fileName, "Tables", "Table");
                var tables = new List<Table>();
                foreach (var xmlTable in xmlTables)
                {
                    var table = new Table();
                    table.DisplayName = string.Format("Table - {0}", xmlTable.Attribute("Id").Value);
                    ushort maxCapacity;
                    if (ushort.TryParse(xmlTable.Attribute("MaxOccupancy").Value, out maxCapacity))
                    {
                        table.MaxOccupancy = maxCapacity;
                        tables.Add(table);
                    }
                }

                if (tables.Count > 0)
                {
                    this.SaveAll(tables);
                }

                returnValue = true;
            }
            catch (Exception ex)
            {
                if (this._logger != null)
                {
                    this._logger.Log(ex.Message, Category.Exception, Priority.High);
                }
            }

            return returnValue;
        }
    }
}
