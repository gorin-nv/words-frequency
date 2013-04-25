using System;
using System.Collections.Generic;
using WordsFrequency;

namespace WordsAutocomplete.Data
{
    public interface IDataSource: IDisposable
    {
        IEnumerable<DictionaryItem> GetDictionaryItems();
        IEnumerable<string> GetWordOpenings();
    }
}