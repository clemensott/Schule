
using System;
using System.Drawing;
using System.ComponentModel;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace ZedHL
{
  public partial class PianoForm : Form
  {
    BinaryWriter _binWr;
    const int N_TONES = 15;
    double _toneA = 440.0;
    float[] _toneTab = new float[N_TONES];
    bool _isOn;

    public PianoForm(BinaryWriter aWr)
    {
      this.ResizeRedraw = true;
      _binWr = aWr;
      InitializeComponent();
      _toneA = 220.0; // AL
      _toneTab[0] = GetHalfTone(-9); // CL
      _toneTab[1] = GetHalfTone(-7); // D
      _toneTab[2] = GetHalfTone(-5); // E
      _toneTab[3] = GetHalfTone(-4); // F
      _toneTab[4] = GetHalfTone(-2); // G
      _toneTab[5] = (float)_toneA;      // A
      _toneTab[6] = GetHalfTone(2);  // H
      _toneTab[7] = GetHalfTone(3);  // CH
      _toneA = 440.0; // AH
      _toneTab[8] = GetHalfTone(-7); // DH
      _toneTab[9] = GetHalfTone(-5); // EH
      _toneTab[10] = GetHalfTone(-4); // FH
      _toneTab[11] = GetHalfTone(-2); // GH
      _toneTab[12] = (float)_toneA;      // AH
      _toneTab[13] = GetHalfTone(2);  // HH
      _toneTab[14] = GetHalfTone(3);  // CHH
    }

    float GetHalfTone(double aNum)
    {
      return (float)(_toneA * Math.Pow(2, aNum / 12));
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      Graphics gr = e.Graphics;
      int dx = this.Size.Width / N_TONES;
      int H = this.Size.Height;
      int x;
      for (int i = 1; i < N_TONES; i++)
      {
        x = i*dx;
        gr.DrawLine(Pens.Black, x, 0, x, H);
      }
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      _lbl1.Text = string.Format("DW {0}",GetKeyIdx(e));
      if (_holdChk.Checked)
      {
        if (_isOn) SendCommand(e, 0);
        else SendCommand(e, 1);
      }
      else
        SendCommand(e, 1);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      _lbl1.Text = string.Format("UP {0}", GetKeyIdx(e));
      if (_holdChk.Checked)
        return;
      SendCommand(e, 0);
    }

    void SendCommand(MouseEventArgs aMousePos, byte aOnOff)
    {
      int idx1 = GetKeyIdx(aMousePos);
      int idx2 = idx1 + 2;
      if (idx2 >= N_TONES) idx2 = idx1;
      
      if( !_polyChk.Checked ) // single Tone
      {
        _binWr.Write((byte)4);
        _binWr.Write(_toneTab[idx1]);
        _binWr.Write(aOnOff);
      }
      else // polyphone
      {
        _binWr.Write((byte)5);
        _binWr.Write(_toneTab[idx1]);
        _binWr.Write(_toneTab[idx2]);
        _binWr.Write(aOnOff);
      }
      _binWr.Flush();
    }

    int GetKeyIdx(MouseEventArgs e)
    {
      int dx = this.Size.Width / N_TONES;
      return e.X / dx;
    }
    
    protected override void OnClosing(CancelEventArgs e)
    {
      this.Hide();
      e.Cancel = true;
    }
  }
}