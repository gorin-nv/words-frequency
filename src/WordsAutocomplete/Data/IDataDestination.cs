using System.Collections.Generic;

namespace WordsAutocomplete.Data
{
    public interface IDataDestination
    {
        void WriteWords(IEnumerable<string> words);
    }
}