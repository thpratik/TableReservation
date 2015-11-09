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
using TableReservation.Common.ViewModel;
using TableReservation.Common;

namespace TableReservation.ViewModel
{
    public class ReservationViewModel : ViewModelBase, IReservationViewModel
    {
        private ObservableCollection<Reservation> _reservations;
        private Reservation _selectedReservation;
        private ILoggerFacade _logger;
        private IUnityContainer _container;
        private IReservationManager _reservationManager;
        private ITableManager _tableManager;
        private IMessageBoxService _messageBoxService;
        private IDialogBoxService _dialogBoxService;

        public ReservationViewModel(IUnityContainer container, ILoggerFacade logger, IReservationManager reservationManager, ITableManager tableManager, IMessageBoxService messageBoxService, IDialogBoxService dialogBoxService)
        {
            this._logger = logger;
            this._container = container;
            this._reservationManager = reservationManager;
            this._tableManager = tableManager;
            this._messageBoxService = messageBoxService;
            this._dialogBoxService = dialogBoxService;

            this.EditCommand = new DelegateCommand(this.OnEditCommand, () => { return this._selectedReservation != null; });
            this.DeleteCommand = new DelegateCommand(this.OnDeleteCommand, () => { return this._selectedReservation != null; });

            this.GetAllReservations();

            TableReservation.Common.ReservationsUpdatedEvent.Instance.Subscribe(this.ReservationsUpdated);
        }

        private void ReservationsUpdated(Guid reservationId)
        {
            this.GetAllReservations();
        }

        private void OnEditCommand()
        {
            try
            {
                this._container.RegisterInstance<Reservation>("SelectedReservation", this._selectedReservation);
                this._dialogBoxService.ShowUserDialog("Edit Reservation", ViewNames.ReservationDetailsView);
            }
            catch (Exception ex)
            {
                if (this._logger != null)
                {
                    this._logger.Log(ex.Message, Category.Exception, Priority.High);
                }
            }
        }

        private void OnDeleteCommand()
        {
            try
            {
                var confirmation = this._messageBoxService.ShowMessageBox("Are you sure you want to delete this Reservation?", "Delete Reservation", MessageBoxButtons.YesNo);
                if (confirmation == MessageBoxResults.Yes)
                {
                    this._reservationManager.Delete(this._selectedReservation);
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

        private void GetAllReservations()
        {
            this.Reservations = this._reservationManager.GetAll();
            if (this.Reservations.Count > 0)
            {
                this.SelectedReservation = this._reservations.First();
            }
        }

        public ObservableCollection<Reservation> Reservations 
        {
            get
            {
                return this._reservations;
            }

            set
            {
                this._reservations = value;
                this.NotifyPropertyChange("Reservations");
            }
        }

        public Reservation SelectedReservation 
        {
            get
            {
                return this._selectedReservation;
            }

            set
            {
                this._selectedReservation = value;
                this.NotifyPropertyChange("SelectedReservation");
                this.NotifyPropertyChange("SelectedReservationTables");
                this.EditCommand.RaiseCanExecuteChanged();
                this.DeleteCommand.RaiseCanExecuteChanged();
            }
        }

        public string SelectedReservationTables
        {
            get
            {
                var selectedReservationTables = "-";
                if (this._selectedReservation != null)
                {
                    var tables = this._tableManager.GetAll().ToDictionary(tbl => tbl.TableId, tbl => tbl);
                    var stringBuilder = new StringBuilder();
                    foreach (var tableId in this._selectedReservation.ReservedTableIds)
                    {
                        stringBuilder.Append(string.Format("{0}\n", tables[tableId].DisplayName));
                    }

                    selectedReservationTables = stringBuilder.ToString();
                }

                return selectedReservationTables;
            }
        }

        public DelegateCommand EditCommand { get; private set; }
        public DelegateCommand DeleteCommand { get; private set; }

    }
}
