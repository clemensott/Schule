
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.IO;


namespace DBalls1
{
    public class GRectangle : GraphicObject
    {
        public GRectangle() : base() { }

        public GRectangle(int aX, int aY, Color aCol) : base(aX, aY, aCol) { }

        public override void PaintVisible(Graphics g)
        {
            foregBrush.Color = m_Color;
            g.FillRectangle(foregBrush, m_Pos.X - 10, m_Pos.Y - 10, 20, 20);
        }

        public override void PaintInVisible(Graphics g)
        {
            g.FillRectangle(backgBrush, m_Pos.X - 10, m_Pos.Y - 10, 20, 20);
        }

        public override int GetRadius()
        {
            return 10;
        }
    }
}











