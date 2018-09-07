using System;
using System.Globalization;
using System.Windows.Data;

namespace LabyrinthSim.Converter
{
    class TargetConverter : IValueConverter
    {
        private ITarget currentValue;
        private IntConverter xCon = new IntConverter(), yCon = new IntConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) value = new SingleTarget(Block.Origin);

            currentValue = (ITarget)value;

            switch (parameter.ToString())
            {
                case "Single":
                    return value is SingleTarget;

                case "Quad":
                    return value is SquareTarget;

                case "X":
                    return xCon.Convert(currentValue.Main.X, typeof(string), null, culture);

                default:
                    return yCon.Convert(currentValue.Main.Y, typeof(string), null, culture);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Block newMain;

            switch (parameter.ToString())
            {
                case "Single":
                    return (bool)value ? (ITarget)new SingleTarget(currentValue.Main) : new SquareTarget(currentValue.Main);

                case "Quad":
                    return (bool)value ? (ITarget)new SquareTarget(currentValue.Main) : new SingleTarget(currentValue.Main);

                case "X":
                    int newX = (int)xCon.ConvertBack(value, typeof(int), null, culture);
                    newMain = new Block(newX, currentValue.Main.Y);

                    return currentValue is SingleTarget ? (ITarget)new SingleTarget(newMain) : new SquareTarget(newMain);

                default:
                    int newY = (int)xCon.ConvertBack(value, typeof(int), null, culture);
                    newMain = new Block(currentValue.Main.X, newY);

                    return currentValue is SingleTarget ? (ITarget)new SingleTarget(newMain) : new SquareTarget(newMain);
            }
        }
    }
}
