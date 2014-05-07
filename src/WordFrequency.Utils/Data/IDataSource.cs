using System;
using System.Collections.Generic;
using WordsFrequency.Contract;

namespace WordFrequency.Utils.Data
{
    public interface IDataSource: IDisposable
    {
        IEnumerable<DictionaryItem> GetDictionaryItems();
        IEnumerable<string> GetWordOpenings();
    }
}