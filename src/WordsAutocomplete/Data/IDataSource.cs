using System.Collections.Generic;
using WordsFrequency;

namespace WordsAutocomplete.Data
{
    public interface IDataSource
    {
        IEnumerable<DictionaryItem> GetDictionaryItems();
        IEnumerable<string> GetWordOpenings();
    }
}