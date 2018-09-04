using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ObjectOrientedDrawingOfObjects.Model
{
    class Parser
    {
        public static Geometry GetGeometryFromObjectType(ObjectTypes objectType, double a)
        {
            switch (objectType)
            {
                case ObjectTypes.Dreieck:
                    return Geometries.GetTriangleGeometry(a);
                case ObjectTypes.Kreis:
                    return new EllipseGeometry(new Rect() { Width = a, Height = a });
                case ObjectTypes.Rechteck:
                    return new RectangleGeometry(new Rect() { Width = a, Height = a });
                default:
                    return null;
            }
        }
    }
}
