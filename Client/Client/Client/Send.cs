using ConsoleClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    class Send
    {

        public void SendServ()
        {
            while (true)
            {
                Console.Write(Thread.CurrentThread.Name + ": ");
                // ввод сообщения
                string message = Console.ReadLine();
                message = String.Format("{0}: {1}", Thread.CurrentThread.Name, message);
                // преобразуем сообщение в массив байтов
                byte[] data = Encoding.Unicode.GetBytes(message);
                // отправка сообщения
                lock (Program.locker)
                {
                    Program.stream.Write(data, 0, data.Length);
                }
            }


        }
    }
}
