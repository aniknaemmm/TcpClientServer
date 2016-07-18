using ConsoleServer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    public static class CommandObserver
    {
        //здесь будет обработчик для каждого енама чо бы можно было вызывать в потоке
        private static object locker = new object();

        public static void Download()
        {
            var data = new byte[256];

            var cmdlin = Environment.CommandLine;
            StreamWriter streamW = new StreamWriter($"{data}temp.txt");
            streamW.Write($"this file contains tcplistener connect information Client {DateTime.Now}:\n");
            streamW.Close();
            FileStream filastrem = new FileStream("temp.txt", FileMode.Open);
            BinaryReader fileBinaryRead = new BinaryReader(filastrem);
            data = fileBinaryRead.ReadBytes(256);
            lock (locker)
            {
                ClientObject.stream.Write(data, 0, data.Length);
            }
            fileBinaryRead.Close();
            Thread.Sleep(10000);
        }

    }
}
