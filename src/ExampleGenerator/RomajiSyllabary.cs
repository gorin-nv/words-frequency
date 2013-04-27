using System.Collections.Generic;
using System.Linq;

namespace ExampleGenerator
{
    internal static class RomajiSyllabary
    {
        static RomajiSyllabary()
        {
            var consonants = new[]
                                 {
                                     "k", "g",
                                     "s", "z",
                                     "t", "d",
                                     "n",
                                     "h", "b", "p",
                                     "m",
                                     "r"
                                 };
            var vowels = new[] {"a", "i", "u", "e", "o"};
            var yBased = new[] {"ya", "yu", "yo"};
            var special = new[]
                              {
                                  "n", "wa", "wo",
                                  "shi", "sha", "shu", "sho",
                                  "chi", "tsu", "cha", "chu", "cho",
                                  "fu",
                                  "ji", "ja", "ju", "jo"
                              };
            var exclude = new[]
                              {
                                  "si", "sya", "syu", "syo",
                                  "ti", "tu", "tya", "tyu", "tyo",
                                  "hu",
                                  "zi", "zya", "zyu", "zyo",
                                  "di", "du", "dya", "dyu", "dyo"
                              };

            var values = new List<string>();
            values.AddRange(vowels);
            values.AddRange(yBased);
            values.AddRange(special);
            values.AddRange(
                from consonant in consonants
                from vowel in vowels
                select consonant + vowel);
            values.AddRange(
                from consonant in consonants
                from digraph in yBased
                select consonant + digraph);
            values.RemoveAll(exclude.Contains);

            Values = values.Distinct().ToArray();
        }

        public static string[] Values { get; private set; }
    }
}