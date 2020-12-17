using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace STTestApp
{
    /// <summary>
    /// Конвертирует double (0.05) в более читабельный вид 5%
    /// </summary>
    class DoubleToPercentStringConverter : IValueConverter
    {
        public static DoubleToPercentStringConverter Instance = new DoubleToPercentStringConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "———";
            return $"{(double)value * 100}%";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
