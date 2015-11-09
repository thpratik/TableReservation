using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TableReservation.Common;
using TableReservation.Common.Base.ViewModel;

namespace TableReservation.ApplicationServices.DialogBox
{
    public enum DialogBoxResult
    {
        None,
        OK,
        Cancel,
        Yes,
        No,
    }

    public enum DialogBoxButtons
    {
        None,
        Cancel
    }

    public interface IDialogBoxService
    {
        DialogBoxResult ShowUserDialog(string title, ViewNames viewName);
        DialogBoxResult ShowUserDialog(string title, ViewNames viewName, DialogBoxButtons dialogBoxButtons);

        DialogBoxResult ShowOpenXMLFileDialog(out string xmlFileName);

        void CloseUserDialog();
    }
}
