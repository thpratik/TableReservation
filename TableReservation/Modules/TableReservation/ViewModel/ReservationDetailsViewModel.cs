using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Unity;
using System.Collections.ObjectModel;
using TableReservation.Common.Base.ViewModel;
using TableReservation.Common.BusinessServices;
using TableReservation.Common.Models;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Commands;
using System;
using TableReservation.ApplicationServices.MessageBox;
using TableReservation.ApplicationServices.DialogBox;
using TableReservation.Common.Base.Models;
using TableReservation.Common.ViewModel;
using System.Collections.Generic;

namespace TableReservation.ViewModel
{
    public class ReservationDetailsViewModel : ViewModelBase, IReservationDetailsViewModel
    {
        private short _minFromHour;
        private short _maxFromHour;

        private short _fromHour;
        private short _toHour;

        private Reservation _currentReservation;
        private ILoggerFacade _logger;
        private IUnityContainer _container;
        private IReservationManager _reservationManager;
        private IMessageBoxService _messageBoxService;
        private IDialogBoxService _dialogBoxService;
        private ITableManager _tableManager;
        private ObservableCollection<Table> _availableTables;
        private ObservableCollection<Table> _reservedTables;
        private Table _selectedAvailableTable;
        private Table _selectedReservedTable;

        public ReservationDetailsViewModel(IUnityContainer container, ILoggerFacade logger, IReservationManager reservationManager, ITableManager tableManager, IMessageBoxService messageBoxService, IDialogBoxService dialogBoxService)
        {
            this._logger = logger;
            this._container = container;
            this._reservationManager = reservationManager;
            this._tableManager = tableManager;
            this._messageBoxService = messageBoxService;
            this._dialogBoxService = dialogBoxService;

            var allTables = this._tableManager.GetAll().ToDictionary(tbl => tbl.TableId, tbl => tbl);

            // Assumption : Reservation duration is between 10 Am and 10 Pm
            this._minFromHour = 10;
            this._maxFromHour = 22;

            this._reservedTables = new ObservableCollection<Table>();
            var currentReservation = this._container.Resolve(typeof(Reservation), "SelectedReservation");
            if (currentReservation != null)
            {
                this.FromHour = (short)(currentReservation as Reservation).TimeFrom;
                this.ToHour = (short)(currentReservation as Reservation).TimeTo;
                this._currentReservation = currentReservation as Reservation;
                this._currentReservation.PropertyChanged += _currentReservation_PropertyChanged;

                foreach (var tableId in this._currentReservation.ReservedTableIds)
                {
                    this._reservedTables.Add(allTables[tableId]);
                }

                this._availableTables = new ObservableCollection<Table>(this.GetAvailableTables((ushort)this._fromHour, (ushort)this._toHour));
                if (this._availableTables != null && this._availableTables.Count > 0)
                {
                    this._selectedAvailableTable = this._availableTables.First();
                }
            }
            else
            {
                this.FromHour = this._minFromHour;
            }

            if (this._reservedTables != null && this._reservedTables.Count > 0)
            {
                this._selectedReservedTable = this._reservedTables.First();
            }

            this.SaveCommand = new DelegateCommand(this.OnSaveCommand, () =>
            {
                return this._currentReservation != null &&
                    this._reservedTables != null &&
                    this._reservedTables.Count > 0 &&
                    !string.IsNullOrEmpty(this._currentReservation.CustomerName) &&
                    !string.IsNullOrEmpty(this._currentReservation.ContactNumber);
            });

            this.CancelCommand = new DelegateCommand(this.OnCancelCommand, () => { return this._currentReservation != null; });

            this.ReserveTableCommand = new DelegateCommand(this.OnReserveTableCommand, () =>
            {
                return this._currentReservation != null &&
                    this._selectedAvailableTable != null &&
                    this._currentReservation.NoOfPersons > this._reservedTables.Sum(tbl => tbl.MaxOccupancy);
            });

            this.UnreserveTableCommand = new DelegateCommand(this.OnUnreserveTableCommand, () =>
            {
                return this._currentReservation != null &&
                    this._selectedReservedTable != null;
            });
        }

