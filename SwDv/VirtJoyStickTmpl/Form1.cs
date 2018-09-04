using System;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using BertlControlLib;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace VirtJoyStick
{
    public partial class Form1 : Form
    {
        private SerialPort m_SerPort;
        private BinaryWriter m_binWr;
        private BinaryReaderEx m_binRd;

        private float curDistance = 100;
        private List<float> distances;

        private BertlXbox bertlCon;

        public Form1()
        {
            InitializeComponent();

            distances = new List<float>();
            distances.Add(100);

            m_SerPort = new SerialPort("COM5");
            m_SerPort.Open();

            m_binWr = new BinaryWriter(m_SerPort.BaseStream);
            m_binRd = new BinaryReaderEx(m_SerPort.BaseStream);
            bertlCon = new BertlXbox(BertlControlLib.Control.Common);
        }

        protected override void OnLoad(EventArgs e)
        {
            _timer1.Enabled = true; _timer1.Interval = 100;
            base.OnLoad(e);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
        }

        void OnTimer(object sender, EventArgs e)
        {
            SteerBertl();
            ReadSerial();
        }

        private void ReadSerial()
        {
            int id, knr, val;
            // nur wenn mindestens 3-Bytes im Empfangspuffer stehen 
            // gibts für uns etwas zu lesen
            while (m_SerPort.BytesToRead >= 3)
            { // Kanalnummer und Datentyp auslesen
                id = m_SerPort.ReadByte();
                if (id >= 11 && id <= 20) // es ist ein short Kanal
                {
                    knr = id - 10;
                    val = m_binRd.ReadInt16();
                }
                if (id == 10) // es ist ein string Kanal
                {
                    System.Diagnostics.Debug.WriteLine("String");
                }
                if (id >= 21 && id <= 30) // es ist ein float Kanal
                {
                    knr = id - 20;
                    curDistance = m_binRd.ReadSingle();

                    //distances.Add(fval);

                    //m_panel.Invalidate();
                }
            }
        }

        private void SteerBertl()
        {
            bertlCon.Upadate();

            float l = bertlCon.IRL.L;
            float r = bertlCon.IRL.R;

            m_lbl_CurDistance.Text = curDistance.ToString();

            if (Math.Abs(l) < 0.1 || (curDistance < 0 && l < 0)) l = 0;
            if (Math.Abs(r) < 0.1 || (curDistance < 0 && r < 0)) r = 0;

            m_binWr.Write((byte)4);
            m_binWr.Write((float)l);
            m_binWr.Write((float)r);
        }

        private void m_panel_Paint(object sender, PaintEventArgs e)
        {
            while (distances.Count > 50) distances.RemoveAt(0);

            int i = 0;
            float distancesCount = Convert.ToSingle(distances.Count);
            float width = m_panel.Width;
            float height = m_panel.Height;
            PointF prePoint = new PointF();

            foreach (float value in distances)
            {
                float x = i / distancesCount * width;
                float y = height - value * 1;

                PointF curPoint = new PointF(x, y);

                e.Graphics.DrawLine(Pens.Black, prePoint, curPoint);

                prePoint = curPoint;
                i++;
            }
        }
    }
}
