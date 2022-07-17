using System;
using System.Windows.Data;

namespace RandstalkerGui.Converters
{
    class CurrentCultureSelectedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if(!(value is string))
            {
                throw new ArgumentException("Value passed is not a string");
            }

            if (!(parameter is string))
            {
                throw new ArgumentException("Parameter passed is not a string");
            }

            string currentCultureName = value.ToString();
            string cultureToCheckName = parameter.ToString();

            return currentCultureName == cultureToCheckName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
