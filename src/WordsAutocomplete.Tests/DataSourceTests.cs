using System;
using System.Linq;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using WordsAutocomplete.Data;
using WordsAutocomplete.TextGateway;

namespace WordsAutocomplete.Tests
{
    [TestFixture]
    public class DataSourceTests
    {
        private DataSource _dataSource;
        private ITextInputGateway _textInput;

        [SetUp]
        public void SetUp()
        {
            _textInput = Mock.Of<ITextInputGateway>();
            _dataSource = new DataSource(_textInput);
        }

        [Test]
        [Sequential]
        public void GetDictionaryItems_should_fail_when_dictionary_items_not_started_with_uint(
            [Values(
                "not integer string",
                "-3",
                "0")] string dictionaryLength)
        {
            Mock.Get(_textInput)
                .Setup(src => src.ReadString())
                .Returns(dictionaryLength);

            Action execute = () => _dataSource.GetDictionaryItems().ToList();

            execute.ShouldThrow<Exception>();
        }

        [Test]
        public void GetDictionaryItems_should_fail_when_source_dictionary_length_is_not_correct()
        {
            var source = new[] {"3", "a 1", "b 2"};
            var iter = 0;
            Mock.Get(_textInput)
                .Setup(src => src.ReadString())
                .Returns(() => source[iter++]);

            Action execute = () => _dataSource.GetDictionaryItems().ToList();

            execute.ShouldThrow<Exception>();
        }

        [Test]
        [Sequential]
        public void GetDictionaryItems_should_fail_when_dictionary_line_is_not_correct(
            [Values(
                new[] { "3", "a 1", "", "c 3"},
                new[] { "3", "a 1", "x", "c 3"},
                new[] { "3", "a 1", "x y", "c 3"},
                new[] { "3", "a 1", "12", "c 3"},
                new[] { "3", "a 1", "1 2", "c 3"},
                new[] { "3", "a 1", "a 2 ", "c 3"}
                )] string[] source)
        {
            var iter = 0;
            Mock.Get(_textInput)
                .Setup(src => src.ReadString())
                .Returns(() => source[iter++]);

            Action execute = () => _dataSource.GetDictionaryItems().ToList();

            execute.ShouldThrow<Exception>();
        }

        [Test]
        public void GetDictionaryItems_should_return_dictionary_items()
        {
            var source = new[] {"3", "a 1", "b 2", "c 3"};
            var iter = 0;
            Mock.Get(_textInput)
                .Setup(src => src.ReadString())
                .Returns(() => source[iter++]);

            var dictionaryItems = _dataSource.GetDictionaryItems().ToList();

            dictionaryItems.ElementAt(0).Word.Should().Be("a");
            dictionaryItems.ElementAt(0).Count.Should().Be(1);
            dictionaryItems.ElementAt(1).Word.Should().Be("b");
            dictionaryItems.ElementAt(1).Count.Should().Be(2);
            dictionaryItems.ElementAt(2).Word.Should().Be("c");
            dictionaryItems.ElementAt(2).Count.Should().Be(3);
        }

        [Test]
        [Sequential]
        public void GetWordOpenings_should_fail_when_words_opening_not_started_with_uint(
            [Values(
                "not integer string",
                "-3",
                "0")] string wordsOpeningLength)
        {
            var source = new[] {"3", "a 1", "b 2", "c 3", wordsOpeningLength};
            var iter = 0;
            Mock.Get(_textInput)
                .Setup(src => src.ReadString())
                .Returns(() => source[iter++]);
            _dataSource.GetDictionaryItems().ToList();

            Action execute = () => _dataSource.GetWordOpenings().ToList();

            execute.ShouldThrow<Exception>();
        }

        [Test]
        public void GetWordOpenings_should_fail_when_words_openings_length_is_not_correct()
        {
            var source = new[] { "3", "a 1", "b 2", "c 3", "2", "a" };
            var iter = 0;
            Mock.Get(_textInput)
                .Setup(src => src.ReadString())
                .Returns(() => source[iter++]);
            _dataSource.GetDictionaryItems().ToList();

            Action execute = () => _dataSource.GetWordOpenings().ToList();

            execute.ShouldThrow<Exception>();
        }

        [Test]
        [Sequential]
        public void GetWordOpenings_should_fail_when_word_opening_is_not_correct()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void GetWordOpenings_should_return_word_openings()
        {
            throw new NotImplementedException();
        }
    }
}