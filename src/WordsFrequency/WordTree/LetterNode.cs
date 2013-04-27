using System;

namespace WordsFrequency.WordTree
{
    public class LetterNode
    {
        private LetterNode _parent;

        public LetterNode(char symbol, LetterNode parent = null)
        {
            Symbol = symbol;
            Variants = new LetterVariants();
            _parent = parent;
        }

        public char Symbol { get; private set; }
        public LetterVariants Variants { get; private set; }
        public uint Weight { get; private set; }
        public bool IsWord { get; private set; }

        public string Word
        {
            get
            {
                if(_parent != null)
                {
                    return _parent.Word + Symbol;
                }
                return Symbol.ToString();
            }
        }

        public void DeclareWord()
        {
            if (IsWord)
                throw new Exception("дублирование слова");
            IsWord = true;
        }

        public void TryUpWeight(uint weight)
        {
            if (Weight < weight)
            {
                Weight = weight;
            }
        }
    }
}