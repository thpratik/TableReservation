using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Data;
using TableReservation.ApplicationServices.DialogBox;
using TableReservation.ApplicationServices.MessageBox;
using TableReservation.Common.Base.ViewModel;
using TableReservation.Common.BusinessServices;
using TableReservation.Common.Models;
using TableReservation.Common.ViewModel;
using TableReservation.Models;
using TableReservation.Utilities;

namespace TableReservation.ViewModel
{
    public class ReservationDashBoardViewModel : ViewModelBase, IReservationDashBoardViewModel
    {
        private string _tableXMLFile;

        private short _minFromHour;
        private short _maxFromHour;

        private short _fromHour;
        private short _toHour; 
        private ushort _noOfPersons;

        private ILoggerFacade _logger;
        private IUnityContainer _container;
        private IReservationManager _reservationManager;
        private ITableManager _tableManager;
        private IMessageBoxService _messageBoxService;
        private IDialogBoxService _dialogBoxService;
        private ObservableCollection<Table> _tables;
        private ObservableCollection<ReservationHour> _reservationHours;
        private MappedValueCollection _reservations;

        public ReservationDashBoardViewModel(IUnityContainer container, ILoggerFacade logger, IReservationManager reservationManager, ITableManager tableManager, IMessageBoxService messageBoxService, IDialogBoxService dialogBoxService)
        {
            this._logger = logger;
            this._container = container;
            this._reservationManager = reservationManager;
            this._tableManager = tableManager;
            this._messageBoxService = messageBoxService;
            this._dialogBoxService = dialogBoxService;
            this._reservationHours = new ObservableCollection<ReservationHour>();
            this._reservations = new MappedValueCollection();


            this.AddCommand = new DelegateCommand(this.OnAddCommand, () => { return this._noOfPersons > 0; });
            this.BrowseCommand = new DelegateCommand(this.OnBrowseCommand);
            this.ImportCommand = new DelegateCommand(this.OnImportCommand, () => { return !string.IsNullOrEmpty(this._tableXMLFile); });

            this._tables = this._tableManager.GetAll();
            
            // Assumption : Reservation duration is between 10 Am and 10 Pm
            this._minFromHour = 10;
            this._maxFromHour = 22;

            for (int hour = this._minFromHour; hour <= this._maxFromHour; hour++)
            {
                this._reservationHours.Add(new ReservationHour(hour));
            }

            this.FromHour = this._minFromHour;

            TableReservation.Common.ReservationsUpdatedEvent.Instance.Subscribe(this.ReservationsUpdated);
            this.ReservationsUpdated(Guid.NewGuid());
        }

        private void ReservationsUpdated(Guid reservationId)
        {
            this.FromHour = this._minFromHour;
            this.NoOfPersons = 0;

            this._reservations = new MappedValueCollection();
            var allReservations = this._reservationManager.GetAll();
            foreach (var reservation in allReservations)
            {
                var reservedTables = this._tableManager.GetAll().Where(tbl => reservation.ReservedTableIds.Contains(tbl.TableId));
                var reservedHours = new List<int>();
                for (int reservedHour = reservation.TimeFrom; reservedHour <= reservation.TimeTo; reservedHour++)
                {
                    reservedHours.Add(reservedHour);
                }

                foreach (var reservedHour in reservedHours)
                {
                    foreach (var table in reservedTables)
                    {
                        this._reservations.Add(new MappedValue()
                        {
                            RowBinding = table,
                            ColumnBinding = this.ReservationHours.Where(rsHr => rsHr.Hour.Equals(reservedHour)).First(),
                            Value = true
                        });
                    }
                }
            }

            this.NotifyPropertyChange("Reservations");
        }

        public ObservableCollection<Table> Tables
        {
            get
            {
                return this._tables;
            }

            set
            {
                this._tables = value;
                this.NotifyPropertyChange("Tables");
            }
        }

        public ushort NoOfPersons
        {
            get
            {
                return this._noOfPersons;
            }

            set
            {
                this._noOfPersons = value;
                this.NotifyPropertyChange("NoOfPersons");
                this.AddCommand.RaiseCanExecuteChanged();
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
            }
        }

        private void OnBrowseCommand()
        {
            try
            {
                string xmlFileName;
                var openFileDialogResult = this._dialogBoxService.ShowOpenXMLFileDialog(out xmlFileName);
                if (openFileDialogResult == DialogBoxResult.OK)
                {
                    this.TableXMLFile = xmlFileName;
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

        private void OnAddCommand()
        {
            try
            {
                var selectedReservation = new Reservation();
                selectedReservation.TimeFrom = (ushort)this.FromHour;
                selectedReservation.TimeTo = (ushort)this.ToHour;
                selectedReservation.NoOfPersons = this.NoOfPersons;
                this._container.RegisterInstance<Reservation>("SelectedReservation", selectedReservation);

                var openFileDialogResult = this._dialogBoxService.ShowUserDialog("Add Reservation", Common.ViewNames.ReservationDetailsView);
                if (openFileDialogResult == DialogBoxResult.OK)
                {
                    //
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

        private void OnImportCommand()
        {
            try
            {
                var importTablesConfirmation = this._messageBoxService.ShowMessageBox("Import Table action will delete all existing tables and reservations.\nAre you sure you want to import tables?", "Import Table", MessageBoxButtons.YesNo);
                if (importTablesConfirmation == MessageBoxResults.Yes)
                {
                    var allCurrentTables = this._tableManager.GetAll();
                    foreach (var table in allCurrentTables)
                    {
                        this._tableManager.Delete(table);
                    }

                    var allCurrentReservations = this._reservationManager.GetAll();
                    foreach (var reservation in allCurrentReservations)
                    {
                        this._reservationManager.Delete(reservation);
                    }

                    this._tableManager.ImportTables(this._tableXMLFile);
                    this.Tables = this._tableManager.GetAll();

                    TableReservation.Common.ReservationsUpdatedEvent.Instance.Publish(Guid.NewGuid());
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

        public DelegateCommand AddCommand { get; private set; }
        public DelegateCommand BrowseCommand { get; private set; }
        public DelegateCommand ImportCommand { get; private set; }

        public string TableXMLFile
        {
            get
            {
                return this._tableXMLFile;
            }

            set
            {
                this._tableXMLFile = value;
                this.NotifyPropertyChange("TableXMLFile");
                this.ImportCommand.RaiseCanExecuteChanged();
            }
        }

        public ObservableCollection<ReservationHour> ReservationHours
        {
            get
            {
                return this._reservationHours;
            }
        }

        public MappedValueCollection Reservations
        {
            get
            {
                return this._reservations;
            }
        }
    }
}
