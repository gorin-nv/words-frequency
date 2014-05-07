using System;
using System.Collections.Generic;

namespace WordFrequency.Utils.Data
{
    public interface IDataDestination : IDisposable
    {
        void WriteWords(IEnumerable<string> words);
    }
}