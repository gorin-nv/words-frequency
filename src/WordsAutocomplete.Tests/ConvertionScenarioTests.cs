using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using WordFrequency.Utils.Data;
using WordsFrequency.Contract;

namespace WordsAutocomplete.Tests
{
    [TestFixture]
    public class ConvertionScenarioTests
    {
        private ConvertionScenario _scenario;
        private IDataSource _dataSource;
        private IDataDestination _dataDestination;
        private IWordsFrequencyDictionary _dictionary;

        [SetUp]
        public void SetUp()
        {
            _dataSource = Mock.Of<IDataSource>();
            _dataDestination = Mock.Of<IDataDestination>();
            _dictionary = Mock.Of<IWordsFrequencyDictionary>();
            _scenario = new ConvertionScenario();
        }

        [Test]
        public void Execute_should_send_dictionary_items_to_dictionary()
        {
            var items = new[]
            {
                new DictionaryItem("a", 1),
                new DictionaryItem("b", 2),
                new DictionaryItem("c", 3)
            };
            Mock.Get(_dataSource)
                .Setup(src => src.GetDictionaryItems())
                .Returns(items);

            _scenario.Execute(() => _dataSource, () => _dataDestination, _dictionary);

            var dicrionaryMock = Mock.Get(_dictionary);
            dicrionaryMock.Verify(d => d.AddWord(items[0]));
            dicrionaryMock.Verify(d => d.AddWord(items[1]));
            dicrionaryMock.Verify(d => d.AddWord(items[2]));
        }

        [Test]
        public void Execute_should_send_word_variants_to_output()
        {
            var wordOpenings = new[] {"a", "b"};
            var queries = new[] {new WordQuery("a", 10), new WordQuery("b", 10)};
            var wordVariants = new IEnumerable<string>[] {new string[0], new string[0]};

            Mock.Get(_dataSource)
                .Setup(src => src.GetWordOpenings())
                .Returns(wordOpenings);

            var dictionaryMock = Mock.Get(_dictionary);
            dictionaryMock
                .Setup(d => d.GetWordVariants(queries[0]))
                .Returns(wordVariants[0]);
            dictionaryMock
                .Setup(d => d.GetWordVariants(queries[1]))
                .Returns(wordVariants[1]);

            _scenario.Execute(() => _dataSource, () => _dataDestination, _dictionary);

            Func<WordQuery, WordQuery, bool> wordQueryEquals = (query1, query2) =>
                                                               query1.WordOpening == query2.WordOpening &&
                                                               query1.MaximumVarinatsCount == query2.MaximumVarinatsCount;
            dictionaryMock.Verify(d => d.GetWordVariants(It.Is<WordQuery>(query => wordQueryEquals(query, queries[0]))));
            dictionaryMock.Verify(d => d.GetWordVariants(It.Is<WordQuery>(query => wordQueryEquals(query, queries[1]))));

            var destinationMock = Mock.Get(_dataDestination);
            destinationMock.Verify(dst => dst.WriteWords(wordVariants[0]));
            destinationMock.Verify(dst => dst.WriteWords(wordVariants[1]));
        }

        [Test]
        public void Execute_should_dispose_sources()
        {
            _scenario.Execute(() => _dataSource, () => _dataDestination, _dictionary);

            Mock.Get(_dataSource)
                .Verify(src => src.Dispose());
            Mock.Get(_dataDestination)
                .Verify(dst => dst.Dispose());
        }
    }
}