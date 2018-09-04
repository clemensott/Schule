
using System;
// using System.Text;
using System.IO.Ports;
using System.IO;
using System.Diagnostics;
using ZedHL;

namespace vis1
{
  public enum Scaling
  {
    None = 0,
    q15 = 1,
  }

  class Scaling2
  {
    public const float q15 = (float)1.0 / (float)Int16.MaxValue;
    public const float q11 = (float)1.0 / (float)2048;
  }

  public interface IPrintCB
  {
    void DoPrint(string aTxt);
  }


  class ProtocolHandler
  {
    #region Member Variables
    protected SerialPort m_P;
    protected BinaryReaderEx m_BinRd;
    protected Stopwatch stw = new Stopwatch();
    protected int m_valSum;
    protected IPrintCB _printCB;
    #endregion

    #region Properties
    public Scaling _scal = Scaling.None;
    public BinaryWriterEx binWr;
    public short[] vs = new short[10];
    public float[] vf = new float[10];
    public IValueSink[] ivs = new IValueSink[10];
    public ByteRingBuffer[] brb = new ByteRingBuffer[10];
    public int NVals, NBytes;
    public double valsPerSec;
    #endregion

    public ProtocolHandler(SerialPort aPort, IPrintCB aPrintObj)
    {
      m_P = aPort;
      m_BinRd = new BinaryReaderEx(m_P.BaseStream);
      binWr = new BinaryWriterEx(m_P.BaseStream);
      _printCB = aPrintObj;
      for (int i = 0; i < ivs.Length; i++)
        ivs[i] = new DummyValueSink();
      stw.Reset(); stw.Start();
    }

    public bool CheckValsPerSecond()
    {
      if (stw.ElapsedMilliseconds > 1000)
      {
        stw.Stop();
        valsPerSec = (double)m_valSum/((double)stw.ElapsedMilliseconds/1000.0);
        m_valSum = 0; stw.Reset(); stw.Start();
        return true;
      }
      return false;
    }

    public void Flush()
    {
      binWr.Flush();
    }

    public void WriteSv16(byte aId, short aVal)
    {
      binWr.WriteSv16(aId, aVal);
    }

    // parses all ProtocolPacket's with all Variables
    public virtual bool ParseAllPackets()
    {
      return false;
    }

    public virtual void SwitchAcq(bool aOnOff)
    {
    }

    public virtual void Close()
    {
      binWr.Close();
      m_BinRd.Close();
    }
  }


  class NxtProtocolHandler : ProtocolHandler
  {
    public NxtProtocolHandler(SerialPort aPort, IPrintCB aPrintObj)
      :base(aPort,aPrintObj)
    {
      NVals = 2; NBytes = 4;
    }

    public override void SwitchAcq(bool aOnOff)
    {
      if (aOnOff)
        binWr.WriteSv16(10,1);
        // binWr.Write((byte)1);
      else
        binWr.WriteSv16(10, 0);
        // binWr.Write((byte)0);
    }

    public override bool ParseAllPackets()
    {
      float flV;
      if (m_P.BytesToRead < NBytes)
        return false;
      while( m_P.BytesToRead>=NBytes )
      {
        // flV = m_BinRd.ReadSingle();
        // vs[0] = (short)flV; ivs[0].AddValue(flV);
        vs[0]=m_BinRd.ReadInt16(); ivs[0].AddValue(vs[0]);
        vs[1]=m_BinRd.ReadInt16(); ivs[1].AddValue(vs[1]);
        // m_valSum += NVals;
      }
      return true;
    }
  }


  class SvIdProtocolHandler : ProtocolHandler
  {
    public SvIdProtocolHandler(SerialPort aPort, IPrintCB aPrintObj)
      : base(aPort,aPrintObj)
    {
      NVals = 4; NBytes = 3 * NVals;
    }

    public override void SwitchAcq(bool aOnOff)
    {
      if (aOnOff)
        { binWr.Write((byte)1); binWr.Write((byte)1); }
      else
        { binWr.Write((byte)1); binWr.Write((byte)0); }
    }

