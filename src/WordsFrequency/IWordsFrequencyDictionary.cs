using System.Collections.Generic;

namespace WordsFrequency
{
    public interface IWordsFrequencyDictionary
    {
        void AddWord(DictionaryItem item);
        IEnumerable<string> GetWordVariants(WordQuery query);
    }
}