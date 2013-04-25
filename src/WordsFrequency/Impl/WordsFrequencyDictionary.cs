using System.Collections.Generic;
using WordsFrequency.Contract;
using WordsFrequency.WordTree;

namespace WordsFrequency.Impl
{
    public class WordsFrequencyDictionary : IWordsFrequencyDictionary
    {
        private readonly LetterVariants _letterVariants;

        public WordsFrequencyDictionary()
        {
            _letterVariants = new LetterVariants();
        }

        public void AddWord(DictionaryItem item)
        {
            _letterVariants.AddWord(item.Word.GetEnumerator(), item.Count);
        }

        public IEnumerable<string> GetWordVariants(WordQuery query)
        {
            throw new System.NotImplementedException();
        }
    }
}