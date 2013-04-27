using System.Collections.Generic;
using System.Linq;

namespace WordsFrequency.WordTree
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
            var minimalNode = _nodes.OrderBy(n => n.Weight).First();
            if(node.Weight > minimalNode.Weight)
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