using System.Collections.Generic;
using System.Linq;
using WordsFrequency.WordTree;

namespace WordsFrequency.Impl
{
    public class NodeStorage
    {
        private readonly uint _limit;
        private readonly List<LetterNode> _nodes;

        public NodeStorage(uint limit)
        {
            _limit = limit;
            _nodes = new List<LetterNode>((int) _limit);
        }

        public void Add(LetterNode node)
        {
            if(_nodes.Count < _limit)
            {
                _nodes.Add(node);
                return;
            }
            var minimalNode = _nodes.OrderBy(n => n.WordWeight).First();
            if (node.WordWeight > minimalNode.WordWeight)
            {
                _nodes.Remove(minimalNode);
                _nodes.Add(node);
            }
        }

        public IEnumerable<LetterNode> Words
        {
            get { return _nodes; }
        }
    }
}