using System;
using System.Collections.Generic;

namespace WordsFrequency.WordTree
{
    public class LetterNode: TreeNode
    {
        private readonly LetterNode _parent;

        public LetterNode(char symbol, LetterNode parent = null)
        {
            Symbol = symbol;
            _parent = parent;
        }

        public char Symbol { get; private set; }
        public int VariantsWeight { get; private set; }
        public int WordWeight { get; private set; }

        public bool IsWord
        {
            get { return WordWeight > 0; }
        }

        public string Word
        {
            get
            {
                var current = this;
                var chars = new List<char>();
                while (current != null)
                {
                    chars.Add(current.Symbol);
                    current = current._parent;
                }
                chars.Reverse();
                return new string(chars.ToArray());
            }
        }

        public void DeclareWord(int selfWeight)
        {
            if (IsWord)
                throw new Exception("дублирование слова");
            WordWeight = selfWeight;
        }

        public void TryUpVariantsWeight(int weight)
        {
            if (VariantsWeight < weight)
            {
                VariantsWeight = weight;
            }
        }

        protected override LetterNode CreateChild(char symbol)
        {
            return new LetterNode(symbol, this);
        }
    }
}