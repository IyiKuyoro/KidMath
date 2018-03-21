using System;
using System.Globalization;
using System.Windows.Data;

namespace KidMath
{
    [Serializable]
    class NumberOfQuestionsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int num = (int)value;
            return string.Format(num + " Questions");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {
                string num = (string)value;
                if (num.Length == 11)
                    return System.Convert.ToInt32(num.Substring(0, 1));
                else
                    return System.Convert.ToInt32(num.Substring(0, 2));
            }
            else
            {
                return 10;
            }
        }
    }
}
