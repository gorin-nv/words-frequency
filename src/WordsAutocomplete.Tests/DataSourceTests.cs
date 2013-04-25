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
            [Values("not integer string", "-3", "0")] string dictionaryLength)
        {
            SetupInputText(dictionaryLength);

            Action readDictionary = () => _dataSource.GetDictionaryItems().ToList();

            readDictionary.ShouldThrow<Exception>();
        }

        [Test]
        public void GetDictionaryItems_should_fail_when_source_dictionary_length_is_not_correct()
        {
            SetupInputText("3", "a 1", "b 2");

            Action readDictionary = () => _dataSource.GetDictionaryItems().ToList();

            readDictionary.ShouldThrow<Exception>();
        }

        [Test]
        [Sequential]
        public void GetDictionaryItems_should_fail_when_dictionary_line_is_not_correct(
            [Values("", "x", "x y", "12", "1 2", "a 2 ")] string incorrectLine)
        {
            SetupInputText("3", "a 1", incorrectLine, "c 3");

            Action readDictionary = () => _dataSource.GetDictionaryItems().ToList();

            readDictionary.ShouldThrow<Exception>();
        }

        [Test]
        public void GetDictionaryItems_should_return_dictionary_items()
        {
            SetupInputText("3", "a 1", "b 2", "c 3");

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
            [Values("not integer string", "-3", "0")] string wordsOpeningLength)
        {
            SetupInputText(wordsOpeningLength);

            Action readWords = () => _dataSource.GetWordOpenings().ToList();

            readWords.ShouldThrow<Exception>();
        }

        [Test]
        public void GetWordOpenings_should_fail_when_words_openings_length_is_not_correct()
        {
            SetupInputText("3", "a", "b");

            Action readWords = () => _dataSource.GetWordOpenings().ToList();

            readWords.ShouldThrow<Exception>();
        }

        [Test]
        [Sequential]
        public void GetWordOpenings_should_fail_when_word_opening_is_not_correct(
            [Values("", "1", "b2", " b ")] string incorrectWordOpening)
        {
            SetupInputText("3", "a", incorrectWordOpening, "c");

            Action readWords = () => _dataSource.GetWordOpenings().ToList();

            readWords.ShouldThrow<Exception>();
        }

        [Test]
        public void GetWordOpenings_should_return_word_openings()
        {
            SetupInputText("3", "a", "b", "c");

            var words = _dataSource.GetWordOpenings().ToList();

            words.ElementAt(0).Should().Be("a");
            words.ElementAt(1).Should().Be("b");
            words.ElementAt(2).Should().Be("c");
        }

        private void SetupInputText(params string[] source)
        {
            var iter = 0;
            Mock.Get(_textInput)
                .Setup(src => src.ReadString())
                .Returns(() => source[iter++]);
        }
    }
}