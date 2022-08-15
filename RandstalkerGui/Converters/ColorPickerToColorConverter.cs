using System;
using System.Windows.Data;
using System.Windows.Media;

namespace RandstalkerGui.Converters
{
    class ColorPickerToColorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (Color)ColorConverter.ConvertFromString($"#" +
                $"{values[0]}{values[0]}" +
                $"{values[1]}{values[1]}" +
                $"{values[2]}{values[2]}");
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
