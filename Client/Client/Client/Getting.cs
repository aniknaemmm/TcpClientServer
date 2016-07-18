using ConsoleClient;
using System;
using System.IO;
using System.Text;
using System.Threading;

namespace Client
{
    class Getting
    {
        private static Stream streamW;

        public void GetServer()
        {

            while (true)
            {
                Thread.Sleep(100);
                // получаем ответ
                var data = new byte[256]; // буфер для получаемых данных
                StringBuilder builder = new StringBuilder();
                int bytes = 0;
                //lock (Program.locker)
                //{
                do
                {
                    bytes = Program.stream.Read(data, 0, data.Length);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    streamW = new FileStream("temp.txt", FileMode.Create);
                    BinaryWriter binwr = new BinaryWriter(streamW, Encoding.Unicode);
                    Console.WriteLine(builder);
                    binwr.Write(data);
                    binwr.Close();
                    streamW.Close();
                }
                while (Program.stream.DataAvailable);
                //}
            }
        }
    }
}
