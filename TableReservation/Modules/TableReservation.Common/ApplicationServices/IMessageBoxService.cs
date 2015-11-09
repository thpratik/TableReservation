using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TableReservation.ApplicationServices.MessageBox
{
    public enum MessageBoxResults
    {
        OK,
        Cancel,
        Yes,
        No,
    }

    public enum MessageBoxButtons
    {
        OK,
        OKCancel,
        YesNoCancel,
        YesNo,
    }

    public enum MessageBoxIcon
    {
        None,
        Error,
        Question,
        Warning,
        Information
    }

    public interface IMessageBoxService
    {
        MessageBoxResults ShowMessageBox(string messageText);
        MessageBoxResults ShowMessageBox(string messageText, string title);
        MessageBoxResults ShowMessageBox(string messageText, string title, MessageBoxButtons messageBoxButtons);
        MessageBoxResults ShowMessageBox(string messageText, string title, MessageBoxButtons messageBoxButtons, MessageBoxIcon messageBoxIcon);
    }
}
