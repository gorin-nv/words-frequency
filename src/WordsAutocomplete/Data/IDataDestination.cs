using System;
using System.Collections.Generic;

namespace WordsAutocomplete.Data
{
    public interface IDataDestination : IDisposable
    {
        void WriteWords(IEnumerable<string> words);
    }
}