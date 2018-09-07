using System;
using System.Globalization;
using System.Windows.Data;

namespace LabyrinthSim.Converter
{
    class BlockConverter : IValueConverter
    {
        private Block currentValue;
        private IntConverter xCon = new IntConverter(), yCon = new IntConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            currentValue = (Block)value;

            if (parameter.ToString() == "X") return xCon.Convert(currentValue.X, targetType, null, culture);
            else return yCon.Convert(currentValue.Y, targetType, null, culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter.ToString() == "X")
            {
                int x = (int)xCon.ConvertBack(value, typeof(int), null, culture);

                return currentValue = new Block(x, currentValue.Y);
            }

            int y = (int)yCon.ConvertBack(value, typeof(int), null, culture);

            return currentValue = new Block(currentValue.X, y);
        }
    }
}
