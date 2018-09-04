
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ZedHL;


namespace ZedTest1
{
  public partial class FuncGenWin : Form
  {
    OnlineCurveWin2 m_olc;
    
    const double F_UPDATE = 10.0; // 2
    const double F_SAMPLE = 100.0; // 1000
    const double T_SAMPLE = 1 / F_SAMPLE;
    const int VALS_PER_PAKET = (int)(F_SAMPLE / F_UPDATE);
    double m_t = 0;
    
    public FuncGenWin()
    {
      InitializeComponent();
    }
    
    protected override void OnLoad(EventArgs e)
    {
      CreateOnlineCurveWin();
      timer1.Interval = (int)((1 / F_UPDATE) * 1000); timer1.Enabled = true;
      base.OnLoad(e);
    }
    
    void CreateOnlineCurveWin()
    {
      m_olc = new OnlineCurveWin2((int)(3 * F_SAMPLE)); // 20 sec
      m_olc.SetY1Scale(false, -50, 50); // Set Scale to default 
      m_olc.SetY2Scale(false, -50, 50); // bewirkt das Autoscale
      m_olc.SetXScale(false, 0, 4);

      m_olc.SetCurve2(0, "A", Color.Red, false, T_SAMPLE);
      m_olc.SetCurve2(1, "B", Color.Blue, false, T_SAMPLE);
      m_olc.SetCurve2(2, "C", Color.Green, false, T_SAMPLE);
      
      m_olc.Show();
      m_olc.AxisChange();
      
      KeyEventHandler ev = new KeyEventHandler(OnKeyDownOnGraph);
      m_olc.AddKeyEventHandler(ev);
      // this.KeyDown += new KeyEventHandler(OnKeyDownOnGraph);
    }

    void GenValues()
    {
      double xVal;
      int ampl = m_AmplBar.Value;
      for (int i = 1; i < VALS_PER_PAKET; i++)
      {
        xVal = ampl*Math.Sin(2 * Math.PI * m_t); // Chan0
        m_olc.AddPoint(0, xVal);
        xVal = ampl * Math.Cos(2 * Math.PI * m_t); // Chan1
        m_olc.AddPoint(1, xVal);
        xVal = ampl;
        m_olc.AddPoint(2, xVal);
        m_t += T_SAMPLE;
      }
      m_olc.AxisChange();
      m_olc.InvalidateGraph();
      label1.Text = ampl.ToString();
    }
    
    void OnTimer(object sender, EventArgs e)
    {
      GenValues();
    }

    void OnUpdateChkChanged(object sender, EventArgs e)
    {
      timer1.Enabled =  m_UpdCheck.Checked;
      m_olc.SetAcqPoints(!m_UpdCheck.Checked);
    }

    void OnKeyDownOnGraph(object sender, KeyEventArgs e)
    {
      if (e.KeyValue == 72)
      {
        if (timer1.Enabled)
          timer1.Enabled = false;
        else
          timer1.Enabled = true;
      }
    }

    void OnAxisChangedButton(object sender, EventArgs e)
    {
      m_olc.AxisChange();
    }

    void OnShowHideChkChanged(object sender, EventArgs e)
    {
      if (m_olc.Visible)
        m_olc.Hide();
      else
        m_olc.Show();
    }
  }
}