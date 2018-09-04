
using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

// Dazu könnte man ein schönes Prozess-Ablaufdiagramm 
// an die Tafel malen

// Unterschied zu MTCliserv1
//   vereinfachtes String ( ReadLine() ) Handling

namespace MTCliserv2
{
  class MTCliserv2
  {
    static void Main(string[] args)
    {
      string txt;

      Console.Write("MTCliserv2 Server or Client (s/c): ");
      txt = Console.ReadLine();

      if (txt == "s")
        Server();
      else
        Client();

      Console.Write("\nHit any Key to continue "); Console.ReadKey();
    }

    static IPAddress GetIPAddress()
    {
      IPAddress ipAdr = Dns.Resolve("localhost").AddressList[0];
      // IPAddress ipAdr = Dns.Resolve("HollNotebook").AddressList[0];
      // return Dns.Resolve("192.168.83.1").AddressList[0];
      // return Dns.GetHostEntry("192.168.83.1").AddressList[0];
      return ipAdr;
    }

    static void Server()
    {
      TcpListener server;
      Socket socke;
      IPAddress ipAdr = GetIPAddress();
      try
      {
        server = new TcpListener(ipAdr, 13);
        server.Start();
        Console.WriteLine("Server {0} gestartet", server.LocalEndpoint);
        while (true)
        {
          socke = server.AcceptSocket(); // Aufruf blockiert!!
          // ein Client hat eine Verbindung zu uns aufgebaut
          // socke repräsentiert diese Verbindung
          // ein neuer Thread wird gestartet welcher nun die Kommunikation mit diesem Client abwickelt
          new ConnTalk(socke);
        }
      }
      catch (Exception e)
      {
        Console.WriteLine("Error {0}", e);
      }
    }

    static void PrintConnectionInfo(Socket aSock)
    {
      Console.WriteLine("Anfrage von {0}", aSock.RemoteEndPoint);
    }

    static void Client()
    {
      const string serverName = "localhost";
      // const string serverName = "HollNotebook";
      // const string serverName = "192.168.83.1";
      try
      {
        TcpClient client = new TcpClient(serverName, 13);
        NetworkStream strm = client.GetStream();
        Socket sc = client.Client;
        StreamReader strmRd = new StreamReader(strm);
        StreamWriter strmWr = new StreamWriter(strm);
        string txt, txt2;
        
        Console.WriteLine("Connected to {0}", sc.RemoteEndPoint);
        while (true)
        {
          Console.Write("Msg= "); txt = Console.ReadLine();
          if (txt == "end")
            break;
          strmWr.WriteLine(txt); strmWr.Flush();
          
          txt2 = strmRd.ReadLine();
          Console.WriteLine("Answer: {0}", txt2);
        }

        // Client samt NetworkStream schließen
        // sc.Shutdown(SocketShutdown.Both); sc.Close();
        client.Close(); strmRd.Close(); strmWr.Close();
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
      }
    }
  }


  class ConnTalk
  {
    Thread m_Thr;
    Socket m_Sc;
    
    public ConnTalk(Socket aSc)
    {
      m_Sc = aSc;
      m_Thr = new Thread(this.TalkWithClient);
      m_Thr.Start();
    }

    void TalkWithClient()
    {
      NetworkStream strm = new NetworkStream(m_Sc);
      StreamReader strmRd = new StreamReader(strm);
      StreamWriter strmWr = new StreamWriter(strm);
      string txt;
      
      Console.WriteLine("Talking with {0}", m_Sc.RemoteEndPoint);
      try
      {
        while (true)
        {
          txt = strmRd.ReadLine();
          Console.WriteLine("Msg: {0}", txt);
          txt += " Echo!! \r\n";
          strmWr.Write(txt); strmWr.Flush();
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine("Exception in TalkWithClient");
      }
      
      Console.WriteLine("{0} Disconnected", m_Sc.RemoteEndPoint);
      // m_Sc.Shutdown(SocketShutdown.Both); 
      m_Sc.Close(); strm.Close(); strmRd.Close(); strmWr.Close();
      return;
    }
  }
}
