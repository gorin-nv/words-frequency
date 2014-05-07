using System.Collections.Generic;
using System.Linq;
using WordsFrequency.Impl;

namespace WordsFrequency.WordTree
{
    public abstract class TreeNode
    {
        private readonly List<LetterNode> _nodes = new List<LetterNode>();

        public IEnumerable<LetterNode> Nodes
        {
            get { return _nodes; }
        }

        public void AddWord(WordIterator wordIterator, int weight)
        {
            wordIterator.Next();
            var symbol = wordIterator.Current;
            var node = _nodes.FirstOrDefault(n => n.Symbol == symbol);
            if (node == null)
            {
                node = CreateChild(symbol);
                _nodes.Add(node);
            }
            node.TryUpVariantsWeight(weight);
            if (wordIterator.HasNext)
            {
                node.AddWord(wordIterator, weight);
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
                             ? node.FindNode(wordIterator)
                             : node;
        }

        protected abstract LetterNode CreateChild(char symbol);
    }
}