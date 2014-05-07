using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WordFrequency.Utils.Data;
using WordFrequency.Utils.TextGateway;
using WordsFrequency.Contract;
using WordsFrequency.Impl;

namespace DictionaryConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length < 1)
            {
                Console.WriteLine("требуется имя файла");
                return;
            }

            var fileName = args[0];
            if(!File.Exists(fileName))
            {
                Console.WriteLine("файл \"{0}\" не найден", fileName);
                return;
            }
            
            var dataSource = new DataSource(new Lazy<ITextInputGateway>(() => new FileInputGateway(fileName)));
            var dictionary = new WordsFrequencyDictionary();
            foreach (var dictionaryItem in dataSource.GetDictionaryItems())
            {
                dictionary.AddWord(dictionaryItem);
            }

            while (true)
            {
                var prefix = Console.ReadLine();
                const int maxVariants = 10;
                try
                {
                    var query = new WordQuery(prefix, maxVariants);
                    var wordVariants = dictionary.GetWordVariants(query);
                    foreach (var variant in wordVariants)
                    {
                        Console.WriteLine(variant);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }
}
