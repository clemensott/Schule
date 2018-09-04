
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
    BinaryReader _rd;
    BinaryWriter _wr;
    
    public Form1()
    {
      InitializeComponent();
      m_DispAry[0] = m_Disp1; m_DispAry[1] = m_Disp2; m_DispAry[2] = m_Disp3;
    }
    
    protected override void OnLoad(EventArgs e)
    {
      _serPort = new SerialPort("COM4", 500000, Parity.None, 8, StopBits.One);
      _serPort.Open();
      _rd = new BinaryReader(_serPort.BaseStream);
      _wr = new BinaryWriter(_serPort.BaseStream);
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
    }

    private void OnSendEditKeyDown(object sender, KeyEventArgs e)
    {
      
    }
  }
}