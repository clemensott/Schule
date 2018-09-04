
using System;
// using System.Text;
using System.Drawing;

namespace MV
{
  class VC
  {
    public const double RAD_GRAD = 180.0 / Math.PI;
    public const double GRAD_RAD = Math.PI / 180.0;
    
    public static int Distance(Point aP1, Point aP2)
    {
      double dx = aP1.X - aP2.X;
      double dy = aP1.Y - aP2.Y;
      return (int)Math.Sqrt(dx*dx + dy*dy);
    }

    public static float Distance2(Point aP1, Point aP2)
    {
      double dx = aP1.X - aP2.X;
      double dy = aP1.Y - aP2.Y;
      return (float)Math.Sqrt(dx * dx + dy * dy);
    }

    public static double Angle(Point aP1, Point aP2)
    {
      double dx = aP2.X - aP1.X;
      double dy = aP2.Y - aP1.Y;
      return (RAD_GRAD * Math.Atan2(dy,dx));
    }

    public static double Angle(Point aV)
    {
      return (RAD_GRAD * Math.Atan2(aV.Y, aV.X));
    }

    public static double DiffAngle(Point aP1, Point aP2)
    {
      double ang = VC.Angle(aP1) - VC.Angle(aP2);
      double ret;
      ret = ang;
      if (ang > 180)
        ret = -360 + ang;
      if (ang < -180)
        ret = 360 + ang;
      return ret;
    }

    public static int ScalarProd(Point aP1, Point aP2, Point aP3, Point aP4)
    {
      Point v1 = Vector(aP1, aP2);
      Point v2 = Vector(aP3, aP4);
      return v1.X * v2.X + v1.Y * v2.Y;
    }

    public static Point Vector(Point aP1, Point aP2)
    {
      Point v = Point.Empty;
      v.X = aP2.X - aP1.X;
      v.Y = aP2.Y - aP1.Y;
      return v;
    }

    public static PointF UnitVector(Point aP1, Point aP2)
    {
      PointF v = Point.Empty;
      v.X = aP2.X - aP1.X;
      v.Y = aP2.Y - aP1.Y;
      float r = (float)Distance2(aP1, aP2);
      v.X = v.X / r;
      v.Y = v.Y / r;
      return v;
    }

    public static PointF VectWithLen(Point aP1, Point aP2, float aLen)
    {
      PointF v = UnitVector(aP1, aP2);
      v.X = v.X * aLen; v.Y = v.Y * aLen;
      return v;
    }

    public static Point VectWithLen(Point aV, float aLen)
    {
      Point ret = Point.Empty;
      double r = Math.Sqrt(aV.X * aV.X + aV.Y * aV.Y);
      ret.X = (int)(((double)aV.X / r) * aLen);
      ret.Y = (int)(((double)aV.Y / r) * aLen);
      return ret;
    }

    public static Point MidPoint(Point aP1, Point aP2)
    {
      PointF v = VectWithLen(aP1, aP2, Distance2(aP1, aP2)/2.0f);
      Point ret = Point.Empty;
      ret.X = aP1.X + (int)v.X;
      ret.Y = aP1.Y + (int)v.Y;
      return ret;
    }

    public static Point Add(Point aP1, Point aP2)
    {
      Point res = Point.Empty;
      res.X = aP1.X + aP2.X;
      res.Y = aP1.Y + aP2.Y;
      return res;
    }

    public static Point Sub(Point aP1, Point aP2)
    {
      Point res = Point.Empty;
      res.X = aP1.X - aP2.X;
      res.Y = aP1.Y - aP2.Y;
      return res;
    }

    public static Point NormalVect(Point aP1, Point aP2)
    {
      Point v = Vector(aP1, aP2);
      int tmp = v.X;
      v.X = -v.Y;
      v.Y = tmp;
      return v;
    }

    public static int ScalarProd(Point aP1, Point aP2)
    {
      return aP1.X * aP2.X + aP1.Y * aP2.Y;
    }

    // returns Index of lowest Number
    public static int LowestNum(int aN1, int aN2, int aN3)
    {
      if ((aN1 < aN2) && (aN2 < aN3))
        return 1;
      if ((aN2 < aN1) && (aN1 < aN3))
        return 2;
      return 3;
    }
  }
}
