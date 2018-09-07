using System;
using System.Globalization;
using System.Windows.Data;

namespace LabyrinthSim.Converter
{
    abstract class EnumConverter<T> : IValueConverter where T : struct, IComparable
    {
        private T currentValue;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            currentValue = (T)value;

            return currentValue.CompareTo(GetValue(parameter.ToString())) == 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value) return currentValue = GetValue(parameter.ToString());

            return currentValue;
        }

        protected virtual T GetValue(string name)
        {
            return (T)Enum.Parse(typeof(T), name);
        }
    }

    class SearchTypeConverter : EnumConverter<SearchType>
    {
    }

    class RelationInterpreterTypeConverter : EnumConverter<RelationInterpreterType>
    {
    }
}
