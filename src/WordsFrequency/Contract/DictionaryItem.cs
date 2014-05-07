namespace WordsFrequency.Contract
{
    public class DictionaryItem
    {
        public DictionaryItem(string word, int count)
        {
            Word = word;
            Count = count;
        }

        public string Word { get; private set; }
        public int Count { get; private set; }
    }
}