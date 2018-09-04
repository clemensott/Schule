using System;
using System.Collections;
using System.Text;
using System.Threading;
// using System.Runtime.CompilerServices;


// Ähnliche Aufgabenstellung wie bei Mutex1.cs
// Das befüllte Array wird jetzt allerdings durch einen eigenen
// Consumer-Thread ausgelesen

// Skizze des Zusammenspiels der Threads, Puffer und Sync-Objekte an der Tafel

// consSig weckt den Consumer auf wenn buffer fertig befüllt ist.

// mutex regelt den wechselseitigen Zugriff von ProducerA und ProducerB 
// auf den DatenBuffer ( buffer )

// wird mutex nicht verwendet liest der Consumer einen inkonsistenten 
// Datenstand aus dem DatenBuffer ( buffer )

// Hausübung: einen 3'ten Producer hinzufügen


namespace prj
{
  class TwoProducers
  {
    int[] buffer = new int[20];
    int idx;
    object consSig = new object(); 
    object mutex = new object();
    
    static void Main(string[] args)
    {
      TwoProducers m2 = new TwoProducers();
      m2.MainProg();
    }

    void MainProg()
    {
      Thread ta, tb, tc;
      ta = new Thread(ProducerA); ta.Priority = ThreadPriority.Lowest;
      tb = new Thread(ProducerB); tb.Priority = ThreadPriority.Lowest;
      tc = new Thread(Consumer); tc.Priority = ThreadPriority.Highest;

      Console.WriteLine("\nHit Enter to finish.....");
      ta.Start(); tb.Start(); tc.Start();
      Console.ReadLine();
      ta.Abort(); tb.Abort(); tc.Abort();
      Console.WriteLine("\n");
    }

    void ProducerA()
    {
      while (true)
      {
        lock (mutex)
        {
          for (idx = 0; idx < buffer.Length; idx++)
          {
            buffer[idx] = idx;
            Thread.Sleep(5);
          }
          lock (consSig)
          { Monitor.Pulse(consSig); }
        }
      }
    }
    
    void ProducerB()
    {
      while (true)
      {
        lock (mutex) 
        {
          for (idx = 0; idx < buffer.Length; idx++)
          {
            buffer[idx] = idx * 10;
            Thread.Sleep(10);
          }
          lock (consSig)
          { Monitor.Pulse(consSig); }
        }
      }
    }

    void Consumer()
    {
      while (true)
      {
        Monitor.Enter(consSig);
        Monitor.Wait(consSig);
        for(int i = 0; i < buffer.Length; i++)
          Console.Write("{0} ", buffer[i]);
        Console.WriteLine();
      }
    }
  
  }
}
