using System;
using System.Collections;
using System.Text;
using System.Threading;
using System.Runtime.CompilerServices;


// Basisbeispiel zu Producer/Consumer

// Unterschiedliches wait-Verhalten wird am besten dadurch erreicht, daß
// in tb.Start(); Thread.Sleep(700); ta.Start(); das ta und tb ausgetauscht wird
// je nachdem wer als erster gestartet wird synchronisieren sich Producer
// und consumer bei einem leeren oder vollen puffer

// Dies ist Variante1:
// Zur Datenübergabe wird ein RB-Objekt verwendet
// Die Synchronisation erfolgt explizit in den Prod() und Cons() Threads

// Variante2:
// Die Synchronisation erfolgt in den put() und get() Methoden des
// Puffers der dann allerdings IPC-Queue oder so heissen müßte.


namespace prj
{
  class ProdCons1
  {
    RBuffer rb = new RBuffer(10);
    Random rnd = new Random();
    
    static void Main(string[] args)
    {
      ProdCons1 m2 = new ProdCons1();
      m2.MainProg();
    }
    
    void MainProg()
    {
      Thread ta, tb;
      ta = new Thread(this.Producer); ta.Priority = ThreadPriority.Lowest;
      tb = new Thread(this.Consumer); tb.Priority = ThreadPriority.Lowest;
      
      Console.WriteLine("\nHit Enter to finish.....");
      ta.Start(); Thread.Sleep(900); tb.Start();
      Console.ReadLine();
      ta.Abort(); tb.Abort();
      Console.WriteLine("\n");
    } 

    void Producer()
    {
      int val = 0;
      bool wasEmpty;
      while (true)
      {
        while(true)
        {
          Monitor.Enter(rb);
          if (rb.isFull())
            { Console.WriteLine("PW"); Monitor.Wait(rb); }
          else
            { Monitor.Exit(rb); break; }
        }
        lock (rb)
        {
          wasEmpty = rb.isEmpty();
          Console.WriteLine("P:{0} {1}", val, rb.getCount());
          rb.put(val); val++;
          if (wasEmpty)
            Monitor.Pulse(rb);
        }
        Thread.Sleep(200);
      }
    }
    
    void Consumer()
    {
      int val = 0;
      bool wasFull;
      while (true)
      {
        while (true)
        {
          Monitor.Enter(rb);
          if (rb.isEmpty())
            { Console.WriteLine("\tCW"); Monitor.Wait(rb); }
          else
            { Monitor.Exit(rb); break; }
        }
        lock (rb)
        {
          wasFull = rb.isFull();
          val = rb.get();
          Console.WriteLine("\tC:{0} {1}", val, rb.getCount());
          if (wasFull)
            Monitor.Pulse(rb);
        }
        // Thread.Sleep(200);
      }
    }
  }


  class RBuffer
  {
    int[] buffer;
    int wrIdx, rdIdx, itemCount;
    
    public RBuffer(int aSize)
    {
      buffer = new int[aSize];
      wrIdx = rdIdx = itemCount = 0;
    }

    public int getCount()
    {
      return itemCount;
    }

    public bool isEmpty()
    {
      return itemCount == 0;
    }

    public bool isFull()
    {
      return itemCount == buffer.Length;
    }

    public void put(int aVal)
    {
      if (itemCount >= buffer.Length)
        return;
      buffer[wrIdx] = aVal; wrIdx++;
      if (wrIdx >= buffer.Length)
        wrIdx = 0;
      itemCount++;
    }

    public int get()
    {
      if (itemCount == 0)
        return 0;
      int ret = buffer[rdIdx]; rdIdx++;
      if (rdIdx >= buffer.Length)
        rdIdx = 0;
      itemCount--;
      return ret;
    }
  }
}
