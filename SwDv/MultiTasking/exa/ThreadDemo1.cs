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
      Thread ta, tb;
      ta = new Thread(ThrFuncA); ta.Priority = ThreadPriority.Lowest;
      tb = new Thread(ThrFuncB); tb.Priority = ThreadPriority.Lowest;
      
      Console.WriteLine("Hit Enter to terminate ...");
      ta.Start(); tb.Start();
      Console.ReadLine();
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
