using System;
using System.Collections;
using System.Text;
using System.Threading;
using System.Runtime.CompilerServices;


// Wenn der Buffer leer ist
// wartet der Consumer bis wieder mind. 5 Items im Buffer
// sind bevor er wieder mit dem Konsumieren anfängt


namespace prj
{
  class ProdCons2
  {
    RBuffer rb = new RBuffer(10);
    Random rnd = new Random();
    
    static void Main(string[] args)
    {
      ProdCons2 m2 = new ProdCons2();
      m2.MainProg();
    }
    
    void MainProg()
    {
      Thread ta, tb;
      ta = new Thread(this.Producer); ta.Priority = ThreadPriority.Lowest;
      tb = new Thread(this.Consumer); tb.Priority = ThreadPriority.Lowest;
      
      Console.WriteLine("\nHit Enter to finish.....");
      ta.Start(); Thread.Sleep(500); tb.Start();
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
        while (rb.isFull())
        {
          Console.WriteLine("PW");
          Monitor.Enter(rb);
          Monitor.Wait(rb);
        }
        Console.WriteLine("P:{0}", val);
        lock (rb)
        {
          wasEmpty = rb.isEmpty();
          rb.put(val); val++;
          // if (wasEmpty)
          Monitor.Pulse(rb);
        }
        Thread.Sleep(50);
      }
    }
    
    void Consumer()
    {
      int val = 0;
      bool wasFull;
      while (true)
      {
        if (rb.isEmpty())
        {
          while( rb.getCount()<5 )
          {
            Console.WriteLine("       CW");
            Monitor.Enter(rb);
            Monitor.Wait(rb);
          }
        }
        lock (rb)
        {
          wasFull = rb.isFull();
          val = rb.get();
          if (wasFull)
            Monitor.Pulse(rb);
        }
        Console.WriteLine("       C:{0}", val);
        // Thread.Sleep(50);
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
