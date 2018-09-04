
using System;
using System.Globalization;
using System.Drawing;

// V4.2
namespace MV
{
    ///<summary>2 Dim Vektor und Operationen (Radius, Phi, Add..) für
    ///2 Dim Vektor</summary>
    public struct Vect2D
    {
        public static Vect2D ini;
        #region Member Variablen
        double m_X, m_Y;    // genaue Koordinaten
        static Point m_P; // Hilfsvariable
        const double GRAD_RAD = Math.PI / 180.0;
        const double RAD_GRAD = 180.0 / Math.PI;
        #endregion

        public static Vect2D Create(double aX, double aY, bool aPolar)
        {
            Vect2D v = Vect2D.ini;
            if (!aPolar)
            {
                v.m_X = aX; v.m_Y = aY;
            }
            else
            {
                // v.m_X = aX * Math.Cos(GRAD_RAD * aY);
                // v.m_Y = aX * Math.Sin(GRAD_RAD * aY);
                v.SetFrom_R_Phi(aX, aY);
            }
            return v;
        }

        // Vector from aP1 to aP2
        public static Vect2D Create(Vect2D aP1, Vect2D aP2)
        {
            Vect2D v1;
            v1.m_X = aP2.X - aP1.X;
            v1.m_Y = aP2.Y - aP1.Y;
            return v1;
        }

        // Vector from aP1 to aP2
        public static Vect2D Create(Point aP1, Point aP2)
        {
            Vect2D v1;
            v1.m_X = aP2.X - aP1.X;
            v1.m_Y = aP2.Y - aP1.Y;
            return v1;
        }

        // aPolar = true/false
        public Vect2D(double aX, double aY, bool aPolar)
        {
            if (!aPolar)
            {
                m_X = aX; m_Y = aY;
            }
            else
            {
                m_X = aX * Math.Cos(GRAD_RAD * aY);
                m_Y = aX * Math.Sin(GRAD_RAD * aY);
            }
        }

        ///<summary>X Koordinate in double</summary>
        public double X
        {
            get { return m_X; }
            set { m_X = value; }
        }

        ///<summary>Y Koordinate in double</summary>
        public double Y
        {
            get { return m_Y; }
            set { m_Y = value; }
        }

        ///<summary>X Koordinate in int</summary>
        public int XI
        {
            get { return (int)m_X; }
        }
        ///<summary>Y Koordinate in int</summary>
        public int YI
        {
            get { return (int)m_Y; }
        }

        public float XF
        {
            get { return (float)m_X; }
        }

        public float YF
        {
            get { return (float)m_Y; }
        }

        public Point AsPoint
        {
            get
            {
                // Point pt;
                // if (m_P == null) m_P = new Point();
                m_P.X = (int)m_X; m_P.Y = (int)m_Y; return m_P;
                // pt.X = (int)m_X; pt.Y = (int)m_X; return pt;
            }
            set
            {
                m_X = value.X; m_Y = value.Y;
            }
        }

        public override string ToString()
        {
            // return String.Format("{0}:{1}", XI, YI);
            // return String.Format("{0};{1}", m_X, m_Y);
            // return String.Format("{0:F1}:{1:F1}", X, Y);
            return String.Format("{0:F1}:{1:F1}", GetR(), GetPhiGrad());
        }

        public void PrintRPhi()
        {
            Console.WriteLine("{0:F1}:{1:F1}", GetR(), GetPhiGrad());
        }

        public void PrintXY()
        {
            Console.WriteLine("{0:F1}:{1:F1}", X, Y);
        }

        public void SetP1P2(double aX1, double aY1, double aX2, double aY2)
        {
            m_X = aX2 - aX1;
            m_Y = aY2 - aY1;
        }

        public void SetXY(double aX, double aY)
        { m_X = aX; m_Y = aY; }

        public void SetXY(int aX, int aY)
        { m_X = aX; m_Y = aY; }

        ///<summary>Über Polarkoordinate R, Phi setzen</summary>
        public void SetFrom_R_Phi(double aR, double aPhi)
        {
            m_X = aR * Math.Cos(GRAD_RAD * aPhi);
            m_Y = aR * Math.Sin(GRAD_RAD * aPhi);
        }

        public double GetPhi()
        {
            return Math.Atan2(m_Y, m_X);
            /* if( m_X==0.0 && m_Y==0.0 )
                      return 0.0;

                  if( m_X==0.0 )
                  {
                      if( m_Y>0.0 )
                          return Math.PI/2;
                      else
                          return -Math.PI/2;
                  }

                  if( m_Y==0.0 )
                  {
                      if( m_X>0.0 )
                          return 0;
                      else
                          return -Math.PI;
                  }

                  double phi = Math.Abs(Math.Atan(m_Y/m_X));

                  if( m_X>0.0 )
                  {
                      if( m_Y>0 )
                          return Math.Abs(phi);
                      else // m_Y<0.0
                          return -Math.Abs(phi);
                  }
                  else // m_X<0.0
                  {
                      if( m_Y>0 )
                          return Math.PI - Math.Abs(phi);
                      else // m_Y<0.0
                          return -(Math.PI - Math.Abs(phi));
                  } */
        }

        public double GetPhiGrad() { return RAD_GRAD * GetPhi(); }

