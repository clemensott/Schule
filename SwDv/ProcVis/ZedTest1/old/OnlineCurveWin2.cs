
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
  public partial class OnlineCurveWin2 : Form
  {
    public GraphPane pane;
    
    LineItem[] m_Line = new LineItem[5];
    RollingPointPairList[] m_Rb = new RollingPointPairList[5];
    Color[] m_Col = new Color[5];
    
    int maxIdx = 0;
    int m_BuffSize;

    public OnlineCurveWin2(int aBuffSize)
    {
      for (int i = 0; i < 5; i++) m_Col[i] = new Color();
      m_BuffSize = aBuffSize;
      InitializeComponent();
      pane = grc.GraphPane;
      pane.Title.FontSpec.IsBold = false; pane.Title.FontSpec.Size = 10;
      pane.Title.Text = "MyVals";
      pane.XAxis.Title.FontSpec.IsBold = false; pane.XAxis.Title.FontSpec.Size = 10;
      pane.XAxis.Title.Text = "Time";
      pane.YAxis.Title.FontSpec.IsBold = false; pane.YAxis.Title.FontSpec.Size = 10;
      pane.YAxis.Title.Text = "Volt";
      pane.XAxis.MajorGrid.IsVisible = true;
      pane.YAxis.MajorTic.IsOpposite = false; pane.YAxis.MinorTic.IsOpposite = false;
      pane.YAxis.MajorGrid.IsVisible = true;
      pane.YAxis.Scale.Align = AlignP.Inside;
      // pane.Y2Axis.IsVisible = true;
      // pane.Y2Axis.MajorTic.IsOpposite = false; pane.Y2Axis.MinorTic.IsOpposite = false;
      // pane.Y2Axis.Scale.Align = AlignP.Inside;
      // grc.IsShowHScrollBar = true; grc.IsShowVScrollBar = true;
      // grc.IsAutoScrollRange = true; grc.IsScrollY2 = true;
    }

    public void SetY1Scale(bool aAuto, double aMin, double aMax)
    {
      Scale ysc = pane.YAxis.Scale;
      ysc.MaxAuto = aAuto; ysc.MinAuto = aAuto;
      ysc.Min = aMin; ysc.Max = aMax;
    }

    public void SetY2Scale(bool aAuto, double aMin, double aMax)
    {
      Scale ysc = pane.Y2Axis.Scale;
      ysc.MaxAuto = aAuto; ysc.MinAuto = aAuto;
      ysc.Min = aMin; ysc.Max = aMax;
    }

    public void AddCurve(string aLabel, Color aColor, bool aIsY2)
    {
      m_Col[maxIdx] = aColor;
      m_Rb[maxIdx] = new RollingPointPairList(m_BuffSize);
      m_Line[maxIdx] = pane.AddCurve(aLabel, m_Rb[maxIdx], aColor, SymbolType.None);
      m_Line[maxIdx].IsY2Axis = aIsY2;
      maxIdx++;
    }

    // Neue Datenpunkte in den Ringbuffer
    public void AddPoint(int aIdx, double aX, double aY)
    {
      m_Rb[aIdx].Add(aX, aY);
    }
    
    // Neuzeichnen auslösen
    public void RefreshCurves()
    {
      grc.AxisChange();
      grc.Invalidate();
    }

    public void SetAcqPoints(bool aOn)
    {
      for (int i = 0; i < maxIdx; i++)
      {
        if (aOn)
          m_Line[i].Symbol = new Symbol(SymbolType.Circle, m_Col[i]);
        else
          m_Line[i].Symbol = new Symbol(SymbolType.None, m_Col[i]);
      }
      grc.Invalidate();
    }
  }
}