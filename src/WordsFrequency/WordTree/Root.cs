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
            var wordIterator = new WordIterator(word);
            Variants.AddWord(wordIterator, weight);
        }

        public LetterNode FindNodeForPrefix(string prefix)
        {
            if (string.IsNullOrEmpty(prefix))
                return null;
            var wordIterator = new WordIterator(prefix);
            return Variants.FindNode(wordIterator);
        }
    }
}