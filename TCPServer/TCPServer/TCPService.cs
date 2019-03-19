using System;
using System.IO;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using Skat;

namespace TCPServer
{
    public class TCPService
    {
        private TcpClient connectionSocket;

        public TCPService(TcpClient connectionSocket)
        {
            this.connectionSocket = connectionSocket;
        }

        internal void DoIt()
        {
            Stream ns = connectionSocket.GetStream();

            Afgift afgift = new Afgift();
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true;
            while (true)
            {
                string message = sr.ReadLine();
                string answer = "";
                while (!string.IsNullOrEmpty(message))
                {

                    Console.WriteLine("Client: " + message);
                    if (message.Contains("Elbil"))
                    {
                        var number = Regex.Match(message, @"\d+").Value;

                        if (number != null)
                        {
                            int elbilafgift = Convert.ToInt32(number);


                            answer = "Afgift på elbil: " + afgift.ElBilAfgift(elbilafgift).ToString();
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


                            answer = "Afgift på personbil: " + afgift.Bilafgift(personbilafgift).ToString();
                        }
                        else
                        {
                            answer = "Angiv pris f.eks. 200000";
                        }
                    }
                    else
                    {
                        answer = "Angiv om det er Personbil eller Elbil og prisen";
                    }
                    sw.WriteLine(answer);
                    message = sr.ReadLine();

                }

                ns.Close();
                connectionSocket.Close();

            }
        }
    }
}