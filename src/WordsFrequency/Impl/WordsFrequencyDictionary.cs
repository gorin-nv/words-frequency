using System.Collections.Generic;
using System.Linq;
using WordsFrequency.Contract;
using WordsFrequency.WordTree;

namespace WordsFrequency.Impl
{
    public class WordsFrequencyDictionary : IWordsFrequencyDictionary
    {
        private readonly RootNode _rootNode;

        public WordsFrequencyDictionary()
        {
            _rootNode = new RootNode();
        }

        public void AddWord(DictionaryItem item)
        {
            if (string.IsNullOrEmpty(item.Word))
                return;
            var wordIterator = new WordIterator(item.Word);
            _rootNode.AddWord(wordIterator, item.Count);
        }

        public IEnumerable<string> GetWordVariants(WordQuery query)
        {
            if (string.IsNullOrEmpty(query.WordOpening))
                return null;
            var wordIterator = new WordIterator(query.WordOpening);
            var prefixNode = _rootNode.FindNode(wordIterator);
            if (prefixNode == null)
            {
                return new string[0];
            }

            var storage = new NodeStorage(query.MaximumVarinatsCount);
            if(prefixNode.IsWord)
            {
                storage.Add(prefixNode);
            }
            var prevNodes = new List<LetterNode> {prefixNode};
            while (prevNodes.Count > 0)
            {
                var currentNodes = prevNodes
                    .SelectMany(n => n.Nodes)
                    .ToList();

                var wordNodes = currentNodes
                    .Where(n => n.IsWord)
                    .OrderByDescending(n => n.WordWeight)
                    .Take((int)query.MaximumVarinatsCount);
                foreach (var node in wordNodes)
                {
                    storage.Add(node);
                }

                prevNodes = currentNodes
                    .OrderByDescending(n => n.VariantsWeight)
                    .Take((int)query.MaximumVarinatsCount)
                    .ToList();
            }
            return storage.Words
                .OrderByDescending(n => n.WordWeight)
                .Select(n => n.Word);
        }
    }
}