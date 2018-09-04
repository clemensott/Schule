using System;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace VirtJoyStick
{
  class BinaryReaderEx : BinaryReader
  {
    byte[] m_CString = new byte[50];

    public BinaryReaderEx(Stream input)
      : base(input)
    {
    }

    public string ReadCString()
    {
      int len = 0; byte ch;
      while (true)
      {
        ch = this.ReadByte();
        if (ch == 0)
          break;
        m_CString[len] = ch; len++;
      }
      string ret = Encoding.ASCII.GetString(m_CString, 0, len);
      return ret;
    }

    // 1.11 Format
    public float Read1p11()
    {
      return (float)this.ReadInt16()/2048;
    }

    // 3.13 Format
    public float Read3p13()
    {
      return (float)this.ReadInt16()/8192;
    }
  }
  
  class BinaryWriterEx : BinaryWriter
  {
    public BinaryWriterEx(Stream input) 
      : base(input)
    {
    }
    
    public void WriteSv16(byte aId, short aVal)
    {
      this.Write(aId);
      /* this.Write((byte)aVal); // LB
      this.Write((byte)(aVal >> 8)); // HB */
      this.Write(aVal);
    }
  }
  
  class TrackBarEx : TrackBar
  {
    int m_LastVal = 0;
    public bool barWasMoved = false;
    
    public TrackBarEx()
      : base()
    {
    }

    protected override void OnValueChanged(EventArgs e)
    {
      barWasMoved = true;
      base.OnValueChanged(e);
    }

    public bool BarValueChanged()
    {
      return (this.Value != m_LastVal);
    }

    public short GetValue()
    {
      m_LastVal = this.Value;
      return (short)m_LastVal;
    }
  }


  class CommandParser
  {
    BinaryWriter _binWr;

    public CommandParser(BinaryWriter aWr)
    {
      _binWr = aWr;
    }

    public void ParseAndSend(string aCmd)
    {
      object obj; bool first = true;
      string[] words = aCmd.Split(' ');
      foreach (string txt in words)
      {
        obj = Str2Val(txt);
        if (obj == null)
          continue;
        if (first)
        {
          short sv = (short)obj;
          _binWr.Write((byte)sv); first = false;
        }
        else if (obj.GetType() == typeof(Int32))
        {
          Int32 v32 = (Int32)obj;
          _binWr.Write(v32);
        }
        else if (obj.GetType() == typeof(float))
        {
          float fv = (float)obj;
          _binWr.Write(fv);
        }
        else
        {
          short sv = (short)obj;
          _binWr.Write(sv);
        }
      }
      _binWr.Flush();
    }

    public void TestCmd1(int aCmd, int aPar1, int aPar2)
    {
      _binWr.Write((byte)aCmd);
      _binWr.Write((short)aPar1);
      _binWr.Write((short)aPar2);
      _binWr.Flush();
    }

    object Str2Val(string aTxt)
    {
      int idx; string txt2;
      
      txt2 = aTxt.Trim();
      if (txt2.Length == 0)
        return null;
      
      idx = txt2.IndexOf('l');
      if( idx!=-1 )
      {
        Int32 val;
        txt2 = txt2.Remove(idx, 1);
        val = Int32.Parse(txt2);
        return val;
      }
      idx = txt2.IndexOf('f');
      if (idx != -1)
      {
        float val;
        txt2 = txt2.Remove(idx, 1);
        val =  float.Parse(txt2);
        return val;
      }
      idx = txt2.IndexOf(',');
      if (idx != -1)
      {
        float val;
        val = float.Parse(txt2);
        return val;
      }
      idx = txt2.IndexOf('/');
      if (idx != -1)
      {
        float val;
        string[] parts = txt2.Split('/');
        val = float.Parse(parts[0]) / float.Parse(parts[1]);
        return val;
      }
      short sval;
      sval = short.Parse(txt2);
      return sval;
    }
  }
}
