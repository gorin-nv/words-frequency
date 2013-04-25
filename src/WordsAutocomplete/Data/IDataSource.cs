using System;
using System.Collections.Generic;
using WordsFrequency;
using WordsFrequency.Contract;

namespace WordsAutocomplete.Data
{
    public interface IDataSource: IDisposable
    {
        IEnumerable<DictionaryItem> GetDictionaryItems();
        IEnumerable<string> GetWordOpenings();
    }
}