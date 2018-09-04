using System;
using System.IO;


// StreamWriter/Reader
// Text und Zahlenwerte werden als ascii-Text auf den FileStream geschrieben
// Vorteil:
//     Daten auf dem File können von Humans gelesen werden
// Nachteil:
//     Verbraucht für numerische Daten (int, float) wesentlich mehr Speichrplatz
//     als Binärdateinen


class StreamWrtRd {
	const string NAME = "demo.txt";
	
  static void Main() 
  {
    // Datei NAME zum schreiben öffnen
    FileStream stream = new FileStream(NAME, FileMode.Create, FileAccess.Write);
    // StreamWriter ( TextWriter ) mit dem Stream verbinden.
    StreamWriter sw = new StreamWriter(stream);
		int i32 = 4711;
		double d = 3.1415926;
		sw.WriteLine("Zeile 1");
		sw.WriteLine("i32 = " + i32.ToString() + ", d = " + d.ToString());
		sw.WriteLine("Zeile 3");
    // StreamWriter und Stream schließen
		sw.Close();
    
    stream = new FileStream(NAME, FileMode.Open, FileAccess.Read);
    StreamReader sr = new StreamReader(stream);
		                  
		Console.WriteLine("Inhalt der Datei "+
		                  ((FileStream)sr.BaseStream).Name+"\n");
		int i = 0;
		while (sr.Peek() >= 0) {
			i++;
			Console.WriteLine(i+":\t"+sr.ReadLine());
		}
		sr.Close();
	}
}

