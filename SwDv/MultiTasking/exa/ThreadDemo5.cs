using System;
using System.Collections;
using System.Text;
using System.Threading;
using System.Runtime.CompilerServices;

// Abwandlung von ThreadDemo3
// Das beenden der Threads wird durch Thread.Join() abgewartet

namespace prj
{
  class ThreadDemo5
  {
    const int MAX_CNT = 100;
    Random rnd = new Random();

    static void Main(string[] args)
    {
      ThreadDemo5 d5 = new ThreadDemo5();
      d5.MainProg();
    }
    
    void MainProg()
    {
      Thread ta, tb;
      ta = new Thread(ThrFuncA); ta.Priority = ThreadPriority.Highest;
      tb = new Thread(ThrFuncB); tb.Priority = ThreadPriority.Lowest;
      
      Console.WriteLine("Warten bis beide Threads beendet sind");
      ta.Start(); tb.Start();

      ta.Join();
      Console.WriteLine("AAAAA Finished");
      tb.Join();
      Console.WriteLine("BBBBB Finished");
      
      Console.WriteLine("\nHit Enter to finish.....");
      Console.ReadLine();
    }
    
    void ThrFuncA()
    {
      int cnt = 0;
      for(cnt=0; cnt<=MAX_CNT; cnt++) {
        Console.WriteLine("A:{0} ", cnt);
        // Thread.Sleep(rnd.Next(10));
      }
    }

    void ThrFuncB()
    {
      int cnt = 0;
      for (cnt = 0; cnt<=MAX_CNT+10; cnt++) {
        Console.WriteLine("     B:{0} ", cnt);
        // Thread.Sleep(10);
      }
    }
  }
}
