using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ZedGraph;

namespace ZedTest1
{
  public partial class Form1 : Form
  {
    GraphPane m_GrPane;
    RollingPointPairList m_GrRb;
    LineItem m_GrLine;
    
    const double T_SAMPLE = 0.05;
    const int POINTS_PER_STEP = 5;
    double m_t = 0;
    
    public Form1()
    {
      InitializeComponent();
      
      m_GrPane = m_GC.GraphPane;
      m_GrPane.Title.Text = "My Vals";
      m_GrPane.XAxis.Title.Text = "Time";
      m_GrPane.YAxis.Title.Text = "Volt";
      Scale ysc = m_GrPane.YAxis.Scale;
      ysc.MaxAuto = false; ysc.MinAuto = false;
      ysc.Min = -5; ysc.Max = 5;
      /* Scale xsc = m_GrPane.XAxis.Scale;
      xsc.MaxAuto = false; xsc.MinAuto = false;
      xsc.Min = 0; xsc.Max = 10; */
      
      m_GrRb = new RollingPointPairList(100); // 5 sec
      m_GrLine = m_GrPane.AddCurve("",m_GrRb,Color.Blue,SymbolType.Circle);
    }

    void OnStepButton(object sender, EventArgs e)
    {
      double xVal;
      for (int i = 1; i < POINTS_PER_STEP; i++)
      {
        xVal = Math.Sin(2 * Math.PI * m_t);
        m_GrRb.Add(m_t, xVal);
        m_t += T_SAMPLE;
      }      
      m_GC.AxisChange();
      m_GC.Invalidate();
    }
  }
}