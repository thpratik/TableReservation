using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TableReservation.ApplicationServices.Controls;
using TableReservation.ApplicationServices.MessageBox;

namespace TableReservation.ApplicationServices
{
    public class MessageBoxService : IMessageBoxService
    {
        public static readonly MessageBoxService Instance = new MessageBoxService();

        private MessageBoxService()
        {

        }

        public MessageBoxResults ShowMessageBox(string messageText)
        {
            var messageBoxWindow = new MessageBoxWindow();
            messageBoxWindow.MessageTitle = string.Empty;
            messageBoxWindow.MessageText = messageText;
            messageBoxWindow.MessageButtons = MessageBoxButtons.OK;
            messageBoxWindow.MessageIcon = MessageBoxIcon.None;
            messageBoxWindow.ShowDialog();

            return messageBoxWindow.Result;
        }

        public MessageBoxResults ShowMessageBox(string messageText, string title)
        {
            var messageBoxWindow = new MessageBoxWindow();
            messageBoxWindow.MessageTitle = title;
            messageBoxWindow.MessageText = messageText;
            messageBoxWindow.MessageButtons = MessageBoxButtons.OK;
            messageBoxWindow.MessageIcon = MessageBoxIcon.None;
            messageBoxWindow.ShowDialog();

            return messageBoxWindow.Result;
        }

        public MessageBoxResults ShowMessageBox(string messageText, string title, MessageBoxButtons messageBoxButtons)
        {
            var messageBoxWindow = new MessageBoxWindow();
            messageBoxWindow.MessageTitle = title;
            messageBoxWindow.MessageText = messageText;
            messageBoxWindow.MessageButtons = messageBoxButtons;
            messageBoxWindow.MessageIcon = MessageBoxIcon.None;
            messageBoxWindow.ShowDialog();

            return messageBoxWindow.Result;
        }

        public MessageBoxResults ShowMessageBox(string messageText, string title, MessageBoxButtons messageBoxButtons, MessageBoxIcon messageBoxIcon)
        {
            var messageBoxWindow = new MessageBoxWindow();
            messageBoxWindow.MessageTitle = title;
            messageBoxWindow.MessageText = messageText;
            messageBoxWindow.MessageButtons = messageBoxButtons;
            messageBoxWindow.MessageIcon = messageBoxIcon;
            messageBoxWindow.ShowDialog();

            return messageBoxWindow.Result;
        }
    }
}
