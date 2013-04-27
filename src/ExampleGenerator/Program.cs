using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ExampleGenerator
{
    class Program
    {
        static void Main()
        {
            var fileName = GetFullFileName("input.txt");
            if(File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            using (var writer = new StreamWriter(fileName))
            {
                const int dictionaryItemsCount = 10000;
                writer.WriteLine(dictionaryItemsCount);
                foreach (var word in GetWords(dictionaryItemsCount, 15))
                {
                    var dictionaryItem = string.Format("{0} {1}", word, word.Length);
                    writer.WriteLine(dictionaryItem);
                }

                const int wordOpeningsCount = 15000;
                writer.WriteLine(wordOpeningsCount);
                foreach (var word in GetWords(wordOpeningsCount, 15))
                {
                    writer.WriteLine(word);
                }
            }
        }

        private static IEnumerable<string> GetWords(int count, int maxLength)
        {
            var random = new Random(Guid.NewGuid().GetHashCode());
            var returnedWordsCount = 0;
            var returnedWords = new List<string>(count);
            while (returnedWordsCount < count)
            {
                var word = GenerateWord(random, maxLength);
                if(returnedWords.Contains(word))
                    continue;
                returnedWords.Add(word);
                returnedWordsCount++;
                yield return word;
            }
        }

        private static string GenerateWord(Random random, int maxLength)
        {
            const string chars = "aiueokstnhmrgjdbpyw";
            var builder = new StringBuilder();
            var length = random.Next(1, maxLength + 1);
            for (var i = 0; i < length; i++)
            {
                var symbolIndex = random.Next(0, chars.Length);
                var symbol = chars[symbolIndex];
                builder.Append(symbol);
            }
            return builder.ToString();
        }

        private static string GetFullFileName(string fileName)
        {
            var consoleName = Environment.GetCommandLineArgs()[0];
            var dir = Path.GetDirectoryName(consoleName);
            var filename = Path.Combine(dir, fileName);
            return filename;
        }
    }
}