        public double DiffAngle(Vect2D aB)
        {
            double ang = this.GetPhiGrad() - aB.GetPhiGrad();
            double ret;
            ret = ang;
            if (ang > 180)
                ret = -360 + ang;
            if (ang < -180)
                ret = 360 + ang;
            return ret;
        }

        public double GetR()
        {
            return Math.Sqrt(m_X * m_X + m_Y * m_Y);
        }

        // Vector verlängern Richtung bleibt gleich
        /* public Vect2D Add_R(double aR)
        {
          Vect2D v1, v2;
          v1 = this.GetNormalizedVersion();
          v2 = v1.ScalarMult(aR);
          return this.Add(v2);
        } */

        // Länge setzen Richtung bleibt gleich
        public void Set_R(double aR)
        {
            double r = VectLength();
            if (r == 0.0)
                return;
            m_X = (m_X / r) * aR;
            m_Y = (m_Y / r) * aR;
        }

        public void Add_Phi(double aPhi)
        {
            Vect2D v1;
            v1.m_X = Math.Cos(aPhi);
            v1.m_Y = Math.Sin(aPhi);
            this.CoMultTo(v1);
        }

        public void Add_PhiGrad(double aPhi)
        {
            this.Add_Phi(GRAD_RAD * aPhi);
        }

        public void Assign(Vect2D aVect)
        {
            m_X = aVect.m_X; m_Y = aVect.m_Y;
        }

        // this = this + aVect
        public void AddTo(Vect2D aVect)
        {
            m_X = m_X + aVect.m_X;
            m_Y = m_Y + aVect.m_Y;
        }

        public Vect2D Add(Vect2D aVect)
        {
            Vect2D sum;
            sum.m_X = m_X + aVect.m_X;
            sum.m_Y = m_Y + aVect.m_Y;
            return sum;
        }

        public static Vect2D operator +(Vect2D aA, Vect2D aB)
        {
            Vect2D sum;
            sum.m_X = aA.m_X + aB.m_X;
            sum.m_Y = aA.m_Y + aB.m_Y;
            return sum;
        }

        // aA - aB
        public static Vect2D operator -(Vect2D aA, Vect2D aB)
        {
            Vect2D sum;
            sum.m_X = aA.m_X - aB.m_X;
            sum.m_Y = aA.m_Y - aB.m_Y;
            return sum;
        }

        public static Vect2D operator *(Vect2D aVect, double aFactor)
        {
            Vect2D v1;
            v1.m_X = aVect.m_X * aFactor;
            v1.m_Y = aVect.m_Y * aFactor;
            return v1;
        }

        // this = this + aVect*aFactor
        public void AddTo(Vect2D aVect, double aFactor)
        {
            m_X = m_X + aVect.m_X * aFactor;
            m_Y = m_Y + aVect.m_Y * aFactor;
        }

        public void SubFrom(Vect2D aVect)
        {
            m_X = m_X - aVect.m_X;
            m_Y = m_Y - aVect.m_Y;
        }

        // this = this * aB
        public void CoMultTo(Vect2D aB)
        {
            double Xres, Yres;
            Xres = m_X * aB.m_X - m_Y * aB.m_Y;
            Yres = m_X * aB.m_Y + m_Y * aB.m_X;
            m_X = Xres; m_Y = Yres;
        }

        // Complex Mult = rot this by aB
        public Vect2D CoMult(Vect2D aB)
        {
            Vect2D v1;
            double Xres, Yres;
            Xres = m_X * aB.m_X - m_Y * aB.m_Y;
            Yres = m_X * aB.m_Y + m_Y * aB.m_X;
            v1.m_X = Xres; v1.m_Y = Yres;
            return v1;
        }

        public Vect2D GetComplexConjugate()
        {
            Vect2D v1;
            v1.m_X = m_X; v1.m_Y = -m_Y;
            return v1;
        }

        public Vect2D ScalarMult(double aFactor)
        {
            Vect2D ret;
            ret.m_X = m_X * aFactor;
            ret.m_Y = m_Y * aFactor;
            return ret;
        }

        public double ScalarProd(Vect2D aB)
        {
            return m_X * aB.m_X + m_Y * aB.m_Y;
        }

        public Vect2D GetNormalVector()
        {
            Vect2D ret;
            ret.m_X = -m_Y;
            ret.m_Y = m_X;
            return ret;
        }

        // this vector normalized to Length 1
        public Vect2D GetNormalizedVersion()
        {
            Vect2D ret;
            double r = VectLength();
            ret.m_X = m_X / r;
            ret.m_Y = m_Y / r;
            return ret;
        }

        // this vector with Length aLenght
        public Vect2D GetScaledVersion(double aLenght)
        {
            Vect2D ret;
            ret = GetNormalizedVersion();
            ret = ret.ScalarMult(aLenght);
            return ret;
        }

        public Vect2D GetOppositeDirection()
        {
            Vect2D ret;
            ret.m_X = -m_X;
            ret.m_Y = -m_Y;
            return ret;
        }

        ///<summary>Vektorlänge sqrt(x*x + y*y)</summary>
        public double VectLength()
        { return Math.Sqrt(m_X * m_X + m_Y * m_Y); }

        // Distance between this and aB
        public double DistBetweenPoints(Vect2D aB)
        {
            double xd = m_X - aB.m_X;
            double yd = m_Y - aB.m_Y;
            return Math.Sqrt(xd * xd + yd * yd);
        }

        public bool IsZero()
        {
            return m_X == 0.0 && m_Y == 0.0;
        }
    }
}


















