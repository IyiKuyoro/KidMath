using System;
using System.Globalization;
using System.Windows.Data;

namespace KidMath
{
    class TimeValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int time = (int)value;
            return string.Format(time + " Seconds");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string time = (string)value;
            return System.Convert.ToInt32(time.Substring(0, 2));
        }
    }
}