        void _currentReservation_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("NoOfPersons"))
            {
                foreach (var table in this._reservedTables)
                {
                    this._availableTables.Add(table);
                }

                this._reservedTables.Clear();
                this.ReserveTableCommand.RaiseCanExecuteChanged();
            }
            else if (e.PropertyName.Equals("CustomerName") || e.PropertyName.Equals("ContactNumber"))
            {
                this.SaveCommand.RaiseCanExecuteChanged();
            }
            else if (e.PropertyName.Equals("TimeFrom") || e.PropertyName.Equals("TimeTo"))
            {
                this.AvailableTables = new ObservableCollection<Table>(this.GetAvailableTables((ushort)this._fromHour, (ushort)this._toHour));
            }
        }

        public short MinFromHour
        {
            get
            {
                return this._minFromHour;
            }
        }

        public short MaxFromHour
        {
            get
            {
                return this._maxFromHour;
            }
        }

        public short FromHour
        {
            get
            {
                return this._fromHour;
            }

            set
            {
                this._fromHour = value;
                this.NotifyPropertyChange("FromHour");

                this._toHour = (short)(this._fromHour + 1);
                this.NotifyPropertyChange("ToHour");

                if (this._currentReservation != null)
                {
                    this._currentReservation.TimeFrom = (ushort)this._fromHour;
                    this._currentReservation.TimeTo = (ushort)this._toHour;
                }
            }
        }

        public short ToHour
        {
            get
            {
                return this._toHour;
            }

            set
            {
                this._toHour = value;
                this.NotifyPropertyChange("ToHour");

                if (this._currentReservation != null)
                {
                    this._currentReservation.TimeTo = (ushort)this._toHour;
                }
            }
        }


        private void OnReserveTableCommand()
        {
            try
            {
                var availableTable = this.SelectedAvailableTable;
                this.AvailableTables.Remove(availableTable);
                this.ReservedTables.Add(availableTable);

                if (this._availableTables.Count > 0)
                {
                    this.SelectedAvailableTable = this._availableTables.First();
                }
                else
                {
                    this.SelectedAvailableTable = null;
                }

                this.SaveCommand.RaiseCanExecuteChanged();
                this.ReserveTableCommand.RaiseCanExecuteChanged();
                this.UnreserveTableCommand.RaiseCanExecuteChanged();
            }
            catch (Exception ex)
            {
                if (this._logger != null)
                {
                    this._logger.Log(ex.Message, Category.Exception, Priority.High);
                }
            }
        }

        private void OnUnreserveTableCommand()
        {
            try
            {
                var reservedTable = this.SelectedReservedTable;
                this.ReservedTables.Remove(reservedTable);
                this.AvailableTables.Add(reservedTable);

                if (this._reservedTables.Count > 0)
                {
                    this.SelectedReservedTable = this._reservedTables.First();
                }
                else
                {
                    this.SelectedReservedTable = null;
                }

                this.SaveCommand.RaiseCanExecuteChanged();
                this.ReserveTableCommand.RaiseCanExecuteChanged();
                this.UnreserveTableCommand.RaiseCanExecuteChanged();
            }
            catch (Exception ex)
            {
                if (this._logger != null)
                {
                    this._logger.Log(ex.Message, Category.Exception, Priority.High);
                }
            }
        }

        private void OnSaveCommand()
        {
            try
            {
                this._currentReservation.ReservedTableIds.Clear();
                foreach (var table in this._reservedTables)
                {
                    this._currentReservation.ReservedTableIds.Add(table.TableId);
                }

                this._reservationManager.Save(this._currentReservation);
                this._messageBoxService.ShowMessageBox("Reservation is saved successfully..!!", "Reservation");
                TableReservation.Common.ReservationsUpdatedEvent.Instance.Publish(this._currentReservation.ReservationId);
                this._dialogBoxService.CloseUserDialog();
            }
            catch (Exception ex)
            {
                if (this._logger != null)
                {
                    this._logger.Log(ex.Message, Category.Exception, Priority.High);
                }
            }
        }

        private void OnCancelCommand()
        {
            try
            {
                this._dialogBoxService.CloseUserDialog();
            }
            catch (Exception ex)
            {
                if (this._logger != null)
                {
                    this._logger.Log(ex.Message, Category.Exception, Priority.High);
                }
            }
        }

        private IEnumerable<Table> GetAvailableTables(ushort fromTime, ushort toTime)
        {
            var availableTalbes = new List<Table>();
            var allReservations = this._reservationManager.GetAll();
            var allTables = this._tableManager.GetAll().ToDictionary(tbl => tbl.TableId, tbl => tbl);
            var reservedTables = new Dictionary<Guid, List<ushort>>();

            var expectedAvailableHours = new List<ushort>();
            for (ushort rsHr = fromTime; rsHr <= toTime; rsHr++)
            {
                expectedAvailableHours.Add(rsHr);
            }

            foreach (var reservation in allReservations)
            {
                foreach (var tableId in reservation.ReservedTableIds)
                {
                    if (!reservedTables.Keys.Contains(tableId))
                    {
                        reservedTables.Add(tableId, new List<ushort>());
                    }

                    for (ushort rsHr = reservation.TimeFrom; rsHr <= reservation.TimeTo; rsHr++)
                    {
                        reservedTables[tableId].Add(rsHr);
                    }
                }
            }

            availableTalbes.AddRange(allTables.Where(tbl => !reservedTables.Keys.Contains(tbl.Key)).Select(tbl => tbl.Value));

            foreach (var table in reservedTables)
            {
                if (table.Value.Intersect(expectedAvailableHours).Count() == 0)
                {
                    availableTalbes.Add(allTables[table.Key]);
                }
            }

            return availableTalbes;
        }

        public Reservation CurrentReservation
        {
            get
            {
                return this._currentReservation;
            }

            set
            {
                this._currentReservation = value;
                this.NotifyPropertyChange("CurrentReservation");
            }
        }

        public DelegateCommand SaveCommand { get; private set; }
        public DelegateCommand CancelCommand { get; private set; }

        public DelegateCommand ReserveTableCommand { get; private set; }
        public DelegateCommand UnreserveTableCommand { get; private set; }

        public ObservableCollection<Table> AvailableTables
        {
            get
            {
                return this._availableTables;
            }

            set
            {
                this._availableTables = value;
                this.NotifyPropertyChange("AvailableTables");
            }
        }

        public ObservableCollection<Table> ReservedTables
        {
            get
            {
                return this._reservedTables;
            }

            set
            {
                this._reservedTables = value;
                this.NotifyPropertyChange("ReservedTables");
            }
        }

        public Table SelectedAvailableTable
        {
            get
            {
                return this._selectedAvailableTable;
            }

            set
            {
                this._selectedAvailableTable = value;
                this.ReserveTableCommand.RaiseCanExecuteChanged();
                this.NotifyPropertyChange("SelectedAvailableTable");
            }
        }

        public Table SelectedReservedTable
        {
            get
            {
                return this._selectedReservedTable;
            }

            set
            {
                this._selectedReservedTable = value;
                this.UnreserveTableCommand.RaiseCanExecuteChanged();
                this.NotifyPropertyChange("SelectedReservedTable");
            }
        }
    }
}
