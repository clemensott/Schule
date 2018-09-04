using System;
using System.Data;
// using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LcdEmul
{
  public partial class Form1 : Form
  {
    StringBuilder _txtA = new StringBuilder(20);
    StringBuilder _txtB = new StringBuilder(20);
    int _blinkIdx;
    bool _on;

    public Form1()
    {
      InitializeComponent();
    }

    void OnFormLoad(object sender, EventArgs e)
    {
      SetLine2("21:22");
      _blinkIdx = 0;
      timer1.Interval = 200; timer1.Enabled = true;
    }

    void SetLine2(string aTxt)
    {
      _txtA.Clear(); _txtA.Append(aTxt);
      _txtB.Clear(); _txtB.Append(aTxt);
      _line2.Text = _txtB.ToString();
    }

    void DoBlink()
    {
      if (!_on) {
        for (int i = 0; i < _txtA.Length; i++) {
          if ( (i==_blinkIdx) || (i==_blinkIdx+1) )
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

    void OnNum1ValCh(object sender, EventArgs e)
    {
      _blinkIdx = Convert.ToInt32(_num1.Value);
    }

    void OnNum2ValCh(object sender, EventArgs e)
    {
      string txt = _num2.Value.ToString();
      _txtA[_blinkIdx] = txt[0];
    }
    
    void OnBlinkTimer(object sender, EventArgs e)
    {
      DoBlink();
    }
  }
}
