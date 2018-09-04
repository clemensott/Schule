
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports; // das brauchen wir für die serielle
using System.IO;

namespace vis1
{
  public partial class Form1 : Form
  {
    // es wird ein Array mit Pointern auf labels allokiert
    Label[] m_DispAry = new Label[3];
    SerialPort m_SerPort;
    BinaryWriter m_BinWr;
    BinaryReaderEx m_BinRd;

    public Form1()
    {
      InitializeComponent();
      m_DispAry[0] = m_Disp1; m_DispAry[1] = m_Disp2; m_DispAry[2] = m_Disp3;
    }
    
    protected override void OnLoad(EventArgs e)
    {
      m_SerPort = new SerialPort("COM4", 500000, Parity.None, 8, StopBits.One);
      m_SerPort.Open();
      m_BinWr = new BinaryWriter(m_SerPort.BaseStream);
      m_BinRd = new BinaryReaderEx(m_SerPort.BaseStream);
      m_Timer1.Enabled = true;  m_Timer1.Interval = 100;
      base.OnLoad(e);
    }
    
    protected override void OnFormClosing(FormClosingEventArgs e)
    {
      m_Timer1.Enabled = false;
      m_BinRd.Close(); m_BinWr.Close(); m_SerPort.Close();
      base.OnFormClosing(e);
    }

    void OnMenueClearMessages(object sender, EventArgs e)
    {
      _Lb1.Items.Clear();
    }

    void Print(string txt)
    {
      _Lb1.Items.Add(txt);
      _Lb1.SetSelected(_Lb1.Items.Count - 1,true);
    }

    private void OnUCSendChk(object sender, EventArgs e)
    {
      m_BinWr.Write((byte)1); // cmd==1
      if( m_uCSendChk.Checked )
        m_BinWr.Write((byte)1); // data==1
      else
        m_BinWr.Write((byte)0); // data==0
      m_BinWr.Flush(); // Sendepuffer leeren ( runterschrieben )
    }

    // Daten empfangen indem wir die serielle Schnittstelle
    // mit 10Hz (Timer) pollen
    private void OnTimer(object sender, EventArgs e)
    {
      int id, knr, val;
      // nur wenn mindestens 3-Bytes im Empfangspuffer stehen 
      // gibts für uns etwas zu lesen
      while (m_SerPort.BytesToRead >= 3)
      { // Kanalnummer und Datentyp auslesen
        id = m_SerPort.ReadByte();
        if (id == 10) // es ist eine Textmedung
        { // Text lesen und einstweilen ignorieren
          string txt = m_BinRd.ReadCString();
          Print(txt);
        }
        if (id >= 11 && id <= 20) // es ist ein short Kanal
        {
          knr = id - 10;
          val = m_BinRd.ReadInt16();
          m_DispAry[knr-1].Text = val.ToString();
        }
      }
    }

    private void OnSendEditKeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyValue != 13) // CR
        return;
      
    }

    void OnLedButton(object sender, EventArgs e)
    {
      Int16 val;
      val = Convert.ToInt16(m_SendEd.Text);
      m_BinWr.Write((byte)3); // 3 ist das Kommando für die LETD's
      m_BinWr.Write(val); // zuerst das Kommando dann die Daten
      m_BinWr.Flush(); // Puffer leeren senden erzwingen
    }
  }
}