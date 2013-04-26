using System;

namespace WordsFrequency.WordTree
{
    public class WordIterator
    {
        private readonly string _word;
        private int _current;

        public WordIterator(string word)
        {
            _word = word;
            _current = -1;
        }

        public bool HasNext
        {
            get { return _current + 1 < _word.Length; }
        }

        public char Current
        {
            get
            {
                if (_current == -1)
                    throw new Exception("ошибка обхода слова");
                return _word[_current];
            }
        }

        public void Next()
        {
            if (HasNext)
            {
                _current += 1;
            }
        }
    }
}