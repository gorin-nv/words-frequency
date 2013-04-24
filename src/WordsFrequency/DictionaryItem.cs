namespace WordsFrequency
{
    public class DictionaryItem
    {
        public DictionaryItem(string word, uint count)
        {
            Word = word;
            Count = count;
        }

        public string Word { get; private set; }
        public uint Count { get; private set; }
    }
}