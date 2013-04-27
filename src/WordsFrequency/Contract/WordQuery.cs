namespace WordsFrequency.Contract
{
    public class WordQuery
    {
        public WordQuery(string wordOpening, uint maximumVarinatsCount)
        {
            WordOpening = wordOpening;
            MaximumVarinatsCount = maximumVarinatsCount;
        }

        public string WordOpening { get; private set; }
        public uint MaximumVarinatsCount { get; private set; }
    }
}