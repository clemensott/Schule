using System;
// using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
// using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WordProc
{
    public delegate void FuncType1(int aData);

    public partial class Form1 : Form
    {
        public static Form1 frm;
        FuncType1 WorkerMessageFuncPtr;
        WordProc wrp;

        public Form1()
        {
            InitializeComponent();
            WorkerMessageFuncPtr = this.RecvMessageFromWorker;
            frm = this;
        }

        protected override void OnLoad(EventArgs e)
        {
            wrp = new WordProc();
            base.OnLoad(e);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            wrp.thr.Abort();
        }

        void OnMenueStartWordProc(object sender, EventArgs e)
        {
            // An den Worker Text übergeben und
            // dem Worker die Start-Nachricht schicken
            // _txtBox1.Text;
            wrp.inTxt = _txtBox1.Text;
            wrp.mbx.Put(1);
        }

        // wird im Context des GUI-Threads aufgerufen
        public void RecvMessageFromWorker(int aData)
        {
            // wird vom GUI-Thread aufgerufen wenn eine
            // Nachricht mit SendMessage2Form eingequeued wurde

            _txtBox2.Text = wrp.outTxt.ToString();
        }

        // wird im Context des WorkerThreads aufgerufen
        public void SendMessage2Form(int aData)
        {
            // wird vom Worker aufgerufen um die Form
            // über die Msg-Queue des GUI-Threads zu benachrichtigen
            // Hier gehört das mit dem Begin-Invoke rein
            BeginInvoke(WorkerMessageFuncPtr, 0);
        }
    }
}
