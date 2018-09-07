using System.Collections.Generic;
using System.Windows.Media;

namespace LabrinthDraw
{
    class Line : List<Position>
    {
        public Brush Brush { get; set; }

        public Line(Brush brush)
        {
            Brush = brush;
        }

        public override string ToString()
        {
            return Brush.ToString();
        }
    }
}