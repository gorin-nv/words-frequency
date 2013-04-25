using System.Collections.Generic;

namespace WordsFrequency.WordTree
{
    public class Root
    {
        public Root()
        {
            Variants = new LetterVariants();
        }

        public LetterVariants Variants { get; private set; }

        public void AddWord(string word, uint weight)
        {
            if (string.IsNullOrEmpty(word))
                return;
            Variants.AddWord(word.GetEnumerator(), weight);
        }
    }

    public class LetterNode
    {
        public LetterNode()
        {
            Weight = 0;
            Variants = new LetterVariants();
        }

        public uint Weight { get; private set; }
        public LetterVariants Variants { get; private set; }

        public void AddWordTail(IEnumerator<char> word, uint count)
        {
            if (word.MoveNext() == false)
                return;

            Weight += count;
            Variants.AddWord(word, count);
        }
    }
}