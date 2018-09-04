using System;
using System.Collections;
using System.Text;
using System.Threading;
using System.Runtime.CompilerServices;

// Minimale Variation von ThreadDemo3
// durch sleep's

namespace prj
{
  class ThreadDemo4
  {
    const int MAX_CNT = 100;
    object finishedSig = new object();
    Random rnd = new Random();

    static void Main(string[] args)
    {
      ThreadDemo4 d4 = new ThreadDemo4();
      d4.MainProg();
    }
    
    void MainProg()
    {
      Thread ta, tb;
      ta = new Thread(ThrFuncA); ta.Priority = ThreadPriority.Lowest;
      tb = new Thread(ThrFuncB); tb.Priority = ThreadPriority.Lowest;
      
      Console.WriteLine("Warten bis einer der Threads MAX_CNT erreicht hat");
      ta.Start(); tb.Start();
      
      Monitor.Enter(finishedSig); Monitor.Wait(finishedSig);
      
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
        Thread.Sleep(50); // rnd.Next(10);
      }
      Console.WriteLine("A finished!!");
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
        Thread.Sleep(50);
      }
      Console.WriteLine("B finished!!");
      lock (finishedSig) {
        Monitor.Pulse(finishedSig);
      }
    }
  }
}
