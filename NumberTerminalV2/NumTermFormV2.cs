
using System;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using vis1;

namespace NumberTerminal
{
  public partial class NumTermFormV2 : Form
  {
    int m_Cnt;
    SerialPort m_SerPort;
    BinaryWriter m_BinWr;
    BinaryReader m_BinRd;
    CommandParser _cmp;
    StringBuilder stb = new StringBuilder(3);
    
    public NumTermFormV2()
    {
      InitializeComponent();
      textBox1.WordWrap = true;
      stb.Length = 1;
    }

    protected override void OnLoad(EventArgs e)
    {
      m_SerPort = new SerialPort("COM6", 115200, Parity.None, 8, StopBits.One);
      // m_SerPort = new SerialPort("COM4", 125000, Parity.None, 8, StopBits.One);
      // m_SerPort = new SerialPort("COM4", 9600, Parity.None, 8, StopBits.One);
      // m_SerPort = new SerialPort("COM7", 19200, Parity.None, 8, StopBits.One);
      m_SerPort.Open();
      m_BinWr = new BinaryWriter(m_SerPort.BaseStream);
      m_BinRd = new BinaryReader(m_SerPort.BaseStream);
      _cmp = new CommandParser(m_BinWr);
      timer1.Enabled = true; timer1.Interval = 100;
      base.OnLoad(e);
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
      timer1.Enabled = false;
      m_BinRd.Close(); m_BinWr.Close(); m_SerPort.Close();
      base.OnFormClosing(e);
    }

    void OnAddNumberMen(object sender, EventArgs e)
    {
      textBox1.AppendText(m_Cnt.ToString() + "   ");
      m_Cnt += 10;
    }

    void OnClearViewMen(object sender, EventArgs e)
    {
      textBox1.Clear();
    }

    // wird mit 10Hz aufgerufen
    // so wie der Ticker am MBed
    void OnTimer(object sender, EventArgs e)
    {
      if (sinleLineMenue.Checked)
        ReadAsSinleLineText();
      else
        ReadAsText();
        // ReadAllBytes();
    }

    void ReadAllBytes()
    {
      int val;
      while (m_SerPort.BytesToRead != 0)
      {
        val = m_SerPort.ReadByte();
        textBox1.AppendText(val.ToString() + "  ");
        // textBox1.AppendText(string.Format("{0:X} ", val));
      }
    }

    void ReadAsText()
    {
      // string vals; 
      char ch;
      while (m_SerPort.BytesToRead != 0)
      {
        // vals = m_SerPort.ReadExisting();
        ch = (char)m_SerPort.ReadChar();
        // textBox1.AppendText(vals + "  ");
        textBox1.AppendText(ch.ToString());
      }
    }
    
    void ReadAsSinleLineText()
    {
      string vals = string.Empty;
      while( m_SerPort.BytesToRead>=3 )
      {
        vals = m_SerPort.ReadTo("\n");
        if (vals[0] == '2')
        {
          vals = vals.Remove(0, 1);
          m_TxtLine2.Text = vals;
        }
        else
          m_TxtLine.Text = vals;
      }
    }
    
    void ReadInt16Vals()
    {
      int val;
      while( m_SerPort.BytesToRead>=2 )
      {
        val = m_BinRd.ReadInt16();
        // textBox1.AppendText(string.Format("{0:X} ", val));
        textBox1.AppendText(string.Format("{0} ", val));
      }
    }

    void ReadFloatVals()
    {
      float val;
      while (m_SerPort.BytesToRead >= 2)
      {
        val = m_BinRd.ReadSingle();
        // textBox1.AppendText(string.Format("{0:X} ", val));
        textBox1.AppendText(string.Format("{0:F} ", val));
      }
    }

    void OnAddSeperatorMen(object sender, EventArgs e)
    {
      textBox1.AppendText("||  ");
    }

    void OnSendEdKeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyValue != 13) // CR
        return;
      _cmp.ParseAndSend(m_SendEd.Text);
      /* string txt = m_SendEd.Text + '\n';
      char[] txtAry = txt.ToCharArray();
      m_BinWr.Write(txtAry, 0, txtAry.Length); */
    }

    void OnTest1Men(object sender, EventArgs e)
    {
      for(int i=1; i<=18; i++)
        m_BinWr.Write((byte)(i));
      m_BinWr.Flush();
    }
  }
}