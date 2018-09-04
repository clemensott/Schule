using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectOrientedDrawingOfObjects.Model
{
    /// <summary>
    /// Emum for all shapes. 
    /// To add new Type simply add Type to enum and add Geometry to Geometries and add parsing function to Parser. 
    /// Rest will automatically be set
    /// </summary>
    enum ObjectTypes
    {
        Dreieck, Kreis, Rechteck
    }
}
