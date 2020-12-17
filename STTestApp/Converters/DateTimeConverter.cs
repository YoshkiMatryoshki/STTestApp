using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace STTestApp
{
    /// <summary>
    /// Конвертирует формат даты в подходящий для отображения в основном окне
    /// </summary>
    class DateTimeConverter : IValueConverter
    {
        public static DateTimeConverter Instance = new DateTimeConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                DateTime systemDT = (DateTime)value;
                return $"{systemDT.Day}.{systemDT.Month}.{systemDT.Year}";

            }
            catch
            {
                return "Error";
            }


        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
