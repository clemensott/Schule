
using System;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;

namespace vis1
{
  public partial class Form1 : Form
  {
    // es wird ein Array mit Pointern auf labels allokiert
    Label[] m_DispAry = new Label[3];
    SerialPort _serPort;
    BinaryWriter _binWr;
    BinaryReaderEx _binRd;
    
    public Form1()
    {
      InitializeComponent();
      m_DispAry[0] = m_Disp1; m_DispAry[1] = m_Disp2; m_DispAry[2] = m_Disp3;
    }
    
    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      _serPort = new SerialPort("COM4", 500000, Parity.None, 8, StopBits.One);
      _serPort.Open();
      _binWr = new BinaryWriter(_serPort.BaseStream);
      _binRd = new BinaryReaderEx(_serPort.BaseStream);
      m_Timer1.Enabled = true; m_Timer1.Interval = 100;
    }
    
    protected override void OnFormClosing(FormClosingEventArgs e)
    {
      m_Timer1.Enabled = false;
      base.OnFormClosing(e);
    }

    private void OnUCSendChk(object sender, EventArgs e)
    {
    }

    private void OnEmptyReceiveBuffer(object sender, EventArgs e)
    {
      
    }

    private void OnTimer(object sender, EventArgs e)
    {
      int id, knr, val;
      // nur wenn mindestens 3-Bytes im Empfangspuffer stehen 
      // gibts für uns etwas zu lesen
      while (_serPort.BytesToRead >= 3)
      { // Kanalnummer und Datentyp auslesen
        id = _serPort.ReadByte();
        if (id >= 11 && id <= 20) // es ist ein short Kanal
        {
          knr = id - 10;
          val = _binRd.ReadInt16();
          m_DispAry[knr - 1].Text = val.ToString();
        }
        if (id == 10) // es ist ein string Kanal
        {
          _msgLb.Items.Add(_binRd.ReadCString());
        }
        if (id >= 21 && id <= 30) // es ist ein float Kanal
        {
          knr = id - 20;
          float fval = _binRd.ReadSingle();
          m_DispAry[knr - 1].Text = fval.ToString();
        }
      }
    }

    private void OnSendEditKeyDown(object sender, KeyEventArgs e)
    {
      
    }
  }
}