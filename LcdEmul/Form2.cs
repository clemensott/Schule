using System;
using System.Data;
// using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;

namespace LcdEmul
{
  public partial class Form2 : Form
  {
    #region Member Variablen
    StringBuilder _txtA = new StringBuilder(20);
    StringBuilder _txtB = new StringBuilder(20);
    int _blinkIdx;
    bool _on;
    SerialPort _serPort;
    BinaryReader _binRd;
    #endregion

    public Form2()
    {
      InitializeComponent();
    }

    void OnFormLoad(object sender, EventArgs e)
    {
      _serPort = new SerialPort("COM6", 115200, Parity.None, 8, StopBits.One);
      _serPort.Open();
      _binRd = new BinaryReader(_serPort.BaseStream);
      _blinkIdx = -1;
      timer1.Interval=200; timer1.Enabled=true;
      timer2.Interval=100; timer2.Enabled=true;
    }

    void SetLine2(string aTxt)
    {
      _txtA.Clear(); _txtA.Append(aTxt);
      while(_txtA[0]==' ')
        _txtA.Remove(0, 1);
      _txtB.Clear(); _txtB.Append(_txtA.ToString());
      _line2.Text = _txtB.ToString();
    }

    void OnBlinkTimer(object sender, EventArgs e)
    {
      if (_blinkIdx < 0)
        return;
      if (!_on)
      {
        for (int i = 0; i < _txtA.Length; i++)
        {
          if ((i == _blinkIdx) || (i == _blinkIdx + 1))
            _txtB[i] = ' ';
          else
            _txtB[i] = _txtA[i];
        }
        _line2.Text = _txtB.ToString();
      }
      else
        _line2.Text = _txtA.ToString();
      _on = !_on;
    }

    void OnCommTimer(object sender, EventArgs e)
    {
      string vals = string.Empty;
      while (_serPort.BytesToRead >= 3)
      {
        vals = _serPort.ReadTo("\n");
        if (vals[0] == '2')
        {
          vals = vals.Remove(0, 1);
          vals.Trim();
          SetLine2(vals);
        }
        if (vals[0] == '1')
        {
          vals = vals.Remove(0, 1);
          _line1.Text = vals;
        }
        if (vals[0] == '3')
        {
          vals = vals.Remove(0, 1);
          vals.Trim();
          _blinkIdx = int.Parse(vals);
          _line2.Text = _txtA.ToString();
          _on = true;
        }
      }
    }

  }
}
