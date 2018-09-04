using System;
using System.Globalization;
using System.Windows.Data;

namespace LabyrinthSim
{
    class IntConverter : IValueConverter
    {
        private const int maxTextLength = 3;
        private int value;
        private string text;

        public IntConverter()
        {
            value = 0;
            text = string.Empty;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (this.value == (int)value) return text;

            this.value = (int)value;

            return text = Convert((int)value);
        }

        public static string Convert(int value)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int newValue;
            text = value.ToString();

            if (int.TryParse(text, out newValue)) return this.value = newValue;

            return this.value;
        }
    }
}
