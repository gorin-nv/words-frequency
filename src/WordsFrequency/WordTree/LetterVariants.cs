using System;
using System.Collections.Generic;
using System.Linq;

namespace WordsFrequency.WordTree
{
    public class LetterVariants
    {
        private readonly List<LetterNode> _nodes = new List<LetterNode>();

        public IEnumerable<LetterNode> Nodes
        {
            get { return _nodes; }
        }

        public void AddWord(WordIterator wordIterator, uint weight)
        {
            wordIterator.Next();
            var symbol = wordIterator.Current;
            var node = _nodes.FirstOrDefault(n => n.Symbol == symbol);
            if (node == null)
            {
                node = new LetterNode(symbol);
                _nodes.Add(node);
            }
            node.TryUpWeight(weight);
            if (wordIterator.HasNext)
            {
                node.Variants.AddWord(wordIterator, weight);
            }
            else
            {
                node.DeclareWord();
            }
        }

        public LetterNode FindNode(WordIterator wordIterator)
        {
            wordIterator.Next();
            var key = wordIterator.Current;
            var node = _nodes.FirstOrDefault(n => n.Symbol == key);
            return node == null
                       ? null
                       : wordIterator.HasNext
                             ? node.Variants.FindNode(wordIterator)
                             : node;
        }
    }
}