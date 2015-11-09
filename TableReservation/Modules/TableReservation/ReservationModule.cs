using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using TableReservation.ApplicationServices;
using TableReservation.ApplicationServices.DialogBox;
using TableReservation.ApplicationServices.MessageBox;
using TableReservation.BusinessServices;
using TableReservation.Common.BusinessServices;
using TableReservation.Common.DataServices;
using TableReservation.Common.Models;
using TableReservation.Common.View;
using TableReservation.Common.ViewModel;
using TableReservation.View;
using TableReservation.ViewModel;

namespace TableReservation
{
    public class ReservationModule : IModule
    {
        private IUnityContainer _container;
        private IRegionManager _regionManager;
        private IEventAggregator _eventAggregator;
        private ILoggerFacade _logger;

        public ReservationModule(IUnityContainer container, IRegionManager regionManager, IEventAggregator eventAggregator, ILoggerFacade logger)
        {
            this._container = container;
            this._regionManager = regionManager;
            this._eventAggregator = eventAggregator;
            this._logger = logger;
        }

        public void Initialize()
        {
            // Type mapping
            this._container.RegisterType<IShellView, ShellView>();
            this._container.RegisterType<IShellViewModel, ShellViewModel>();
            this._container.RegisterType<IReservationView, ReservationView>();
            this._container.RegisterType<IReservationViewModel, ReservationViewModel>();
            this._container.RegisterType<IReservationDetailsView, ReservationDetailsView>();
            this._container.RegisterType<IReservationDetailsViewModel, ReservationDetailsViewModel>();
            this._container.RegisterType<IReservationDashBoardView, ReservationDashBoardView>();
            this._container.RegisterType<IReservationDashBoardViewModel, ReservationDashBoardViewModel>();

            // Depency injection
            this._container.RegisterInstance<IMessageBoxService>(MessageBoxService.Instance);

            var dialogBoxService = this._container.Resolve<DialogBoxService>();
            this._container.RegisterInstance<IDialogBoxService>(dialogBoxService);

            var tableDataService = new TableReservation.DataServices.XMLDataService(this._logger, "Tables");
            this._container.RegisterInstance<ITableDataService>(tableDataService);

            var reservationDataService = new TableReservation.DataServices.XMLDataService(this._logger, "Reservations");
            this._container.RegisterInstance<IReservationDataService>(reservationDataService);

            var reservationManager = this._container.Resolve<ReservationManager>();
            this._container.RegisterInstance<IReservationManager>(reservationManager);

            var tableManager = this._container.Resolve<TableManager>();
            this._container.RegisterInstance<ITableManager>(tableManager);

            this._container.RegisterInstance<Reservation>("SelectedReservation", new Reservation());
            
            // Placing views
            this._regionManager.RegisterViewWithRegion(RegionNames.ShellViewRegion, typeof(IShellView));
            this._regionManager.RegisterViewWithRegion(RegionNames.ReservationDetailsRegion, typeof(IReservationView));
            this._regionManager.RegisterViewWithRegion(RegionNames.ReservationDashboardRegion, typeof(IReservationDashBoardView));
        }
    }
}
