using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using Skat;

namespace TCPServer
{
    class Program
    {
        static void Main(string[] args)
        {

            IPAddress ip = IPAddress.Parse("127.0.0.1");


            TcpListener serverSocket = new TcpListener(ip, 7000);


            Afgift afgift = new Afgift();


            serverSocket.Start();
            Console.WriteLine("Server started");

            TcpClient connectionSocket = serverSocket.AcceptTcpClient();

            Console.WriteLine("Server activated");

            Stream ns = connectionSocket.GetStream();


            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true;
            while (true)
            {
                string message = sr.ReadLine();
                string answer = "";
                while (message != null && message != "")
                {

                    Console.WriteLine("Client: " + message);
                    if (message.Contains("Elbil"))
                    {
                        var number = Regex.Match(message, @"\d+").Value;

                        if (number != null)
                        {
                            int elbilafgift = Convert.ToInt32(number);
                            

                            answer = "Afgift på elBil: " + afgift.ElBilAfgift(elbilafgift).ToString();
                        }
                        else
                        {
                            answer = "Angiv pris f.eks. 200000";
                        }
                    }
                    else if (message.Contains("Personbil"))
                    {
                        var number = Regex.Match(message, @"\d+").Value;

                        if (number != null)
                        {
                            int personbilafgift = Convert.ToInt32(number);
                            

                            answer = "Afgift på personBil: " + afgift.Bilafgift(personbilafgift).ToString();
                        }
                        else
                        {
                            answer = "Angiv pris f.eks. 200000";
                        }
                    }
                    else
                    {
                        answer = "Angiv om det er PersonBil eller Elbil og prisen";
                    }
                    sw.WriteLine(answer);
                    message = sr.ReadLine();

                }

                ns.Close();
                connectionSocket.Close();

            }

            serverSocket.Stop();
        }
        
    }
}
