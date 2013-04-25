using System.Collections.Generic;

namespace WordsFrequency.WordTree
{
    public class LetterNode
    {
        public LetterNode()
        {
            Weight = 0;
            Variants = new LetterVariants();
        }

        public uint Weight { get; private set; }
        public LetterVariants Variants { get; private set; }

        public void ChangeWeight(uint diff)
        {
            Weight += diff;
        }

        public void AddWord(IEnumerator<char> word, uint count)
        {
            ChangeWeight(count);
            Variants.AddWord(word, count);
        }
    }
}