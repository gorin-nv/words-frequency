using System;
using System.Collections.Generic;

namespace WordsFrequency.WordTree
{
    public class LetterVariants
    {
        private readonly Dictionary<char, LetterNode> _nodes = new Dictionary<char, LetterNode>();

        public LetterNode this[char key]
        {
            get { return _nodes[key]; }
        }

        public bool TryGetNode(char key, out LetterNode node)
        {
            return _nodes.TryGetValue(key, out node);
        }

        public bool ContainsKey(char key)
        {
            return _nodes.ContainsKey(key);
        }

        public bool IsEmpty
        {
            get { return _nodes.Count == 0; }
        }

        public void AddWord(WordIterator wordIterator, uint count)
        {
            wordIterator.Next();
            var key = wordIterator.Current;
            LetterNode node;
            if (_nodes.TryGetValue(key, out node) == false)
            {
                node = new LetterNode();
                _nodes[key] = node;
            }
            node.AddWeight(count);
            if (wordIterator.HasNext)
            {
                node.Variants.AddWord(wordIterator, count);
            }
        }

        public LetterNode FindNode(WordIterator wordIterator)
        {
            wordIterator.Next();
            var key = wordIterator.Current;
            LetterNode node;
            if (_nodes.TryGetValue(key, out node) == false)
            {
                return null;
            }
            if (wordIterator.HasNext == false)
            {
                return node;
            }
            return node.Variants.FindNode(wordIterator);
        }
    }
}