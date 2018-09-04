using System;
using System.IO;

// Benutzung eines Files ohne Stream Reader/Writer
// es können nur Bytes und Byte-Arrays gelesen und geschrieben werden

class FSDemo 
{
	static void Main() 
  {
		FileStream fs = null;
		byte[] arr = {0,1,2,3,4,5,6,7};
		try {
			fs = new FileStream("demo.bin", FileMode.Create);
			fs.Write(arr, 0, arr.Length);
			
      fs.Seek(0, SeekOrigin.Begin);
			
      fs.Read(arr, 0, arr.Length);
			for (int i = 0; i < arr.Length; i++)
				Console.WriteLine(arr[i]);
		} catch (Exception e) {
			Console.WriteLine("IO-Fehler: "+e.Message);
		} finally {
			if (fs != null)
				fs.Close();
		}
	}
}

