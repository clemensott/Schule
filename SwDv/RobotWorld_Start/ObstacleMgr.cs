using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using MV;

namespace RobotWorld
{

  class Obstacle
  {
    public Vect2D pos;

    public Obstacle(Point aP)
    {
      pos.AsPoint = aP;
    }

    public void Paint(Graphics gr)
    {
      gr.FillEllipse(Brushes.DarkGreen, pos.XI-10, pos.YI-10, 20, 20);
    }
  }
  
  
  class Omgr
  {
    static List<Obstacle> _list = new List<Obstacle>();

    public static void NewObstacle(Point aMp)
    {
      _list.Add(new Obstacle(aMp));
    }

    public static Obstacle At(int i)
    {
      if (_list.Count == 0)
          return null;
      return _list[i];
    }

    public static int Count
    {
      get { return _list.Count; }
    }

    public static void Paint(Graphics gr)
    {
      foreach (Obstacle obs in _list)
        obs.Paint(gr);
    }
  }
}
