using Server;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace ConsoleServer
{
    public class ClientObject
    {
        public TcpClient client;
        public ClientObject(TcpClient tcpClient)
        {
            client = tcpClient;
        }
        public static NetworkStream stream = null;

        public void Process()
        {
            Console.WriteLine($"client connected :{client.Connected}\n client Exclusive addrss use :{client.ExclusiveAddressUse}\n client send buffer size {client.SendBufferSize} \n client linger state:{client.LingerState} \n");
            try
            {
                stream = client.GetStream();
                byte[] data = new byte[256]; // буфер для получаемых данных
                while (true)
                {
                    // получаем сообщение
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (stream.DataAvailable);

                    string message = builder.ToString();
                    command Command = ParseResult(message);
                    if (Command == command.NOP)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            message = message.Substring(message.IndexOf(':') + 1).Trim().ToUpper();
                            data = Encoding.Unicode.GetBytes(message);
                            stream.Write(data, 0, data.Length);
                            Thread.Sleep(100);
                        }

                    }
                    else if (Command == command.download)
                    {
                        Thread clientThread = new Thread(CommandObserver.Download);
                        clientThread.Start();
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (stream != null)
                    stream.Close();
                if (client != null)
                    client.Close();
            }
        }

        static command ParseResult(string result)
        {
            foreach (var keyVal in Server.Enum.commandList)
            {
                Regex reg = new Regex(keyVal.Value);
                if (reg.IsMatch(result))
                {
                    return keyVal.Key;
                }
            }
            return command.NOP;
        }
    }


}