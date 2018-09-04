
using System;
using System.Windows.Forms;
using System.Threading;

namespace ThrForm
{
  // Definition eines Funktionstyps welcher ein int übernimmt und
  // void zurückliefert
  public delegate void FuncType1(int aData);

  public partial class Form1 : Form
  {
    public static Form1 frm;
    Worker1 worker1;
    Worker2 worker2;
    public FuncType1 Result1FuncPtr, Result2FuncPtr, TraceFuncPtr;
    int workCnt;

    public Form1()
    {
      frm = this;
      InitializeComponent();
      timer1.Interval = 100; timer1.Enabled = false;
      Result1FuncPtr = this.Result1CB;
      Result2FuncPtr = this.Result2CB;
      TraceFuncPtr = this.TRaceCB;
    }

    protected override void OnLoad(EventArgs e)
    {
      worker1 = new Worker1();
      worker2 = new Worker2();
      base.OnLoad(e);
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
      // Abort() zur Veranschaulichung weglassen
      // Prozess kann erst beendet werden wenn alle Threads beendet wurden
      if (worker1 != null)
      {
        if (worker1.thr.ThreadState == ThreadState.Suspended)
          { worker1.thr.Resume(); Thread.Sleep(100); }
        worker1.thr.Abort();
      }
      if( worker2!=null )
        worker2.thr.Abort();
      base.OnFormClosing(e);
    }

    void OnCheckChanged(object sender, EventArgs e)
    {
      if (worker1 == null)
        return;
      if (checkBox1.Checked)
        worker1.thr.Resume();
      else
        worker1.thr.Suspend();
    }

    void OnTimer(object sender, EventArgs e)
    {
      // Am einfachsten ist es Daten ( Arbeitsergebnisse )
      // aus den Threads über Polling abzufragen
      // OnTimer wird im Context der Form aufgerufen => es kann nichts schief gehen.
      // label1.Text =
      // string.Format("Worker1: {0}", worker1.workCount);
    }

    public void Result1CB(int aData)
    {
      label1.Text = string.Format("Worker1 Res.: {0}", aData);
    }

    public void Result2CB(int aData)
    {
      label2.Text = string.Format("Worker2 Res.: {0}", aData);
    }

    public void TRaceCB(int aData)
    {
      if (aData == 1)
        label4.Text = "Blocked at Remove";
      else
        label4.Text = "!! Working !!";
    }

    void OnAddWorkButton(object sender, EventArgs e)
    {
      workCnt++;
      label3.Text = string.Format("ToDo: {0}", workCnt);
      if( worker2!=null )
        worker2.queue.AddWork(workCnt);
    }
  }
}