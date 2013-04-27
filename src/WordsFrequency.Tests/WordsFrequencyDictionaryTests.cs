using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using WordsFrequency.Contract;
using WordsFrequency.Impl;

namespace WordsFrequency.Tests
{
    [TestFixture]
    public class WordsFrequencyDictionaryTests
    {
        [Test]
        public void Should_return_word_variants()
        {
            var dictionary = new WordsFrequencyDictionary();

            dictionary.AddWord(new DictionaryItem("kare", 10));
            dictionary.AddWord(new DictionaryItem("kanojo", 20));
            dictionary.AddWord(new DictionaryItem("karetachi", 1));
            dictionary.AddWord(new DictionaryItem("korosu", 7));
            dictionary.AddWord(new DictionaryItem("sakura", 3));

            var variantsK = dictionary.GetWordVariants(new WordQuery("k", 10));
            variantsK.Should().HaveCount(4);
            //variantsK.ElementAt(0).Should().Be("kanojo");
            //variantsK.ElementAt(1).Should().Be("kare");
            //variantsK.ElementAt(2).Should().Be("korosu");
            //variantsK.ElementAt(3).Should().Be("karatachi");

            var variantsKa = dictionary.GetWordVariants(new WordQuery("ka", 10));
            variantsKa.Should().HaveCount(3);
            //variantsKa.ElementAt(0).Should().Be("kanojo");
            //variantsKa.ElementAt(1).Should().Be("kare");
            //variantsKa.ElementAt(2).Should().Be("karatachi");

            var variantsKar = dictionary.GetWordVariants(new WordQuery("kar", 10));
            variantsKar.Should().HaveCount(2);
            //variantsKar.ElementAt(0).Should().Be("kare");
            //variantsKar.ElementAt(1).Should().Be("karatachi");
        }
    }
}