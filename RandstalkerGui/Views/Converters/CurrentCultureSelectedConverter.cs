using System;
using System.Windows.Data;

namespace RandstalkerGui.Views.Converters
{
    internal class CurrentCultureSelectedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is not string currentCultureName)
            {
                throw new ArgumentException("Value passed is not a string");
            }

            if (parameter is not string cultureToCheckName)
            {
                throw new ArgumentException("Parameter passed is not a string");
            }

            return currentCultureName == cultureToCheckName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
