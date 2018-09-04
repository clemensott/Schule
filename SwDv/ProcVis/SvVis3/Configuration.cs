
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.IO.Ports;
using System.IO;
// using System.Diagnostics;
using ZedHL;

namespace vis1
{
    partial class VisForm3
    {
        #region Timing
        const double F_SAMPLE = 100;
        const double T_SAMPLE = 1 / F_SAMPLE;
        const int T_DISP = 100;  // milliSec
        const int T_THREAD = 20; // milliSec
        #endregion

        void ConfigCommunication()
        {
            string com = "COM8";

            try
            {
                com = File.ReadAllText("COM.txt");
            }
            catch { }

            ComForm comForm;

            do
            {
                comForm = new ComForm(com);

                try
                {
                    //m_SerPort = new SerialPort("COM4", 115200, Parity.None, 8, StopBits.One);
                    m_SerPort = new SerialPort(comForm.COM);

                    m_SerPort.ReadBufferSize = 20 * 1024;
                    m_SerPort.Open();
                    ph = new SvIdProtocolHandler3(m_SerPort, this);
                    ph._scal = Scaling.None; // MaxI16 = +/-1.0
                                             // ph = new HPerfProtocolHandler(m_SerPort, this);
                    return;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }

                if (comForm.ShowDialog() == DialogResult.Cancel) Application.Exit();

                try
                {
                    File.WriteAllText("COM.txt", comForm.COM);
                }
                catch { }

            } while (true);
        }

        void CreateOnlineCurveWin()
        {
            _ow = new OnlineCurveWin3();
            _olc = _ow.olc;
            _olc.buffSize = (int)(20 * F_SAMPLE); // 20
            _olc.SetY1Scale(false, 0, 1000);
            _olc.SetY2Scale(false, 0, 1000);
            _olc.SetXScale(false, 10, 21);

            ph.ivs[0] = _olc.SetCurve2(0, "dLO", Color.Red, false, T_SAMPLE);
            ph.ivs[1] = _olc.SetCurve2(1, "dLI", Color.Blue, false, T_SAMPLE);
            ph.ivs[2] = _olc.SetCurve2(2, "dRO", Color.Green, false, T_SAMPLE);
            ph.ivs[3] = _olc.SetCurve2(3, "dRI", Color.Gold, false, T_SAMPLE);

            _olc.AxisChange();
            _olc.AddKeyEventHandler(new KeyEventHandler(OnKeyDownOnGraph));
        }

        void CreateVertWin()
        {
            _vbw = new VertBarWin();
            string[] names = { "1", "2", "3", "4", "5" };
            // string[] names = { "Pitch", "Roll" };
            _vbw.CreateBars2(names);
            _vbw.SetY1Scale(false, 0, 1000);
            _vbw.AxisChange();
        }

        // Wertebereich konfigurieren
        void SetupSliders()
        {
            _sb.ms[0].SetRange(0, 1000, 1); _sb.ms[0].cb = this;
            _sb.ms[0].Text = "Front"; _sb.ms[0].SetBarValue(0);

            _sb.ms[1].SetRange(0, 1000, 1); _sb.ms[1].cb = this;
            _sb.ms[1].Text = "Right"; _sb.ms[1].SetBarValue(0);

            _sb.ms[2].SetRange(0, 1000, 1); _sb.ms[2].cb = this;
            _sb.ms[2].Text = "Back"; _sb.ms[2].SetBarValue(0);

            _sb.ms[3].SetRange(0, 1000, 1); _sb.ms[3].cb = this;
            _sb.ms[3].Text = "Left"; _sb.ms[3].SetBarValue(0);
        }

        // Wird aufgerufen wenn einer der 3 Slider verstellt wurde
        // aId sagt uns welcher Slider verstellt wurde
        public void OnValChanged(int aId, MSlider aSlider)
        {
            ph.binWr.Write((byte)(aId + 2)); // cmd-Nummer
            ph.binWr.Write((short)aSlider.val); // wert int16 oder float
            ph.binWr.Flush();
        }

        // alles auskommentieren
        void SendSliderVals()
        {
            /* if (_sb.ms[0].changed)
            {
              ph.binWr.Write((byte)7);
              float val = (float)_sb.ms[0].val; val = val * 0.1f;
              ph.binWr.Write(val);
              ph.binWr.Flush();
            }
            if (_sb.ms[1].changed)
            {
              ph.binWr.Write((byte)11);
              float val = (float)_sb.ms[1].val; val = val * 0.05f;
              ph.binWr.Write(val);
              ph.binWr.Flush();
            } */
            /* if (_sb.ms[1].changed)
            {
              ph.binWr.Write((byte)5);
              ph.binWr.Write((float)_sb.ms[1].val);
            } */
            // _sb.ResetChanged();
        }

        // alles auskommentieren
        public void OnMoseUp(int aId, MSlider aSlider) // SliderCB
        {
            /* if( aId==0 )
              _sb.ms[0].SetBarValue2(0);
            if (aId == 1)
              _sb.ms[1].SetBarValue2(0); */
        }
    }
}
