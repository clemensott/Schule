using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace DBalls1
{
    class GrObjMgr
    {
        ArrayList _grObjList = new ArrayList();
        GraphicObject _dragObj; // Graphicobjekt welches momentan gezogen wird

        // return true if an Object was deleted at aMP
        // Nur eines löschen

        public GrObjMgr()
        {

        }

        public 

        public bool DeleteObjectAt(Point aMP)
        {
            foreach (GraphicObject obj in _grObjList)
            {
                if (obj.HitInRadius(aMP))
                {
                    _grObjList.Remove(obj);
                    return true;
                }
            }
            return false;
        }

        public GraphicObject FindObjectAt(Point aMP)
        {
            foreach (GraphicObject obj in _grObjList)
            {
                if (obj.HitInRadius(aMP))
                {
                    return obj;
                }
            }
            return null;
        }
    }
}
