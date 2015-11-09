using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TableReservation.ApplicationServices.Controls;
using TableReservation.ApplicationServices.DialogBox;
using TableReservation.Common;
using TableReservation.Common.View;

namespace TableReservation.ApplicationServices
{
    public class DialogBoxService : IDialogBoxService
    {
        private DialogBoxWindow _currentWindow;
        IUnityContainer _container;
        public DialogBoxService(IUnityContainer container)
        {
            this._container = container;
        }

        public DialogBoxResult ShowUserDialog(string title, ViewNames viewName)
        {
            var dialogBoxWindow = new DialogBoxWindow();
            dialogBoxWindow.Title = title;
            switch (viewName)
            {
                case ViewNames.ReservationView:
                    break;
                case ViewNames.ReservationDetailsView:
                    var reservationDetailsView = _container.Resolve<IReservationDetailsView>();
                    if (reservationDetailsView != null)
                    {
                        dialogBoxWindow.Content = reservationDetailsView;
                    }
                    break;
            }

            this._currentWindow = dialogBoxWindow;
            dialogBoxWindow.ShowDialog();
            return DialogBoxResult.OK;
        }

        public DialogBoxResult ShowUserDialog(string title, ViewNames viewName, DialogBoxButtons dialogBoxButtons)
        {
            throw new NotImplementedException();
        }

        public DialogBoxResult ShowOpenXMLFileDialog(out string xmlFileName)
        {
            // Configure open file dialog box
            var openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.FileName = "Tables"; // Default file name
            openFileDialog.DefaultExt = ".xml"; // Default file extension
            openFileDialog.Filter = "XML documents (.xml)|*.xml"; // Filter files by extension

            // Show open file dialog box
            Nullable<bool> result = openFileDialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                xmlFileName = openFileDialog.FileName;
                return DialogBoxResult.OK;
            }
            else
            {
                xmlFileName = string.Empty;
                return DialogBoxResult.Cancel;
            }
        }

        public void CloseUserDialog()
        {
            if (this._currentWindow != null)
            {
                this._currentWindow.Close();
            }
        }
    }
}
