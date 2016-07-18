using Client;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ConsoleClient
{
    class Program
    {
        const int port = 8888;
        const string address = "127.0.0.1";
        public static NetworkStream stream;
        public static object locker = new object();
        static void Main(string[] args)
        {
            Console.Write("Введите свое имя:");
            string userName = Console.ReadLine();
            TcpClient client = null;
            try
            {
                client = new TcpClient(address, port);
                stream = client.GetStream();

                Send sendserv = new Send();
                Thread send = new Thread(sendserv.SendServ);
                send.Name = "send";
                send.Start();
                send.IsBackground = false;

                Getting getserv = new Getting();
                Thread getting = new Thread(getserv.GetServer);
                getting.Name = "Getting";
                getting.Start();
                getting.IsBackground = false;

                //AppDomain.CurrentDomain.UnhandledException += ()=>Console.WriteLine("fdfd");
                for (;;);
             
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                client.Close();
            }
        }
    }
}