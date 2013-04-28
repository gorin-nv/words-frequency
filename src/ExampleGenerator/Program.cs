using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
                const int maxWordLength = 15;

                const int dictionaryItemsCount = 10000;
                writer.WriteLine(dictionaryItemsCount);
                var random = new Random(Guid.NewGuid().GetHashCode());
                foreach (var word in GetWords(dictionaryItemsCount, maxWordLength, GenerateWord, random))
                {
                    var dictionaryItem = string.Format("{0} {1}", word, random.Next(1, maxWordLength+1));
                    writer.WriteLine(dictionaryItem);
                }

                const int wordOpeningsCount = 15000;
                writer.WriteLine(wordOpeningsCount);
                foreach (var word in GetWords(wordOpeningsCount, maxWordLength, GenerateWordOpening, random))
                {
                    writer.WriteLine(word);
                }
            }
        }

        private static IEnumerable<string> GetWords(int count, int maxLength, Func<Random, int, string> exampleFactory, Random random)
        {
            var returnedWordsCount = 0;
            var returnedWords = new List<string>(count);
            while (returnedWordsCount < count)
            {
                var word = exampleFactory(random, maxLength);
                if(returnedWords.Contains(word))
                    continue;
                returnedWords.Add(word);
                returnedWordsCount++;
                yield return word;
            }
        }

        private static string GenerateWord(Random random, int maxLength)
        {
            var builder = new StringBuilder();
            var lengthLeft = random.Next(1, maxLength + 1);
            do
            {
                var availibleSyllables = RomajiSyllabary.Values.Where(v => v.Length <= lengthLeft).ToList();
                var syllableIndex = random.Next(0, availibleSyllables.Count());
                var syllable = availibleSyllables[syllableIndex];
                if (syllable.Length > lengthLeft)
                    continue;
                builder.Append(syllable);
                lengthLeft -= syllable.Length;

            } while (lengthLeft > 0);
            return builder.ToString();
        }

        private static string GenerateWordOpening(Random random, int maxLength)
        {
            var word = GenerateWord(random, maxLength);
            var partSize = random.Next(1, word.Length);
            return word.Substring(0, partSize);
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