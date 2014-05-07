using System;
using System.IO;

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

            var address = args[0];

            int port;
            if (!int.TryParse(args[1], out port))
            {
                Console.WriteLine("некорректный номер порта \"{0}\"", port);
                return;
            }

            var gateway = new ServerGateway(address, port);
            while (true)
            {
                var input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                    continue;
                var responce = gateway.Request(input);
                if (responce.HasError)
                {
                    Console.WriteLine("ошибка сервера: " + responce.Error);
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