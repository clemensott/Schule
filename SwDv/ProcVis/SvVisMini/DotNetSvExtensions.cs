using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace vis1
{
  class BinaryReaderEx : BinaryReader
  {
    byte[] m_CString = new byte[30];

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
      this.Write((byte)aVal); // LB
      this.Write((byte)(aVal >> 8)); // HB
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
      // base.OnValueChanged(e);
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
}
