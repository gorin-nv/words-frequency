using System;
using WordFrequency.Utils.Data;
using WordsFrequency.Contract;

namespace WordsAutocomplete
{
    public class ConvertionScenario
    {
        public void Execute(Func<IDataSource> dataSourceFactory, Func<IDataDestination> dataDestinationFactory, IWordsFrequencyDictionary dictionary)
        {
            using (var dataSource = dataSourceFactory())
            {
                foreach (var dictionaryItem in dataSource.GetDictionaryItems())
                {
                    dictionary.AddWord(dictionaryItem);
                }

                using (var dataDestination = dataDestinationFactory())
                {
                    foreach (var wordOpening in dataSource.GetWordOpenings())
                    {
                        const int maximumVarinatsCount = 10;
                        var wordQuery = new WordQuery(wordOpening, maximumVarinatsCount);
                        var words = dictionary.GetWordVariants(wordQuery);
                        dataDestination.WriteWords(words);
                    }
                }
            }
        }
    }
}