
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.IO;

namespace DBalls1
{

    public class GCircle : GraphicObject
    {
        public GCircle() : base() { }

        public GCircle(int aX, int aY, Color aCol) : base(aX, aY, aCol) { }

        public override void PaintVisible(Graphics g)
        {
            foregBrush.Color = m_Color;
            g.FillEllipse(foregBrush, m_Pos.X - 20, m_Pos.Y - 20, 40, 40);
        }

        public override void PaintInVisible(Graphics g)
        {
            g.FillEllipse(backgBrush, m_Pos.X - 20, m_Pos.Y - 20, 40, 40);
        }

        public override int GetRadius()
        {
            return 20;
        }

    }
}











