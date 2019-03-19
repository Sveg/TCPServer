using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Skat;

namespace TCPServer
{
    class Program
    {
        static void Main(string[] args)
        {

            IPAddress ip = IPAddress.Parse("127.0.0.1");
            
            TcpListener serverSocket = new TcpListener(ip, 7000);

            serverSocket.Start();

            while (true)
            {
                Console.WriteLine("Server started");

                TcpClient connectionSocket = serverSocket.AcceptTcpClient();

                Console.WriteLine("Server activated");

                TCPService service = new TCPService(connectionSocket);

                Task.Factory.StartNew(() => service.DoIt());
            }
            
            serverSocket.Stop();
        }
        
    }
}
