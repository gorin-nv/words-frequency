using System.Collections.Generic;
using System.Linq;
using WordsFrequency.WordTree;

namespace WordsFrequency.Impl
{
    public class LimittedNodeStorage
    {
        private readonly int _limit;
        private readonly SortedList<int, LetterNode> _nodes;

        public LimittedNodeStorage(int limit)
        {
            _limit = limit;
            _nodes = new SortedList<int, LetterNode>(_limit + 1);
        }

        public void Add(LetterNode node)
        {
            _nodes.Add(-(int)node.WordWeight, node);
            
            if(_nodes.Count > _limit)
            {
                _nodes.RemoveAt(_limit);
            }
        }

        public IEnumerable<LetterNode> Words
        {
            get { return _nodes.Select(x => x.Value); }
        }
    }
}