
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

// 1) Aufbau einer TCP/IP (Socket) Verbindung
//
// 2) Serverseitige Kommunikation über die Verbindung auf lowest Level
//       ohne Verwendung von Streams und Stream Reader/Writer
//
// 3) Clientseitige Kommunikation über die Verbindung wesentlich
//    einfacher und bequemer über Stream und Stream Reader/Writer
//

namespace SimpleCliServ1
{
  class SimpleCliServ1
  {
    static void Main(string[] args)
    {
      string txt;
      
      Console.Write("Server or Client (s/c): ");
      txt = Console.ReadLine();

      if( txt == "s" )
        Server();
      else
        Client();
      
      Console.Write("\nHit any Key to continue "); Console.ReadKey();
    }

    static IPAddress GetIPAddress()
    {
      return Dns.Resolve("localhost").AddressList[0];
      // IPAddress ipAdr = Dns.Resolve("HollNotebook").AddressList[0];
      // return Dns.GetHostEntry("192.168.83.1").AddressList[0];
    }
    
    static void Server()
    {
      TcpListener server;
      Socket socke;
      byte[] rcBuff = new byte[256];
      
      IPAddress ipAdr = GetIPAddress();
      try {
       
        server = new TcpListener(ipAdr, 13);
        server.Start();

        Console.WriteLine("Server {0} gestartet", server.LocalEndpoint);
        while (true)
        {
          // Das Programm bleibt hier solange blockiert bis ein client eine
          // Verbindung zu uns ( Server ) öffnet
          socke = server.AcceptSocket(); 

          // socke ist die Verbindung zum Client
          Console.WriteLine("Anfrage von {0}", socke.RemoteEndPoint);

          // lesen was uns der client so sendet
          // Das Programm bleibt hier solange blockiert bis der client etwas sendet
          int nRead = socke.Receive(rcBuff);
          
          // ausgeben was uns der client gesendet hat
          string txt = Encoding.ASCII.GetString(rcBuff, 0, nRead);
          Console.WriteLine("Msg: {0}", txt);
          
          txt += " Echo!!"; // an den text des clients etwas anhängen
          Byte[] txtAry = Encoding.ASCII.GetBytes(txt.ToCharArray());
          
          socke.Send(txtAry); // text an den client senden
          
          // verbindung zum client schließen
          socke.Shutdown(SocketShutdown.Both); socke.Close();
        }
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
      }
    }
  
    static void Client()
    {
      const string serverName = "localhost";
      // const string serverName = "HollNotebook";
      // const string serverName = "192.168.83.1";
      try
      {
        // Verbindung zum server aufbauen ( connect )
        TcpClient client = new TcpClient(serverName, 13);

        // Lesen und schreiben auf den NetworkStream mithilfe von
        // StreamReader und StreamWriter
        NetworkStream strm = client.GetStream();
        StreamReader strmRd = new StreamReader(strm);
        StreamWriter strmWr = new StreamWriter(strm);

        // "Hallo" zum server senden
        strmWr.Write("Hallo"); strmWr.Flush();

        // antwort des servers lesen
        // Das Programm bleibt hier solange blockiert bis der server etwas gesendet hat
        Console.WriteLine("Answer: {0}", strmRd.ReadToEnd());
        
        // Client samt NetworkStream schließen
        client.Close(); strmRd.Close(); strmWr.Close();
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
      }
    }
  }
}
