using System;
// using System.Collections.Generic;
// using System.Linq;
using System.Text;
using System.Threading;

namespace WordProc
{
    // Dieselbe MailBox-Idee wie im Request/Response Beispiel
    class MailBox
    {
        int msg;
        public void Put(int aMsg) // Signals waiting Thread
        {
            lock (this)
            {
                Monitor.Pulse(this);
            }
        }
        public int Get() // Blocks Caller
        {
            lock (this)
            {
                Monitor.Wait(this);
            }

            return 0;
        }
    }

    class WordProc
    {
        public Thread thr;
        public string inTxt; // der zu verarbeitende Input-Text

        // der word-processte output-Text
        public StringBuilder outTxt = new StringBuilder(2000);

        public MailBox mbx = new MailBox(); // Mailbox of Worker

        public WordProc()
        {
            thr = new Thread(MainLoop);
            thr.Start();
        }

        void MainLoop()
        {
            // an der mbx auf einen neuen Auftrag warten
            // wenn neuer Autrag da ProcessWords() aufrufen

            while (true)
            {
                mbx.Get();

                outTxt.Clear();

                for (int i = 0; i < inTxt.Length; i++)
                {
                    outTxt.Append(inTxt[i]);

                    if (inTxt[i] == ' ') outTxt.Append("  ");
                    else outTxt.Append('.');

                    Thread.Sleep(10);

                    if ((i - 1) % 8 == 0)
                    {
                        Form1.frm.SendMessage2Form(0);
                        Thread.Sleep(500);
                    }
                }

                Form1.frm.SendMessage2Form(0);
            }
        }

        void ProcessWords()
        {
            // Den inTxt char für char word-processen
            // Nach jeweils 20 Buchstabe die Form benachrichtigen
            // damit das momentane Zwischenergebniss angezeigt werden kann
        }
    }
}
