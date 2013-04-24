using System;
using WordsAutocomplete.TextGateway;
using WordsFrequency;

namespace WordsAutocomplete
{
    class Program
    {
        static void Main()
        {
            var dataSource = new TextInputGateway();
            var dataDestination = new TextOutputGateway();
            var dictionary = new WordsFrequencyDictionary();
            var programScenario = new ProgramScenario(dataSource, dataDestination, dictionary);
            try
            {
                programScenario.Execute();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}