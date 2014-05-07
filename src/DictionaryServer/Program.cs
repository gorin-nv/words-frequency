using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using WordFrequency.Utils.Data;
using WordFrequency.Utils.TextGateway;
using WordsFrequency.Contract;
using WordsFrequency.Impl;

namespace DictionaryServer
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("требуется имя файла и номер порта");
                return;
            }

            var fileName = args[0];
            if (!File.Exists(fileName))
            {
                Console.WriteLine("файл \"{0}\" не найден", fileName);
                return;
            }

            int port;
            if (!int.TryParse(args[1], out port))
            {
                Console.WriteLine("некорректный номер порта \"{0}\"", port);
                return;
            }

            var dataSource = new DataSource(new Lazy<ITextInputGateway>(() => new FileInputGateway(fileName)));
            var dictionary = new WordsFrequencyDictionary();
            foreach (var dictionaryItem in dataSource.GetDictionaryItems())
            {
                dictionary.AddWord(dictionaryItem);
            }
            var listener = new TcpListener(IPAddress.Any, port);
            try
            {
                listener.Start();
                Console.WriteLine("сервер запущен");
                var thread = new Thread(() =>
                {
                    while (true)
                    {
                        var client = listener.AcceptTcpClient();
                        ThreadPool.QueueUserWorkItem(ThreadProc, new RequestContext(client, dictionary));
                    }
                });
                thread.Start();
                Console.ReadLine();
                thread.Abort();
            }
            finally
            {
                listener.Stop();
            }
        }

        private static void ThreadProc(object state)
        {
            var context = (RequestContext) state;

            var client = context.Client;
            try
            {
                var inputBuf = new byte[1024];
                var inputLenght = client.GetStream().Read(inputBuf, 0, inputBuf.Length);
                var request = Encoding.ASCII.GetString(inputBuf, 0, inputLenght);

                string response;
                var prefix = GetPrefix(request);
                if (string.IsNullOrEmpty(prefix))
                {
                    response = string.Empty;
                }
                else
                {
                    const int maxVariants = 10;
                    var query = new WordQuery(prefix, maxVariants);
                    var wordVariants = context.Dictionary.GetWordVariants(query);
                    response = string.Join(Environment.NewLine, wordVariants);
                }

                var buffer = Encoding.ASCII.GetBytes(response);
                client.GetStream().Write(buffer, 0, buffer.Length);
            }
            finally 
            {
                client.Close();
            }
        }

        private static string GetPrefix(string request)
        {
            const string command = "get ";
            if(request.StartsWith(command))
            {
                var prefix = request.Remove(0, command.Length);
                return prefix;
            }
            return string.Empty;
        }

        private class RequestContext
        {
            public RequestContext(TcpClient client, WordsFrequencyDictionary dictionary)
            {
                Client = client;
                Dictionary = dictionary;
            }

            public TcpClient Client { get; private set; }
            public WordsFrequencyDictionary Dictionary { get; private set; }
        }
    }
}