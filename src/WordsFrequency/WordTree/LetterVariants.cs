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

        public void AddWord(IEnumerator<char> word, uint count)
        {
            if(word.MoveNext() == false)
                return;
            var key = word.Current;
            LetterNode node;
            if (_nodes.TryGetValue(key, out node) == false)
            {
                node = new LetterNode();
                _nodes[key] = node;
            }
            node.AddWord(word, count);
        }
    }
}