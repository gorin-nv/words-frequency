using System;
using System.IO;
using System.Threading;

namespace DictionaryClient
{
    class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("требуется (1)ip-адрес или имя хоста и (2)номер порта");
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

            while (true)
            {
                var input = Console.ReadLine();
                var gateway = new ServerGateway("127.0.0.1", port);
                var responce = gateway.Request(input);
                if (responce.HasError)
                {
                    Console.WriteLine("id=" + id + ": server error: " + responce.Error);
                    Console.WriteLine(responce.Error);
                }
                else
                {
                    Console.WriteLine(responce.Text);
                }
            }
        }
    }
}