using System;
using System.Collections;
using System.Text;
using System.Threading;
using System.Runtime.CompilerServices;

// einfaches Beispiel zur einseitigen Synchronistaion ( Signalisierung )

// ReceiverThread() wird durch SenderThread() zyklisch immer wieder aufgeweckt.
// zum Benachrichtigen wird Monitor.Wait(sigObject) und Monitor.Pulse(sigObject) verwendet

// Hausübung
// Auf 2 bzw. N Receiver erhöhen und überprüfen ob die auch schön Round-Robin drankommen


namespace prj
{
  class SignalingDemo
  {
    object sigObject = new object();

    static void Main(string[] args)
    {
      SignalingDemo d3 = new SignalingDemo();
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
        Console.WriteLine("Sender: {0}", cnt++);
        lock (sigObject) {
          Monitor.Pulse(sigObject);
        }
        Thread.Sleep(800);
      }
    }

    void ReceiverThread()
    {
      int cnt = 0;
      while (true)
      {
        lock (sigObject) {
          Monitor.Wait(sigObject);
        }
        Console.WriteLine("Receiver: {0}", cnt++);
      }
    }

  }
}
