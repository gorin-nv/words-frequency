using System.Collections.Generic;
using WordsFrequency.Contract;
using WordsFrequency.WordTree;

namespace WordsFrequency.Impl
{
    public class WordsFrequencyDictionary : IWordsFrequencyDictionary
    {
        private readonly Root _root;

        public WordsFrequencyDictionary()
        {
            _root = new Root();
        }

        public void AddWord(DictionaryItem item)
        {
            _root.AddWord(item.Word, item.Count);
        }

        public IEnumerable<string> GetWordVariants(WordQuery query)
        {
            var prefixNode = _root.FindNodeForPrefix(query.WordOpening);
            throw new System.NotImplementedException();
        }
    }
}