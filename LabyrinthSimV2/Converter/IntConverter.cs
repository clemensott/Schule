using System;
using System.Globalization;
using System.Windows.Data;

namespace LabyrinthSim.Converter
{
    class IntConverter : IValueConverter
    {
        private string text;

        public int CurrentValue { get; set; }

        public IntConverter()
        {
            CurrentValue = 0;
            text = CurrentValue.ToString();
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int newValue = (int)value;

            if (this.CurrentValue == newValue) return text;

            this.CurrentValue = newValue;

            return text = newValue.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int newValue;
            text = value.ToString();

            if (int.TryParse(text, out newValue)) return this.CurrentValue = newValue;

            return this.CurrentValue;
        }
    }
}
