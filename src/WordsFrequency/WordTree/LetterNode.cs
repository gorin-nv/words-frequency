namespace WordsFrequency.WordTree
{
    public class LetterNode
    {
        public LetterNode()
        {
            Variants = new LetterVariants();
        }

        public uint Weight { get; private set; }
        public LetterVariants Variants { get; private set; }

        public void AddWeight(uint weight)
        {
            Weight += weight;
        }
    }
}