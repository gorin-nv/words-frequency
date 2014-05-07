using System;
using System.Linq;

namespace WordsFrequency.Contract
{
    public class WordQuery
    {
        public WordQuery(string wordOpening, int maximumVarinatsCount)
        {
            if (!wordOpening.Any(char.IsLetter))
                throw new Exception("слово должно состоять только из букв");
            if (maximumVarinatsCount <= 0)
                throw new Exception("количество вариантов должно быть больше нуля");
            WordOpening = wordOpening;
            MaximumVarinatsCount = maximumVarinatsCount;
        }

        public string WordOpening { get; private set; }
        public int MaximumVarinatsCount { get; private set; }
    }
}