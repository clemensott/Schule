using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ObjectOrientedDrawingOfObjects.Model
{
    /// <summary>
    /// Class for extra geometries
    /// </summary>
    class Geometries
    {
        public static Geometry GetTriangleGeometry(double a)
        {
            Point p1 = new Point(0.0d, a);
            Point p2 = new Point(a, a);
            Point p3 = new Point(a / 2, 0);

            List<PathSegment> segments = new List<PathSegment>(3)
                {
                    new LineSegment(p1, true),
                    new LineSegment(p2, true),
                    new LineSegment(p3, true)
                };

            List<PathFigure> figures = new List<PathFigure>(1);
            PathFigure pf = new PathFigure(p1, segments, true);
            figures.Add(pf);

            return new PathGeometry(figures, FillRule.EvenOdd, null);
        }
    }
}
