using System.Collections.Generic;
using System.Linq;
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
                var nodes = prevNodes
                    .SelectMany(n => n.Variants.Nodes)
                    .OrderByDescending(n => n.VariantsWeight)
                    .Take((int) query.MaximumVarinatsCount)
                    .ToList();
                var wordNodes = prevNodes
                    .SelectMany(n => n.Variants.Nodes)
                    .Where(n => n.IsWord)
                    .OrderByDescending(n => n.WordWeight)
                    .Take((int)query.MaximumVarinatsCount);

                foreach (var node in wordNodes)
                {
                    storage.Add(node);
                }
                prevNodes = nodes;
            }
            return storage.Words
                .OrderByDescending(n => n.WordWeight)
                .Select(n => n.Word);
        }
    }
}