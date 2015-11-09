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
    public class ReservationManager : IReservationManager
    {
        private IReservationDataService _reservationDataService;
        private ILoggerFacade _logger;
        private bool _isDirty = true;
        private ObservableCollection<Reservation> _reservationCollection;

        public ReservationManager(ILoggerFacade logger, IReservationDataService reservationDataService)
        {
            this._logger = logger;
            this._reservationDataService = reservationDataService;
            this._reservationDataService.Serializer = new XmlSerializer(typeof(Reservation));
            this._reservationCollection = new ObservableCollection<Reservation>();
        }

        public bool Save(Reservation reservation)
        {
            try
            {
                _isDirty = true;
                return this._reservationDataService.SaveObject(reservation.ReservationId, reservation);
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

        public bool Delete(Reservation reservation)
        {
            try
            {
                _isDirty = true;
                return _reservationDataService.DeleteObject(reservation.ReservationId);
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

        public Reservation Get(Guid reservationId)
        {
            try
            {
                var nbmBoard = _reservationDataService.GetObjectByID(reservationId);
                if (nbmBoard != null)
                {
                    return (Reservation)nbmBoard;
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

        public ObservableCollection<Reservation> GetAll()
        {
            if (_isDirty)
            {
                this._reservationCollection.Clear();
                try
                {
                    var allAvailableReservations = this._reservationDataService.GetAllObjects();
                    foreach (var reservation in allAvailableReservations)
                    {
                        this._reservationCollection.Add(reservation as Reservation);
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

            return this._reservationCollection;
        }

        public bool SaveAll(List<Reservation> reservations)
        {
            try
            {
                this._reservationDataService.DeleteAllObjects(reservations.ToDictionary(res => res.ReservationId, res => (object)res));
                this._reservationCollection.Clear();

                var allReservations = new Dictionary<Guid, object>();
                foreach (var reservation in reservations)
                {
                    this._reservationCollection.Add(reservation);
                    allReservations[reservation.ReservationId] = reservation;
                }

                return this._reservationDataService.SaveAllObjects(allReservations);
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
