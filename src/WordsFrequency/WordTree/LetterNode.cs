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
        public uint VariantsWeight { get; private set; }
        public uint WordWeight { get; private set; }

        public bool IsWord
        {
            get { return WordWeight > 0; }
        }

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

        public void DeclareWord(uint selfWeight)
        {
            if (IsWord)
                throw new Exception("дублирование слова");
            WordWeight = selfWeight;
        }

        public void TryUpVariantsWeight(uint weight)
        {
            if (VariantsWeight < weight)
            {
                VariantsWeight = weight;
            }
        }
    }
}