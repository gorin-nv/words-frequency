using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using WordsAutocomplete.TextGateway;
using WordsFrequency;

namespace WordsAutocomplete.Tests
{
    [TestFixture]
    public class ProgramScenarioTests
    {
        private ProgramScenario _programScenario;
        private ITextInputGateway _textInputGateway;
        private ITextOutputGateway _textOutputGateway;
        private IWordsFrequencyDictionary _dictionary;

        [SetUp]
        public void SetUp()
        {
            _textInputGateway = Mock.Of<ITextInputGateway>();
            _textOutputGateway = Mock.Of<ITextOutputGateway>();
            _dictionary = Mock.Of<IWordsFrequencyDictionary>();
            _programScenario = new ProgramScenario(_textInputGateway, _textOutputGateway, _dictionary);
        }

        [Test]
        [Sequential]
        public void Should_fail_when_source_not_started_with_uint(
            [Values(
                "not integer string",
                "-3",
                "0")] string firstString)
        {
            Mock.Get(_textInputGateway)
                .Setup(src => src.ReadString())
                .Returns(firstString);

            Action execute = () => _programScenario.Execute();

            execute.ShouldThrow<Exception>();
        }

        [Test]
        [Sequential]
        public void Should_fail_when_source_dictionary_length_is_not_correct()
        {
            var source = new[] {"3", "a 1", "b 2"};
            var iter = 0;
            Mock.Get(_textInputGateway)
                .Setup(src => src.ReadString())
                .Returns(() => source[iter++]);

            Action execute = () => _programScenario.Execute();

            execute.ShouldThrow<Exception>();
        }

        [Test]
        public void Should_fail_when_dictionary_line_is_not_correct(
            [Values(
                new[] { "3", "a 1", "", "c 3"},
                new[] { "3", "a 1", "x", "c 3"},
                new[] { "3", "a 1", "x y", "c 3"},
                new[] { "3", "a 1", "12", "c 3"}
                )] string[] source)
        {
            var iter = 0;
            Mock.Get(_textInputGateway)
                .Setup(src => src.ReadString())
                .Returns(() => source[iter++]);

            Action execute = () => _programScenario.Execute();

            execute.ShouldThrow<Exception>();
        }

        [Test]
        public void Should_send_word_to_dictionary()
        {
            var source = new[] {"3", "a 1", "b 2", "c 3"};
            var iter = 0;
            Mock.Get(_textInputGateway)
                .Setup(src => src.ReadString())
                .Returns(() => source[iter++]);
            

            _programScenario.Execute();

            var dictionaryMock = Mock.Get(_dictionary);
            dictionaryMock.Verify(d => d.AddWord("a", 1));
            dictionaryMock.Verify(d => d.AddWord("b", 2));
            dictionaryMock.Verify(d => d.AddWord("c", 3));
        }
    }
}