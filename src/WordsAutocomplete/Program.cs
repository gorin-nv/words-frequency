using System;
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
                var dataSource = new DataSource(new TextInputGateway());
                var dataDestination = new DataDestination(new TextOutputGateway());
                var dictionary = new WordsFrequencyDictionary();
                var scenario = new ConvertionScenario();
                scenario.Execute(dataSource, dataDestination, dictionary);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}