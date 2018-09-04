
using System;
// using System.Collections.Generic;
// using System.ComponentModel;
// using System.Data;
using System.Drawing;
// using System.Text;
using System.Windows.Forms;
using ZedHL;


namespace ZedTest1
{
  public partial class FuncGenWin2 : Form
  {
    const double F_UPDATE = 5.0; // 2
    const double F_SAMPLE = 1000.0; // 1000
    const double T_SAMPLE = 1 / F_SAMPLE;
    const int VALS_PER_PAKET = (int)(F_SAMPLE / F_UPDATE);
    double m_t = 0;
    
    public FuncGenWin2()
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
      m_occ.buffSize = (int)(10 * F_SAMPLE);
      
      m_occ.SetY1Scale(false, -50, 50); // Set Scale to default 
      m_occ.SetY2Scale(false, -50, 50); // bewirkt das Autoscale
      m_occ.SetXScale(false, 5, 11);

      m_occ.SetCurve2(0, "A", Color.Red, false, T_SAMPLE);
      m_occ.SetCurve2(1, "B", Color.Blue, false, T_SAMPLE);
      m_occ.SetCurve2(2, "C", Color.Green, false, T_SAMPLE);
      
      m_occ.Show();
      m_occ.AxisChange();
      
      KeyEventHandler ev = new KeyEventHandler(OnKeyDownOnGraph);
      m_occ.AddKeyEventHandler(ev);
      // this.KeyDown += new KeyEventHandler(OnKeyDownOnGraph);
    }

    void GenValues()
    {
      double xVal;
      int ampl = m_AmplBar.Value;
      for (int i = 1; i < VALS_PER_PAKET; i++)
      {
        xVal = ampl*Math.Sin(2 * Math.PI * m_t); // Chan0
        m_occ.AddPoint(0, xVal);
        xVal = ampl * Math.Cos(2 * Math.PI * m_t); // Chan1
        m_occ.AddPoint(1, xVal);
        xVal = ampl;
        m_occ.AddPoint(2, xVal);
        m_t += T_SAMPLE;
      }
      // m_occ.AxisChange();
      m_occ.Invalidate();
      label1.Text = ampl.ToString();
    }
    
    void OnTimer(object sender, EventArgs e)
    {
      GenValues();
    }

    void OnUpdateChkChanged(object sender, EventArgs e)
    {
      timer1.Enabled =  m_UpdCheck.Checked;
      m_occ.SetAcqPoints(!m_UpdCheck.Checked);
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
      m_occ.AxisChange();
    }

    void OnShowHideChkChanged(object sender, EventArgs e)
    {
      /* if (m_occ.Visible)
        m_occ.Hide();
      else
        m_occ.Show(); */
    }
  }
}