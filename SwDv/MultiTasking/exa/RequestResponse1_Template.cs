using System;
using System.Collections;
using System.Text;
using System.Threading;
using System.Runtime.CompilerServices;

// Variante1:
// MailBox ist nur ein Datenkontainer
// Synchronisation erfolgt explizit im Requester() und Responder()

// Skizze von requestBox, responseBox, Requester, Responder an der Tafel

namespace prj
{
  class RequestResponse1
  {
    MailBox requestBox = new MailBox();
    MailBox responseBox = new MailBox();
    
    static void Main(string[] args)
    {
      RequestResponse1 m2 = new RequestResponse1();
      m2.MainProg();
    }

    void MainProg()
    {
      // Threads starten
      // auf Tastendruck warten ....
    } 

    void Requester()
    {
      int val = 0, ret = 0;
      while (true)
      {
        Thread.Sleep(1000);
        val++;
        Console.WriteLine("Request:{0}", val);
        
        // Anfrage senden

        // Auf die Antwort warten ( Thread blockiert hier )
        // antwort lesen  ret = ....

        Console.WriteLine("Answer:{0}\n", ret);
      }
    }

    void Responder()
    {
      int requestVal=0, answer;
      while (true)
      {
        // auf eine Anfrage warten ( Thread blockiert hier )
        // requestVal aud der Mailbox auslesen
        
        Console.WriteLine("Got Request:{0}", requestVal);

        Thread.Sleep(1000); // Verarbeitungszeit simulieren

        answer = requestVal + 2;
        Console.WriteLine("Send Answer:{0}", answer);
        
        // Antwort senden ( answer und Signal auf die responseBox )
      }
    }
  }


  class MailBox
  {
    public int m_data;

    public void put(int aVal)
    {
      m_data = aVal;
    }

    public int get()
    {
      return m_data;
    }
  }
}
