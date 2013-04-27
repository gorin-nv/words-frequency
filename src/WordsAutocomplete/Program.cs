using System;
using System.IO;
using WordsAutocomplete.Data;
using WordsAutocomplete.TextGateway;
using WordsFrequency.Impl;

namespace WordsAutocomplete
{
    class Program
    {
        static void Main()
        {
            try
            {
                var scenario = new ConvertionScenario();
                scenario.Execute(
                    () => new DataSource(new Lazy<ITextInputGateway>(() => new FileInputGateway(GetFullFileName("input.txt")))),
                    () => new DataDestination(new Lazy<ITextOutputGateway>(() => new FileOutputGateway(GetFullFileName("output.txt")))),
                    new WordsFrequencyDictionary());
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
}