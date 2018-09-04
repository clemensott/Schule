using System;
using System.Globalization;
using System.Windows.Data;

namespace LabyrinthSim.Converter
{
    class PrefixConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (parameter?.ToString() ?? string.Empty) + " " + (value?.ToString() ?? string.Empty);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
