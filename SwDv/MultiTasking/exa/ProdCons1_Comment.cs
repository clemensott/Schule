using System;
using System.Collections;
using System.Text;
using System.Threading;
using System.Runtime.CompilerServices;


// Producer/Consumer
// Kommentiert und ohne WriteLine's


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
      ta.Start(); Thread.Sleep(700); tb.Start();
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
        while(true) // solange der puffer voll ist auf rb warten
        {
          Monitor.Enter(rb);
          if (rb.isFull())
            Monitor.Wait(rb); // schlafen bis uns der Consumer aufweckt
          else {
            Monitor.Exit(rb);
            break;
          }
        }
        lock (rb) // exklusiver zugriff um Daten abzulegen
        {
          wasEmpty = rb.isEmpty();
          rb.put(val); val++;
          if (wasEmpty)
            Monitor.Pulse(rb); // wenn der puffer leer war Consumer aufwecken
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
        while (true) // solange der puffer leer ist auf rb warten
        {
          Monitor.Enter(rb);
          if (rb.isEmpty())
            Monitor.Wait(rb); // schlafen bis uns der Producer aufweckt
          else { 
            Monitor.Exit(rb); 
            break; 
          }
        }
        lock (rb) // exklusiver zugriff um Daten zu konsumieren
        {
          wasFull = rb.isFull();
          val = rb.get();
          if (wasFull)
            Monitor.Pulse(rb); // wenn der puffer voll war Producer aufwecken
        }
        Thread.Sleep(200);
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
