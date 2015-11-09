namespace TableReservation.DataServices
{
    using Microsoft.Practices.Prism.Logging;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;
    using System.Linq;
    using System.Xml.Linq;
    using TableReservation.Common.DataServices;

    public class XMLDataService : DataServiceBase, ITableDataService, IReservationDataService
    {
        private ILoggerFacade _logger;
        private string _directoryPath;

        public XMLDataService(ILoggerFacade logger, string objectTypeName)
            : base()
        {
            try
            {
                this._logger = logger;
                this._directoryPath = Path.Combine(this._dataDirectoryPath, objectTypeName);
                if (!Directory.Exists(this._directoryPath))
                {
                    Directory.CreateDirectory(this._directoryPath);
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

        public object GetObjectByID(Guid id)
        {
            object returnValue = null;

            try
            {
                var fileStream = base.GetFileReadStream(Path.Combine(this._directoryPath, id.ToString() + ".xml"));
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

        public List<object> GetAllObjects()
        {
            var returnValue = new List<object>();
            try
            {
                var allObjects = base.GetAllFiles(this._directoryPath);
                foreach (var obj in allObjects)
                {
                    var fileStream = base.GetFileReadStream(obj.Value);
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

        public bool SaveObject(Guid id, object obj)
        {
            var returnValue = false;

            try
            {
                var fileName = Path.Combine(this._directoryPath, id + ".xml");
                var fileStream = base.GetFileWriteStream(fileName);
                var xmlTextWriter = new XmlTextWriter(fileStream, encoding);

                Serializer.Serialize(xmlTextWriter, obj);

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

        public bool SaveAllObjects(Dictionary<Guid, object> objects)
        {
            try
            {
                foreach (var obj in objects)
                {
                    this.SaveObject(obj.Key, obj.Value);
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

        public bool DeleteObject(Guid id)
        {
            try
            {
                return base.DeleteFile(Path.Combine(this._directoryPath, id.ToString() + ".xml"));
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

        public bool DeleteAllObjects(Dictionary<Guid, object> objects)
        {
            try
            {
                foreach (var obj in objects)
                {
                    this.DeleteObject(obj.Key);
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

        public List<XElement> GetXElements(string fileName, string rootName, string elementName)
        {
            if (string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(rootName) || string.IsNullOrEmpty(elementName) || !System.IO.File.Exists(fileName))
                return new List<XElement>();

            var xDocument = XDocument.Load(fileName);
            var xElements = xDocument.Descendants(rootName).Elements(elementName);
            if (xElements != null)
            {
                return xElements.ToList<XElement>();
            }
            else
            {
                return new List<XElement>();
            }
        }
    }
}

