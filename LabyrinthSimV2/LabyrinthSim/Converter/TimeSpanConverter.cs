using System;
using System.Globalization;
using System.Windows.Data;

namespace LabyrinthSim.Converter
{
    class TimeSpanConverter : IValueConverter
    {
        private TimeSpan value;
        private string text;

        public TimeSpanConverter()
        {
            value = TimeSpan.Zero;
            text = value.TotalMilliseconds.ToString();
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TimeSpan newValue = (TimeSpan)value;

            if (this.value == newValue) return text;

            this.value = newValue;

            return text = newValue.TotalMilliseconds.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double newMilliseconds;
            text = value.ToString();

            string parseText = text.Length > 0 ? text : "0";

            if (double.TryParse(parseText, out newMilliseconds)) return this.value = TimeSpan.FromMilliseconds(newMilliseconds);

            return this.value;
        }
    }
}
