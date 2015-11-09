namespace TableReservation.DataServices
{
    using Microsoft.Practices.Prism.Logging;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;
    using TableReservation.Common.DataServices;

    public class ReservationDataService : DataServiceBase, IReservationDataService
    {
        private ILoggerFacade _logger;
        private string _reservationDirectoryPath;

        public ReservationDataService(ILoggerFacade logger)
        {
            try
            {
                this._logger = logger;
                this._reservationDirectoryPath = Path.Combine(this._dataDirectoryPath, @"Reservations");
                if (!Directory.Exists(this._reservationDirectoryPath))
                {
                    Directory.CreateDirectory(this._reservationDirectoryPath);
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

        public object Reservation_GetByID(Guid reservationId)
        {
            object returnValue = null;

            try
            {
                var fileStream = base.GetFileReadStream(Path.Combine(this._reservationDirectoryPath, reservationId.ToString() + ".xml"));
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

        public List<object> Reservation_GetAll()
        {
            var returnValue = new List<object>();
            try
            {
                var allReservations = base.GetAllFiles(this._reservationDirectoryPath);
                foreach (var reservation in allReservations)
                {
                    var fileStream = base.GetFileReadStream(reservation.Value);
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

        public bool Reservation_Save(Guid reservationId, object reservation)
        {
            var returnValue = false;

            try
            {
                var fileName = Path.Combine(this._reservationDirectoryPath, reservationId + ".xml");
                var fileStream = base.GetFileWriteStream(fileName);
                var xmlTextWriter = new XmlTextWriter(fileStream, encoding);

                Serializer.Serialize(xmlTextWriter, reservation);

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

        public bool Reservation_SaveAll(Dictionary<Guid, object> reservations)
        {
            try
            {
                foreach (var reservation in reservations)
                {
                    this.Reservation_Save(reservation.Key, reservation.Value);
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

        public bool Reservation_Delete(Guid reservationId)
        {
            try
            {
                return base.DeleteFile(Path.Combine(this._reservationDirectoryPath, reservationId.ToString() + ".xml"));
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

        public bool Reservation_DeleteAll(Dictionary<Guid, object> reservations)
        {
            try
            {
                foreach (var reservation in reservations)
                {
                    this.Reservation_Delete(reservation.Key);
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

