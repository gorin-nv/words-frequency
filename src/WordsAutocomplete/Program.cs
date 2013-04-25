using System;
using System.Collections.Generic;
using System.IO;
using WordsAutocomplete.Data;
using WordsAutocomplete.TextGateway;
using WordsFrequency;

namespace WordsAutocomplete
{
    class Program
    {
        static void Main()
        {
            try
            {
                //new WordsFrequencyDictionary();
                var scenario = new ConvertionScenario();
                scenario.Execute(
                    () => new DataSource(new Lazy<ITextInputGateway>(() => new FileInputGateway(GetFullFileName("input.txt")))),
                    () => new DataDestination(new Lazy<ITextOutputGateway>(() => new FileOutputGateway(GetFullFileName("output.txt")))),
                    new DictionaryStub());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static string GetFullFileName(string fileName)
        {
            var consoleName = Environment.GetCommandLineArgs()[0];
            var dir = Path.GetDirectoryName(consoleName);
            var filename = Path.Combine(dir, fileName);
            return filename;
        }
    }

    internal class DictionaryStub : IWordsFrequencyDictionary
    {
        public void AddWord(DictionaryItem item)
        {
        }

        public IEnumerable<string> GetWordVariants(WordQuery query)
        {
            return new[] {"abc", "def"};
        }
    }
}