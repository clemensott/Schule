using System;
using System.IO;


// Der FileStream wird mit einem BinaryReader/Writer verbunden
// nun können zumindest die Grunddatentypen int float char-Array
// gelesen und geschrieben werden.

class BinWrtRd 
{
  const string NAME = "demo.bin";
	
  static void Main() 
  {
		FileStream fs = new FileStream(NAME, FileMode.Create);
		BinaryWriter bw = new BinaryWriter(fs);
    int i32 = 4711; uint ui32;
		double d = 3.1415926;
    bw.Write((uint)0x1234ABCD);
    bw.Write((uint)0xFEDC9876);
		bw.Write(i32);
		bw.Write(d);
		bw.Close();
		
    fs = new FileStream(NAME, FileMode.Open, FileAccess.Read);
		BinaryReader br = new BinaryReader(fs);
    ui32 = br.ReadUInt32();
    ui32 = br.ReadUInt32();
		i32 = br.ReadInt32();
		d = br.ReadDouble();
		
    Console.WriteLine("i32 = "+i32+"\nd = "+d);
		br.Close();
	}
}

