using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using TableReservation.ApplicationServices.DialogBox;
using TableReservation.ApplicationServices.MessageBox;

namespace TableReservation.ApplicationServices.Utilities
{
    internal class UrlToImageSourceConverter : IValueConverter
    {
        /// <summary>
        /// The instance to use
        /// </summary>
        public static readonly UrlToImageSourceConverter Instance = new UrlToImageSourceConverter();

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return null;

            return new BitmapImage(new Uri(value.ToString()));
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    internal class MessageButtonsConverter : IValueConverter
    {
        public static readonly MessageButtonsConverter Instance = new MessageButtonsConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return null;

            var buttonVisibility = Visibility.Collapsed;
            if (value is MessageBoxButtons)
            {
                var messageBoxButtons = (MessageBoxButtons)value;
                switch (messageBoxButtons)
                {
                    case MessageBoxButtons.OK:
                        if (parameter.ToString().Equals("OK", StringComparison.CurrentCultureIgnoreCase))
                        {
                            buttonVisibility = Visibility.Visible;
                        }
                        break;
                    case MessageBoxButtons.OKCancel:
                        if (parameter.ToString().Equals("OK", StringComparison.CurrentCultureIgnoreCase) ||
                            parameter.ToString().Equals("Cancel", StringComparison.CurrentCultureIgnoreCase))
                        {
                            buttonVisibility = Visibility.Visible;
                        }
                        break;
                    case MessageBoxButtons.YesNoCancel:
                        if (parameter.ToString().Equals("Yes", StringComparison.CurrentCultureIgnoreCase) ||
                            parameter.ToString().Equals("No", StringComparison.CurrentCultureIgnoreCase) ||
                            parameter.ToString().Equals("Cancel", StringComparison.CurrentCultureIgnoreCase))
                        {
                            buttonVisibility = Visibility.Visible;
                        }
                        break;
                    case MessageBoxButtons.YesNo:
                        if (parameter.ToString().Equals("Yes", StringComparison.CurrentCultureIgnoreCase) ||
                            parameter.ToString().Equals("No", StringComparison.CurrentCultureIgnoreCase))
                        {
                            buttonVisibility = Visibility.Visible;
                        }
                        break;
                }
            }
            else if (value is DialogBoxButtons)
            {
                var dialogBoxButtons = (DialogBoxButtons)value;
                switch (dialogBoxButtons)
                {
                    case DialogBoxButtons.None:
                        break;
                    case DialogBoxButtons.Cancel:
                        if (parameter.ToString().Equals("Cancel", StringComparison.CurrentCultureIgnoreCase))
                        {
                            buttonVisibility = Visibility.Visible;
                        }
                        break;
                }
            }

            return buttonVisibility;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    internal class MessageBoxIconConverter : IValueConverter
    {
        private const string _iconResourcePathPrefix = @"pack://application:,,,/IM.UI.XamlResources;component/Images/UIW/MessageBox/{0}";

        public static readonly MessageBoxIconConverter Instance = new MessageBoxIconConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var messageBoxIcon = (MessageBoxIcon)value;
            var path = string.Empty;

            switch (messageBoxIcon)
            {
                case MessageBoxIcon.None:
                    break;
                case MessageBoxIcon.Error:
                    path = string.Format(_iconResourcePathPrefix, "48x48_Error.png");
                    break;
                case MessageBoxIcon.Question:
                    path = string.Format(_iconResourcePathPrefix, "48x48_Question.png");
                    break;
                case MessageBoxIcon.Information:
                    path = string.Format(_iconResourcePathPrefix, "48x48_Information.png");
                    break;
                case MessageBoxIcon.Warning:
                    path = string.Format(_iconResourcePathPrefix, "48x48_Warning.png");
                    break;
            }

            if (!string.IsNullOrEmpty(path))
                return UrlToImageSourceConverter.Instance.Convert(path, null, null, null);
            else
                return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
