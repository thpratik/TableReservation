namespace TableReservation.DataServices
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;
    using TableReservation.Common;

    public abstract class DataServiceBase
    {
        protected string _dataDirectoryPath;

        protected Encoding encoding;
        public XmlSerializer Serializer { get; set; }

        public DataServiceBase()
        {
            if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["DataDirectory"]))
            {
                throw new System.Exception("DataDirectory is not found");
            }
            else
            {
                if (System.IO.Path.IsPathRooted(ConfigurationManager.AppSettings["DataDirectory"]))
                {
                    this._dataDirectoryPath = ConfigurationManager.AppSettings["DataDirectory"];
                }
                else
                {
                    this._dataDirectoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["DataDirectory"]);
                }

                if (!Directory.Exists(this._dataDirectoryPath))
                {
                    Directory.CreateDirectory(this._dataDirectoryPath);
                }
            }

            // TODO: encoding can be configured
            encoding = new UTF8Encoding();
        }

        protected object CheckParamValue(DateTime paramValue)
        {
            if (paramValue.Equals(DefaultConstants.NullDateTime))
            {
                return DBNull.Value;
            }

            return paramValue;
        }

        protected object CheckParamValue(decimal paramValue)
        {
            if (paramValue.Equals(DefaultConstants.NullDecimal))
            {
                return DBNull.Value;
            }

            return paramValue;
        }

        protected object CheckParamValue(double paramValue)
        {
            if (paramValue.Equals(DefaultConstants.NullDouble))
            {
                return DBNull.Value;
            }

            return paramValue;
        }

        protected object CheckParamValue(Guid paramValue)
        {
            if (paramValue.Equals(DefaultConstants.NullGuid))
            {
                return DBNull.Value;
            }

            return paramValue;
        }

        protected object CheckParamValue(int paramValue)
        {
            if (paramValue.Equals(DefaultConstants.NullInt))
            {
                return DBNull.Value;
            }

            return paramValue;
        }

        protected object CheckParamValue(float paramValue)
        {
            if (paramValue.Equals(DefaultConstants.NullFloat))
            {
                return DBNull.Value;
            }

            return paramValue;
        }

        protected object CheckParamValue(string paramValue)
        {
            if (string.IsNullOrEmpty(paramValue))
            {
                return DBNull.Value;
            }

            return paramValue;
        }

        protected Guid GetGuid(object value)
        {
            Guid nullGuid = DefaultConstants.NullGuid;
            if (value is string)
            {
                return new Guid((string)value);
            }

            if (value is Guid)
            {
                nullGuid = (Guid)value;
            }

            return nullGuid;
        }

        protected bool DeleteFile(string filePath)
        {
            var returnValue = false;
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                returnValue = true;
            }
            catch (Exception ex)
            {
                returnValue = false;
            }

            return returnValue;
        }

        protected Stream GetFileReadStream(string filePath)
        {
            var returnValue = Stream.Null;
            if (!File.Exists(filePath))
            {
                throw new System.Exception(filePath + " is not found");
            }

            var streamReader = new StreamReader(filePath, encoding);
            returnValue = streamReader.BaseStream;

            return returnValue;
        }

        protected Stream GetFileWriteStream(string filePath)
        {
            var returnValue = Stream.Null;
            var streamReader = new StreamWriter(filePath, false, encoding);
            returnValue = streamReader.BaseStream;

            return returnValue;
        }

        protected Dictionary<Guid, string> GetAllFiles(string directoryPath)
        {
            var returnValue = new Dictionary<Guid, string>();
            var allFiles = Directory.GetFiles(directoryPath, "*.xml");
            foreach (var filePath in allFiles)
            {
                returnValue.Add(GetGuid(Path.GetFileName(filePath).Replace(".xml", string.Empty)), filePath);
            }

            return returnValue;
        }
    }
}

