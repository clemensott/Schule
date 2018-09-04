
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
    public partial class VisForm3 : Form, IPrintCB, SliderCB
    {
        #region Member Variables
        Label[] m_LblAry = new Label[9];
        SerialPort m_SerPort;
        OnlineCurveWin3 _ow;
        OnlineCurveControl _olc;
        VertBarWin _vbw;
        ProtocolHandler ph;
        // Stopwatch stw = new Stopwatch();
        CommandParser _cmp;
        PianoForm _pnf;
        string _bitTxt;
        int _sliderSendCntr;
        #endregion

        #region Decoder Thread
        bool _doDisplay = false;
        bool _doDecode = true;
        Thread _decoderThr;
        string _msg;
        MethodInvoker _AddTextInvoker;
        #endregion

        public VisForm3()
        {
            InitializeComponent();
            m_LblAry[0] = m_Disp1; m_LblAry[1] = m_Disp2; m_LblAry[2] = m_Disp3;
            m_LblAry[3] = m_Disp4; m_LblAry[4] = m_Disp5; m_LblAry[5] = m_Disp6;
            m_LblAry[6] = m_Disp7; m_LblAry[7] = m_Disp8; m_LblAry[8] = m_Disp9;
            SetupSliders();
            _bitTxt = "0 0 0 0 0 0xx";
        }

        protected override void OnLoad(EventArgs e)
        {
            ConfigCommunication();
            _cmp = new CommandParser(ph.binWr);
            m_DispTimer.Interval = T_DISP; m_DispTimer.Enabled = true;
            _decodeTimer.Interval = T_THREAD; _decodeTimer.Enabled = true;
            CreateOnlineCurveWin();
            CreateVertWin();
            _pnf = new PianoForm(ph.binWr);
            _AddTextInvoker = this.AddText2ListBox;
            // _decoderThr = new Thread(this.DecoderThreadLoop); _decoderThr.Start();
            base.OnLoad(e);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            acqOnOffMenuItem.Checked = false;
            OnAcqOnOffMenue(null, null);
            // _doDecode = false;
            // Thread.Sleep(100);
            // _decoderThr.Join();
            ph.Close(); m_SerPort.Close();
            base.OnFormClosing(e);
        }

        void OnAcqOnOffMenue(object sender, EventArgs e)
        {
            if (acqOnOffMenuItem.Checked)
            {
                m_SerPort.DiscardInBuffer();
                Thread.Sleep(200);
                ph.SwitchAcq(true); ph.Flush();
                // m_DispTimer.Enabled = true;
                // stw.Reset(); stw.Start();
            }
            else
            {
                ph.SwitchAcq(false); ph.Flush();
                Thread.Sleep(200);
                // m_SerPort.DiscardInBuffer();
                // m_DispTimer.Enabled = false;
                // stw.Stop();
            }
        }

        void OnEmptyReceiveBufferMenue(object sender, EventArgs e)
        {
            m_SerPort.DiscardInBuffer();
        }

        void OnClearMessagesMenue(object sender, EventArgs e)
        {
            m_MsgLb.Items.Clear();
        }

        void OnAcqPointsOnOffMenue(object sender, EventArgs e)
        {
            if (!_ow.Visible) return;
            _olc.SetAcqPoints(acqPointMenuItem.Checked);
        }

        void OnDispTimer(object sender, EventArgs e)
        {
            /* if (ph.CheckValsPerSecond())
            {
              string txt = string.Format("VPS: {0:F1}", ph.valsPerSec);
              PrintMsg(txt);
            } */
            // if( ph.ParseAllPackets() )
            // DisplayValues();
            if (_doDisplay)
            {
                DisplayValues(); _doDisplay = false;
            }
            _sliderSendCntr++;
            if (_sliderSendCntr > 2)
            {
                _sliderSendCntr = 0;
                SendSliderVals();
            }
        }

        void OnDecodeTimer(object sender, EventArgs e)
        {
            if (ph.ParseAllPackets())
                _doDisplay = true;
        }

        void DecoderThreadLoop()
        {
            while (_doDecode)
            {
                Thread.Sleep(T_THREAD);
                if (ph.ParseAllPackets())
                    _doDisplay = true;
            }
            _doDisplay = false;
            ph.SwitchAcq(false);
            ph.Flush();
        }

        void DisplayValues()
        {
            for (int i = 0; i < ph.NVals; i++)
            {
                /* if (i == 8)
                  DisplayLineBits();
                else */
                m_LblAry[i].Text = String.Format("{0:F2}", ph.vf[i]);
            }
            if (_vbw.Visible)
            {
                for (int i = 0; i < ph.NVals; i++)
                    _vbw.SetBarValue(i, ph.vf[i]);
                _vbw.InvalidateGraph();
            }
            if (_ow.Visible)
            {
                // _ow.Invalidate();
                // _olc.AxisChange();
                _olc.Invalidate();
            }
        }

        void DisplayLineBits()
        {
            /* short val = (short)ph.vf[8];
            if ( val==0 )
              return;
            for (int i = 0; i < 6; i++)
            {
              if (  val & (1 << i)   )
                _bitTxt[2 * i] = '1';
              else
                _bitTxt[2 * i] = '0';
            }
            m_LblAry[8].Text = _bitTxt; */
        }

        void ToggleAcq()
        {
            if (acqOnOffMenuItem.Checked)
                acqOnOffMenuItem.Checked = false;
            else
                acqOnOffMenuItem.Checked = true;
            OnAcqOnOffMenue(null, null);
        }

        public void DoPrint(string aTxt)
        {
            // _msg = aTxt;
            // this.Invoke(_AddTextInvoker);
            m_MsgLb.Items.Add(aTxt);
            m_MsgLb.SetSelected(m_MsgLb.Items.Count - 1, true);
        }

        void AddText2ListBox()
        {
            m_MsgLb.Items.Add(_msg);
            m_MsgLb.SetSelected(m_MsgLb.Items.Count - 1, true);
        }

        void OnKeyDownOnGraph(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 72)
                ToggleAcq();
        }

        void OnSendEditKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 72)
            {
                m_SendEd.Text = "";
                ToggleAcq();
            }
            if (e.KeyValue != 13) // CR
                return;
            _cmp.ParseAndSend(m_SendEd.Text);
            /* short id, val;
            string[] words = m_SendEd.Text.Split(',');
            id = short.Parse(words[0]);
            val = short.Parse(words[1]);
            ph.binWr.Write((byte)id);
            // m_BinWr.WriteSv16((byte)id, val);
            ph.binWr.Flush(); */
        }

        void OnKeyBoardMenue(object sender, EventArgs e)
        {
            if (keyBoardMenuItem.Checked)
                _pnf.Show();
            else
                _pnf.Hide();
        }

        void OnCurveWinOnOffMenue(object sender, EventArgs e)
        {
            if (curveWinMenuItem.Checked)
                _ow.Show();
            else
                _ow.Hide();
        }

        void OnBarWinMenue(object sender, EventArgs e)
        {
            if (barWinMenuItem.Checked)
                _vbw.Show();
            else
                _vbw.Hide();
        }

        void OnMenueTest1(object sender, EventArgs e)
        {
            _cmp.TestCmd1(2, 1000, 2000);
        }
    }
}