    public override bool ParseAllPackets()
    {
      if (m_P.BytesToRead < 3)
        return false;
      int i;
      while (m_P.BytesToRead >= 3)
      {
        i = m_BinRd.ReadByte() - 1;
        if (i >= 0 && i < NVals) // float-SV
        {
          vf[i] = m_BinRd.ReadSingle();
          ivs[i].AddValue(vf[i]);
        }
        if (i == 9) // string SV
        {
          _printCB.DoPrint(m_BinRd.ReadCString());
        }
      }
      return true;
    }
  }


  class SvIdProtocolHandler3 : SvIdProtocolHandler
  {
    const float C1 = (float)1.0 / (float)Int16.MaxValue;

    public SvIdProtocolHandler3(SerialPort aPort, IPrintCB aPrintObj)
      : base(aPort, aPrintObj)
    {
      NVals=9; NBytes=3*NVals;
    }

    public override bool ParseAllPackets()
    {
      if (m_P.BytesToRead < 3)
        return false;
      int i;
      while (m_P.BytesToRead >= 3)
      {
        i = m_BinRd.ReadByte() - 1;
        if (i == 9) { // string SV
          _printCB.DoPrint(m_BinRd.ReadCString());
          continue;
        }
        if (i >= 0 && i <= 8) { // 3.13 Format
          vf[i] = m_BinRd.Read3p13();
          ivs[i].AddValue(vf[i]);
          continue;
        }
        if (i >= 10 && i <= 19) { // short
          if( _scal==Scaling.q15 )
            vf[i - 10] = C1*(float)m_BinRd.ReadInt16();
          else
            vf[i - 10] = m_BinRd.ReadInt16();
          ivs[i-10].AddValue(vf[i-10]);
          continue;
        }
        if (i >= 20 && i <= 29) { // float
          vf[i - 20] = m_BinRd.ReadSingle();
          ivs[i - 20].AddValue(vf[i-20]);
          continue;
        }
      }
      return true;
    }
  }

  
  class SvIdProtocolHandler2 : SvIdProtocolHandler
  {
    public SvIdProtocolHandler2(SerialPort aPort, IPrintCB aPrintObj)
      : base(aPort, aPrintObj)
    {
    }

    public override bool ParseAllPackets()
    {
      if (m_P.BytesToRead < 3)
        return false;
      int i;
      while (m_P.BytesToRead >= 3)
      {
        i = m_BinRd.ReadByte() - 1;
        if (i == 9) // string SV
        {
          _printCB.DoPrint(m_BinRd.ReadCString());
          continue;
        }
        if( i>=0 && i<=3 )
          vf[i] = m_BinRd.Read1p11();
        // if( i>=1 && i<=3 ) vf[i] = m_BinRd.ReadInt16();
        ivs[i].AddValue(vf[i]);
      }
      return true;
    }
  }


  class HPerfProtocolHandler : SvIdProtocolHandler
  {
    public HPerfProtocolHandler(SerialPort aPort, IPrintCB aPrintObj)
      : base(aPort, aPrintObj)
    {
      NVals = 4; NBytes = 3 * NVals;
    }

    public override bool ParseAllPackets()
    {
      if (m_P.BytesToRead < 3)
        return false;
      int i;
      while (m_P.BytesToRead >= 3)
      {
        i = m_P.ReadByte() - 1;
        if (i == 9) // string SV
          _printCB.DoPrint(m_BinRd.ReadCString());
        else if (i >= 0 && i <= 1) // float-SV
        {
          vf[i] = m_BinRd.ReadSingle();
          // ivs[i].AddValue(vf[i]);
        }
        else if (i >= 2 && i <= 3)
        {
          int NVals = (byte)m_P.ReadByte();
          brb[i].AddBytes(m_P.BaseStream, 2*NVals);
        }
        else
          ;
      }
      return true;
    }
  }
}
