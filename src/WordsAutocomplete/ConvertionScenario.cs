using WordsAutocomplete.Data;
using WordsFrequency;

namespace WordsAutocomplete
{
    public class ConvertionScenario
    {
        public void Execute(IDataSource dataSource, IDataDestination dataDestination, IWordsFrequencyDictionary dictionary)
        {
            const int maximumVarinatsCount = 10;

            foreach (var dictionaryItem in dataSource.GetDictionaryItems())
            {
                dictionary.AddWord(dictionaryItem);
            }

            foreach (var wordOpening in dataSource.GetWordOpenings())
            {
                var wordQuery = new WordQuery(wordOpening, maximumVarinatsCount);
                var words = dictionary.GetWordVariants(wordQuery);
                dataDestination.WriteWords(words);
            }
        }
    }
}