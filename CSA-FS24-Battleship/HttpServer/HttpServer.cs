using System.Net;
using System.Net.Sockets;

namespace HttpServer;

public class HttpServer {
    public static void StartServer() {
        
        string filename = "data.txt";
        //string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        string logDirectory = "logs/data.txt";
        
        //port should be higher then 1024
        TcpListener listen = new TcpListener(IPAddress.Any, 8080);
        listen.Start();
        
        bool busy = true;
        while (busy) {
            Console.WriteLine("Wait for connection on port {0}...",
                listen.LocalEndpoint);
            try {
                new Thread(new Handler(listen.AcceptTcpClient(), filename, logDirectory).Do).Start();
            }
            catch (Exception) {
                busy = false;
            }
        }
        Console.WriteLine("HttpServer connection closed...");
    }
}