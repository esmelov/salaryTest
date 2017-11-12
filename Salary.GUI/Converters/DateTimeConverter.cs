using Salary.Core.Helper;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Salary.GUI.Converters
{
    [ValueConversion(typeof(int), typeof(DateTime))]
    public class DateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            DateTime dateTimeType = ((int)value).ToDateTime();
            return dateTimeType;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime dt && dt == DateTime.MinValue)
                return null;
            int unixTimeType = ((DateTime)value).ToUnixTimeStamp();
            return unixTimeType;
        }
    }
}
