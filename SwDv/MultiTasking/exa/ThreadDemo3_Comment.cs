using System;
using System.Collections;
using System.Text;
using System.Threading;
using System.Runtime.CompilerServices;

// Visualisierung von:
// Wie verteilt der Scheduler die Rechenzeit auf 2-Threads

// Warten bis einer der Threads MAX_CNT erreicht hat
// Signalisieren von MainProg

namespace prj
{
  class ThreadDemo3
  {
    const int MAX_CNT = 100;
    object finishedSig = new object();
    Random rnd = new Random();

    static void Main(string[] args)
    {
      ThreadDemo3 d3 = new ThreadDemo3();
      d3.MainProg();
    }
    
    void MainProg()
    {
      Thread ta, tb;
      ta = new Thread(ThrFuncA); ta.Priority = ThreadPriority.Lowest;
      tb = new Thread(ThrFuncB); tb.Priority = ThreadPriority.Lowest;
      
      Console.WriteLine("Warten bis einer der Threads MAX_CNT erreicht hat");
      ta.Start(); tb.Start();

      // Main wird hier blockiert ( bleibt stehen ) bis ThrFuncA() oder ThrFuncB()
      // einen Puls auf finishedSig geben ( finishedSig signalisieren )
      Monitor.Enter(finishedSig); 
      Monitor.Wait(finishedSig);
      
      ta.Abort(); tb.Abort();

      Console.WriteLine("\nHit Enter to finish.....");
      Console.ReadLine();
    }
    
    void ThrFuncA()
    {
      int cnt = 0;
      for(cnt=0; cnt<=MAX_CNT; cnt++)
      {
        Console.WriteLine("A:{0} ", cnt);
        // Thread.Sleep(0); // rnd.Next(10);
      }
      Console.WriteLine("A finished!!");
      // ein Signal auf finishedSig geben
      lock (finishedSig) {
        Monitor.Pulse(finishedSig);
      }
    }

    void ThrFuncB()
    {
      int cnt = 0;
      for (cnt = 0; cnt<=MAX_CNT; cnt++)
      {
        Console.WriteLine("     B:{0} ", cnt);
        // Thread.Sleep(0);
      }
      Console.WriteLine("B finished!!");
      // ein Signal auf finishedSig geben
      lock (finishedSig) {
        Monitor.Pulse(finishedSig);
      }
    }
  }
}
