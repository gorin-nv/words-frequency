using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace DictionaryServer
{
    class Program
    {
        static void Main()
        {
            var listener = new TcpListener(IPAddress.Any, 9050);
            try
            {
                var active = true;
                var inProcess = false;
                listener.Start();
                var thread = new Thread(() =>
                {
                    while (active)
                    {
                        var client = listener.AcceptTcpClient();
                        inProcess = true;
                        ThreadPool.QueueUserWorkItem(ThreadProc, client);
                        inProcess = false;
                    }
                });
                thread.Start();
                Console.ReadLine();
                active = false;
                while (inProcess)
                {
                }
                thread.Abort();
            }
            finally
            {
                listener.Stop();
            }
        }

        private static void ThreadProc(object state)
        {
            var client = (TcpClient) state;
            var inputBuf = new byte[1024];
            var inputLenght = client.GetStream().Read(inputBuf, 0, inputBuf.Length);
            var input = Encoding.ASCII.GetString(inputBuf, 0, inputLenght);

            var text = "hello, " + input;
            Thread.Sleep(3000);
            var buffer = Encoding.ASCII.GetBytes(text);
            client.GetStream().Write(buffer, 0, buffer.Length);
            client.Close();

            Console.WriteLine("Client connected");
        }
    }
}
