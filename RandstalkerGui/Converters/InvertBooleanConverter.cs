using System;
using System.Windows.Data;

namespace RandstalkerGui.Converters
{
    class InvertBooleanConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if(!(value is bool))
				throw new ArgumentException("Value passed is not a bool");

			return !(bool)value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
