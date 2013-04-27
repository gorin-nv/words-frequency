﻿using System.Collections.Generic;
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

        public void AddWord(WordIterator wordIterator, uint weight, LetterNode parent = null)
        {
            wordIterator.Next();
            var symbol = wordIterator.Current;
            var node = _nodes.FirstOrDefault(n => n.Symbol == symbol);
            if (node == null)
            {
                node = new LetterNode(symbol, parent);
                _nodes.Add(node);
            }
            node.TryUpVariantsWeight(weight);
            if (wordIterator.HasNext)
            {
                node.Variants.AddWord(wordIterator, weight, node);
            }
            else
            {
                node.DeclareWord(weight);
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