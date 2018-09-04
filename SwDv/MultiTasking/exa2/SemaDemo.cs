using System;
// using System.Collections;
using System.Text;
using System.Threading;
// using System.Runtime.CompilerServices;

// Gehen bei mehrfachem Pulse Wakeups verloren oder nicht ?

namespace prj
{
  class SemaDemo
  {
    Semaphore sema = new Semaphore(0, 4);
    
    static void Main(string[] args)
    {
      SemaDemo d3 = new SemaDemo();
      d3.MainProg();
    }
    
    void MainProg()
    {
      Thread ta, tb;
      ta = new Thread(this.SenderThread); ta.Priority = ThreadPriority.Lowest;
      tb = new Thread(this.ReceiverThread); tb.Priority = ThreadPriority.Lowest;
      
      ta.Start(); tb.Start();
      Console.WriteLine("\nHit Enter to finish.....");
      Console.ReadLine();
      
      ta.Abort(); tb.Abort();
    }

    void SenderThread()
    {
      int cnt = 0;
      while (true)
      {
        Console.WriteLine("\nSender: {0}", cnt+=03);
        sema.Release(3); // sema.Release(1); sema.Release(1);
        Thread.Sleep(800);
      }
    }

    void ReceiverThread()
    {
      int cnt = 0;
      while (true)
      {
        sema.WaitOne();
        Console.WriteLine("Receiver: {0}", cnt++);
      }
    }
  }
}
