namespace TableReservation.DataServices
{
    using Microsoft.Practices.Prism.Logging;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;
    using TableReservation.Common.DataServices;

    public class TableDataService : DataServiceBase, ITableDataService
    {
        private ILoggerFacade _logger;
        private string _tableDirectoryPath;

        public TableDataService(ILoggerFacade logger)
        {
            try
            {
                this._logger = logger;
                this._tableDirectoryPath = Path.Combine(this._dataDirectoryPath, @"Tables");
                if (!Directory.Exists(this._tableDirectoryPath))
                {
                    Directory.CreateDirectory(this._tableDirectoryPath);
                }
            }
            catch (Exception ex)
            {
                if (this._logger != null)
                {
                    this._logger.Log(ex.Message, Category.Exception, Priority.High);
                }
            }
        }

        public object Table_GetByID(Guid tableId)
        {
            object returnValue = null;

            try
            {
                var fileStream = base.GetFileReadStream(Path.Combine(this._tableDirectoryPath, tableId.ToString() + ".xml"));
                var xmlTextReader = new XmlTextReader(fileStream);

                returnValue = Serializer.Deserialize(xmlTextReader);

                fileStream.Close();
                fileStream.Dispose();

                xmlTextReader.Close();
                xmlTextReader.Dispose();
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

        public List<object> Table_GetAll()
        {
            var returnValue = new List<object>();
            try
            {
                var allTables = base.GetAllFiles(this._tableDirectoryPath);
                foreach (var table in allTables)
                {
                    var fileStream = base.GetFileReadStream(table.Value);
                    var xmlTextReader = new XmlTextReader(fileStream);

                    returnValue.Add(Serializer.Deserialize(xmlTextReader));

                    fileStream.Close();
                    fileStream.Dispose();

                    xmlTextReader.Close();
                    xmlTextReader.Dispose();
                }
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

        public bool Table_Save(Guid tableId, object table)
        {
            var returnValue = false;

            try
            {
                var fileName = Path.Combine(this._tableDirectoryPath, tableId + ".xml");
                var fileStream = base.GetFileWriteStream(fileName);
                var xmlTextWriter = new XmlTextWriter(fileStream, encoding);

                Serializer.Serialize(xmlTextWriter, table);

                fileStream.Close();
                fileStream.Dispose();

                xmlTextWriter.Close();
                xmlTextWriter.Dispose();
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

        public bool Table_SaveAll(Dictionary<Guid, object> tables)
        {
            try
            {
                foreach (var table in tables)
                {
                    this.Table_Save(table.Key, table.Value);
                }

                return true;
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

        public bool Table_Delete(Guid tableId)
        {
            try
            {
                return base.DeleteFile(Path.Combine(this._tableDirectoryPath, tableId.ToString() + ".xml"));
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

        public bool Table_DeleteAll(Dictionary<Guid, object> tables)
        {
            try
            {
                foreach (var table in tables)
                {
                    this.Table_Delete(table.Key);
                }

                return true;
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
    }
}

