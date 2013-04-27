using System.Collections.Generic;

namespace WordsFrequency.Contract
{
    public interface IWordsFrequencyDictionary
    {
        void AddWord(DictionaryItem item);
        IEnumerable<string> GetWordVariants(WordQuery query);
    }
}