using System;
using System.IO;
using WordFrequency.Utils.Data;
using WordFrequency.Utils.TextGateway;
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
                    () => new DataSource(new Lazy<ITextInputGateway>(() => new FileInputGateway("input.txt"))),
                    () => new DataDestination(new Lazy<ITextOutputGateway>(() => new FileOutputGateway("output.txt"))),
                    new WordsFrequencyDictionary());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}