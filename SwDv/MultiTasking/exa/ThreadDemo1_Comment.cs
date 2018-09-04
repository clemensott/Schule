using System;
using System.Collections;
using System.Text;
using System.Threading;


namespace prj
{
  class ThreadDemo1
  {
    static void Main(string[] args)
    {
      Thread ta, tb; // 2-Threadobjekte zur Kontrolle der Threads deklarieren

      // Threads mit den Funktionen ( Methoden ) verbinden welche die Threads ausführen sollen
      ta = new Thread(ThrFuncA);  tb = new Thread(ThrFuncB);
      
      // Scheduling-Prioritäten der Threads setzen
      ta.Priority = ThreadPriority.Lowest;  tb.Priority = ThreadPriority.Lowest;
      
      Console.WriteLine("Hit Enter to terminate ...");

      // Threads starten
      ta.Start(); tb.Start();
      
      // Warten bis CR für den Abbruch gedrückt wurde
      Console.ReadLine();

      // Threads abbrechen ( terminieren )
      ta.Abort(); tb.Abort();
    }
    
    static void ThrFuncA()
    {
      int cnt = 0;
      while (true)
      {
        Console.WriteLine("A: {0}", cnt++);
        Thread.Sleep(1000);
      }
    }
    
    static void ThrFuncB()
    {
      int cnt = 0;
      while (true)
      {
        Console.WriteLine("B: {0}", cnt++);
        Thread.Sleep(1000);
      }
    }
  }
}
