using System;

namespace X_Y_To_R_L_Lib
{
    public class XYRL : IXYRL
    {
        private float l, r, x, y;

        public float L { get { return l; } }

        public float R { get { return r; } }

        public float X
        {
            get { return x; }
            set
            {
                if (x == value) return;

                x = value;
                SetRL();
            }
        }

        public float Y
        {
            get { return y; }
            set
            {
                if (y == value) return;

                y = value;
                SetRL();
            }
        }

        public XYRL(float x, float y)
        {
            this.x = x;
            this.y = y;

            SetRL();
        }

        private void SetRL()
        {
            double hypo = Math.Sqrt(x * x + y * y);
            double xyAngle = Math.Atan(x / y);
            double angleDegrees = xyAngle * 180 / Math.PI;

            if (y < 0) xyAngle += Math.PI;

            r = (float)(Math.Cos(xyAngle + Math.PI / 4.0) * hypo);
            l = (float)(Math.Sin(xyAngle + Math.PI / 4.0) * hypo);

            if (y > 0) return;

            float tmp = r;
            r = l;
            l = tmp;
        }

        public override string ToString()
        {
            return string.Format("X={0}, Y={1}, R={2}, L={3}", X, Y, R, L);
        }
    }
}
