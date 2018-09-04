using System;
using System.Drawing;
using System.Collections.Generic;
using MV;

namespace RobotWorld
{
    class BaseRobot
    {
        #region Member Variablen
        protected static Pen _speedPen = new Pen(Color.Green, 3);
        protected static Pen _carPen = new Pen(Color.Red, 2);
        #endregion
        // Properties
        public Vect2D Pos;
        public Vect2D V; // current speed
        double _speed; // Betrag und Vorzeichen des V-Vektors
        public Vect2D dPhi; // Rotational Speed
        public bool showPath = true;

        #region Gyroscope
        protected double _dPhiGrad;
        public double GyroAngle = 0;
        #endregion

        public BaseRobot()
        {
            Reset();
        }

        public void Reset()
        {
            Pos.SetXY(0, 0);
            V.SetXY(0.01, 0);
            dPhi.SetFrom_R_Phi(1, 0);
        }

        public void Paint(Graphics gr)
        {
            Vect2D endPoint;
            endPoint = V * 5.0;
            endPoint = Pos + endPoint;
            gr.DrawLine(_speedPen, Pos.AsPoint, endPoint.AsPoint);
        }

        public double GetPhi()
        { return V.GetPhiGrad(); }

        // Set Magnitude of V
        public void SetV(double aSpeed)
        {
            if (aSpeed == 0.0)
                aSpeed = 0.01;
            if (_speed < 0.0 && aSpeed > 0)
            {
                _speed = aSpeed;
                V.Set_R(-aSpeed);
            }
            else if (_speed < 0.0 && aSpeed < 0)
            {
                _speed = aSpeed;
                V.Set_R(-aSpeed);
            }
            else
            {
                _speed = aSpeed;
                V.Set_R(aSpeed);
            }
        }

        // Set rotational Speed
        public void Set_dPhi(double aDPhi)
        {
            _dPhiGrad = aDPhi * Par.DT;
            dPhi.SetFrom_R_Phi(1, aDPhi * Par.DT);
        }

        public bool HitInRadius(Point aMp)
        {
            if (VC.Distance(aMp, Pos.AsPoint) > 20)
                return false;
            return true;
        }

        public override string ToString()
        {
            return String.Format("{0}  {1},{2}", V, Pos.XI, Pos.YI);
        }
    }


    class Robot : BaseRobot
    {
        #region VectorGraphic
        Vect2D _oldPos;
        Vect2D[] _relPoints; // relative to Pos
        Point[] _absPoints; // absolute Points for drawing
        LinkedList<Point> _path = new LinkedList<Point>();
        #endregion

        public Robot()
          : base()
        {
            _relPoints = new Vect2D[3];
            _absPoints = new Point[3];
            Reset();
        }

        // Setup the picture (Vector-Graphic) of the Robot
        // Can be overwritten in derived Classes to create other Pictures of the Robot
        new public void Reset()
        {
            base.Reset();
            _relPoints[0].SetXY(20, 0);
            _relPoints[1].SetXY(-20, -15);
            _relPoints[2].SetXY(-20, +15);
            _path.Clear();
        }

        // rotate picture (Vector-Graphic) of the Robot by aRot
        // aRot should be a Unit-Vector
        void Rotate(Vect2D aRot)
        {
            for (int i = 0; i < _relPoints.Length; i++)
                _relPoints[i].CoMultTo(aRot);
        }

        // rotate picture (Vector-Graphic) of the ship by dPhi
        void RotateDPhi()
        {
            for (int i = 0; i < _relPoints.Length; i++)
                _relPoints[i].CoMultTo(dPhi);
        }

        new public void Paint(Graphics gr)
        {
            CalcAbsCoordinates();
            gr.DrawLines(_carPen, _absPoints);
            gr.DrawLine(_carPen, _absPoints[_absPoints.Length - 1], _absPoints[0]);
            if (showPath)
            {
                foreach (Point pt in _path)
                    gr.FillEllipse(Brushes.Red, pt.X - 2, pt.Y - 2, 4, 4);
            }
            base.Paint(gr);
        }

        void CalcAbsCoordinates()
        {
            Vect2D pointPos;
            for (int i = 0; i < _relPoints.Length; i++)
            {
                pointPos = _relPoints[i] + Pos;
                _absPoints[i] = pointPos.AsPoint;
            }
        }

        public void UpdatePath()
        {
            if (!showPath)
                return;
            if (Pos.DistBetweenPoints(_oldPos) < 10)
                return;
            _oldPos = Pos;
            if (_path.Count > 50)
                _path.RemoveLast();
            _path.AddFirst(Pos.AsPoint);
        }

        public void CalcNextPos()
        {
            GyroAngle += _dPhiGrad;
            RotateDPhi(); // Frame(n+1) = FRame(n) rot dPhi*dt
            V.CoMultTo(dPhi); // Vn+1 = Vn rot dPhi*dt
            Pos.AddTo(V, Par.DT); // Pos(n+1) = Pos(n) + V(n)*dt
                                  // wenn rechts raus links wieder rein
            DblBuffForm.frm.CorrigatePosition(ref Pos);
        }
    }
}






















