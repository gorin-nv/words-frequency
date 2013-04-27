using System;

namespace WordsFrequency.WordTree
{
    public class LetterNode
    {
        public LetterNode(char symbol)
        {
            Symbol = symbol;
            Variants = new LetterVariants();
        }

        public char Symbol { get; private set; }
        public LetterVariants Variants { get; private set; }
        public uint Weight { get; private set; }
        public bool IsWord { get; private set; }

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