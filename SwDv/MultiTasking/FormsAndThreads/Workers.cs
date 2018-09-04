
using System;
using System.Collections.Generic;
// using System.Text;
using System.Threading;

namespace ThrForm
{
  public class Worker1
  {
    public Thread thr;
    public int workCount;

    public Worker1()
    {
      thr = new Thread(DoWork);
      thr.Start(); thr.Suspend();
    }

    void DoWork()
    {
      while (true)
      {
        Thread.Sleep(200);
        workCount++;
        // Daten mithilfe einer Message an die Form übergeben
        Form1.frm.BeginInvoke(Form1.frm.Result1FuncPtr, workCount);
      }
    }
  }


  public class WorkQueue
  {
    Queue<int> _data = new Queue<int>();

    public void AddWork(int aData)
    {
      lock(this)
      {
        int cntBefore = _data.Count;
        _data.Enqueue(aData);
        if (cntBefore == 0)
          Monitor.Pulse(this);
      }
    }

    public int RemoveWork()
    {
      lock (this)
      {
        if (_data.Count == 0)
          Monitor.Wait(this);
        return _data.Dequeue();
      }
    }
  }
  
  
  public class Worker2
  {
    public Thread thr;
    public WorkQueue queue = new WorkQueue();

    public Worker2()
    {
      thr = new Thread(DoWork);
      thr.Start();
    }

    void DoWork()
    {
      int data;
      while (true)
      {
        DoTrace(1); Thread.Sleep(200);
        data = queue.RemoveWork(); // get Work
        DoTrace(2);
        Thread.Sleep(2000); // working
        // Show Result in the Form
        Form1.frm.BeginInvoke(Form1.frm.Result2FuncPtr, data);
      }
    }

    void DoTrace(int aData)
    {
      Form1.frm.BeginInvoke(Form1.frm.TraceFuncPtr, aData);
    }
  }
}
