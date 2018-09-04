using System;
using System.Collections;
using System.Text;
using System.Threading;
using System.Runtime.CompilerServices;


// Ein Array soll von 2 Producer Threads abwechselnd konsistent mit
// 10 Dateneinträgen befüllt werden.
// Ohne Synchronisation ( lock mutex ) werden die Einträge der Threads 
// im Array vermischt.


namespace prj
{
  class Mutex1
  {
    object mutex = new object();
    
    static void Main(string[] args)
    {
      Mutex1 m1 = new Mutex1();
      m1.MainProg();
    }

    void MainProg()
    {
      Thread ta, tb;
      ta = new Thread(ThrFuncA); ta.Priority = ThreadPriority.Lowest;
      tb = new Thread(ThrFuncB); tb.Priority = ThreadPriority.Lowest;

      Console.WriteLine("\nHit Enter to finish.....");
      ta.Start(); tb.Start();
      Console.ReadLine();
      ta.Abort(); tb.Abort();
      Console.WriteLine("\n");
    }

    void ThrFuncA()
    {
      int num2 = 0;
      while (true)
      {
        // Monitor.Enter(mutex);
          FillArray("A", num2++);
        // Monitor.Exit(mutex);
      }
    }

    void ThrFuncB()
    {
      int num2 = 0;
      while (true)
      {
        // lock (mutex)
        {
          FillArray("B", num2++);
        }
      }
    }

    void FillArray(string aThrName, int aNum2)
    {
      int i;
      for (i = 0; i <= 10; i++)
      {
        Console.Write("{0}:{1} ", aThrName, aNum2*10+i);
        Thread.Sleep(10);
      }
      Console.WriteLine();
    }
  }

}
