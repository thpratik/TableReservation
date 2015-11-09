using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TableReservation.ApplicationServices.DialogBox;
using TableReservation.ApplicationServices.MessageBox;

namespace TableReservation.Utilities
{
    internal class MinMaxToTimeConverter : IValueConverter
    {
        public static readonly MinMaxToTimeConverter Instance = new MinMaxToTimeConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !(value is short))
                return 11;

            return (short)((short)value) + 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    internal class ReservationBackgroundConverter : IValueConverter
    {
        public static readonly ReservationBackgroundConverter Instance = new ReservationBackgroundConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && (value is bool) && (bool)value)
                return new SolidColorBrush(Colors.Red);

            return new SolidColorBrush(Colors.White);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    internal class RowAndColumnMultiValueConverter : IMultiValueConverter
    {
        public static readonly RowAndColumnMultiValueConverter Instance = new RowAndColumnMultiValueConverter();
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //SupplierCostData dummyData = new SupplierCostData();

            //double? val = dummyData.GetCost((int)RowValue, (int)ColumnValue);

            return "";//string.Format("{0}", val.Value);   
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
