using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace DictionaryClient
{
    class Program
    {
        private static void Main()
        {
            var requester = 0;

            while (true)
            {
                var input = Console.ReadLine();
                if (input == "exit")
                    break;
                if (input == "add")
                {
                    var id = requester++;
                    new Thread(() =>
                                   {
                                       var gateway = new ServerGateway("127.0.0.1", 9050);
                                       for (var i = 0; i < 100; i++)
                                       {
                                           var responce = gateway.Request(input);
                                           if (responce.HasError)
                                           {
                                               var actual = Console.ForegroundColor;
                                               Console.ForegroundColor = ConsoleColor.Red;
                                               Console.WriteLine("id=" + id + ": server error: " + responce.Error);
                                               Console.WriteLine(responce.Error);
                                               Console.ForegroundColor = actual;
                                           }
                                           else
                                           {
                                               Console.WriteLine("id=" + id + ": " + responce.Text);
                                           }
                                           Thread.Sleep(1000);
                                       }
                                   }).Start();
                }
            }
        }
    }

    internal class Responce
    {
        public static Responce Create(string text)
        {
            return new Responce {Text = text};
        }

        public static Responce CreateError(string message)
        {
            return new Responce {Error = message};
        }

        public string Text { get; private set; }
        public string Error { get; private set; }

        public bool HasError
        {
            get { return !string.IsNullOrEmpty(Error); }
        }
    }

    internal class ServerGateway
    {
        private const int BufferLength = 1024;
        private readonly byte[] _buffer;
        private readonly IPEndPoint _ipep;

        public ServerGateway(string ip, int port)
        {
            _ipep = new IPEndPoint(IPAddress.Parse(ip), port);
            _buffer = new byte[BufferLength];
        }

        public Responce Request(string request)
        {
            var requestBuffer = Encoding.ASCII.GetBytes(request);
            using (var server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                try
                {
                    server.Connect(_ipep);
                }
                catch (SocketException e)
                {
                    return Responce.CreateError(e.ToString());
                }
                server.Send(requestBuffer);
                var responce = server.Receive(_buffer);
                var data = Encoding.ASCII.GetString(_buffer, 0, responce);
                server.Shutdown(SocketShutdown.Both);
                server.Close();
                return Responce.Create(data);
            }
        }
    }
}