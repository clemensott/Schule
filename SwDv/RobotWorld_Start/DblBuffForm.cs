
using System;
// using System.Collections.Generic;
// using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using MV;

namespace RobotWorld
{
  public class DblBuffForm : Form
  {
    public static DblBuffForm frm;

    public DblBuffForm()
      : base()
    {
      this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint |
      ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw, true);
      frm = this;
    }

    public static int XMax()
    {
      return frm.Size.Width-10;
    }

    public static int XMin()
    {
      return 0;
    }

    public static int YMax()
    {
      return frm.Size.Height-26;
    }

    public static int YMin()
    {
      return 26;
    }
    
    // Bring Positions outside of ScPanel back into ScPanel
    // Pos > Xmax => Xmin; Pos < Xmin = > Xmax ......
    public void CorrigatePosition(ref Vect2D aPos)
    {
      if (aPos.X < -20)
        aPos.X = Width;
      if (aPos.X > Width+20)
        aPos.X = 0;
      if (aPos.Y < -5)
        aPos.Y = Height;
      if (aPos.Y > Height+20)
        aPos.Y = 0;
    }
  }
}
