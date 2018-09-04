
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
  public partial class VertBarWin : Form
  {
    public GraphPane pane;
    LineItem m_Line1;
    
    public VertBarWin()
    {
      InitializeComponent();
      pane = grc.GraphPane;
      pane.Title.FontSpec.IsBold = false; pane.Title.FontSpec.Size = 10;
      pane.Title.Text = "MyBars";
      pane.XAxis.Title.FontSpec.IsBold = false; pane.XAxis.Title.FontSpec.Size = 10;
      pane.XAxis.Title.Text = "BarNum";
      pane.YAxis.Title.FontSpec.IsBold = false; pane.YAxis.Title.FontSpec.Size = 10;
      pane.YAxis.Title.Text = "Volt";
      pane.XAxis.MajorGrid.IsVisible = true;
      pane.YAxis.MajorTic.IsOpposite = false; pane.YAxis.MinorTic.IsOpposite = false;
      pane.YAxis.MajorGrid.IsVisible = true;
      pane.YAxis.Scale.Align = AlignP.Inside;
    }

    public void SetY1Scale(bool aAuto, double aMin, double aMax)
    {
      Scale ysc = pane.YAxis.Scale;
      ysc.MaxAuto = aAuto; ysc.MinAuto = aAuto;
      ysc.Min = aMin; ysc.Max = aMax;
    }
    
    public void SetCurve1(IPointList points)
    {
      m_Line1 = pane.AddCurve("", points, Color.Red, SymbolType.None);
    }

    public void RefreshCurves()
    {
      grc.AxisChange();
      grc.Invalidate();
    }

  }
}