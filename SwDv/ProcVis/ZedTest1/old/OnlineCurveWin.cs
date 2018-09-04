
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
  public partial class OnlineCurveWin : Form
  {
    public GraphPane pane;
    
    LineItem m_Line1;
    LineItem m_Line2;

    public OnlineCurveWin()
    {
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
      pane.Y2Axis.IsVisible = true;
      pane.Y2Axis.MajorTic.IsOpposite = false; pane.Y2Axis.MinorTic.IsOpposite = false;
      pane.Y2Axis.Scale.Align = AlignP.Inside;
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
    
    public void SetCurve1(IPointList points)
    {
      m_Line1 = pane.AddCurve("", points, Color.Red, SymbolType.None);
    }

    public void SetCurve2(IPointList points)
    {
      m_Line2 = pane.AddCurve("", points, Color.Blue, SymbolType.None);
      m_Line2.IsY2Axis = true;
    }

    public void RefreshCurves()
    {
      grc.AxisChange();
      grc.Invalidate();
    }

    public void SetAcqPoints(bool aOn)
    {
      if (aOn) {
        m_Line1.Symbol = new Symbol(SymbolType.None, Color.Red);
        if (m_Line2 != null) m_Line2.Symbol = new Symbol(SymbolType.None, Color.Blue);
      }
      else {
        m_Line1.Symbol = new Symbol(SymbolType.Circle, Color.Red);
        if (m_Line2 != null) m_Line2.Symbol = new Symbol(SymbolType.Circle, Color.Blue);
      }
      grc.Invalidate();
    }
  }
}