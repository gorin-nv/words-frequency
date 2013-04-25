using System;
using Moq;
using NUnit.Framework;
using WordsAutocomplete.Data;
using WordsAutocomplete.TextGateway;

namespace WordsAutocomplete.Tests
{
    [TestFixture]
    public class DataDestinationTests
    {
        private DataDestination _dataDestination;
        private ITextOutputGateway _textOutput;
        private Lazy<ITextOutputGateway> _textOutputProxy;

        [SetUp]
        public void SetUp()
        {
            _textOutput = Mock.Of<ITextOutputGateway>();
            _textOutputProxy = new Lazy<ITextOutputGateway>(() => _textOutput);
            _dataDestination = new DataDestination(_textOutputProxy);
        }

        [Test]
        public void WriteWords_should_write_each_text_in_new_line()
        {
            var words = new[] {"a", "b", "c"};

            _dataDestination.WriteWords(words);

            var textOutputMock = Mock.Get(_textOutput);
            textOutputMock.Verify(x => x.WriteString("a"));
            textOutputMock.Verify(x => x.WriteString("b"));
            textOutputMock.Verify(x => x.WriteString("c"));
        }

        [Test]
        [Sequential]
        public void WriteWords_should_not_write_when_words_is_null_or_empty(
            [Values(new string[0], (string[])null)] string[] words)
        {
            _dataDestination.WriteWords(words);

            Mock.Get(_textOutput)
                .Verify(x => x.WriteString(It.IsAny<string>()), Times.Never());
        }

        [Test]
        public void WriteWords_should_add_empty_string_before_new_words_portion()
        {
            var words = new[] { "a", "b", "c" };
            _dataDestination.WriteWords(words);
            
            var newWordsPortion = new[] { "d", "e", "f" };
            _dataDestination.WriteWords(newWordsPortion);

            var textOutputMock = Mock.Get(_textOutput);
            textOutputMock.Verify(x => x.WriteString("a"));
            textOutputMock.Verify(x => x.WriteString("b"));
            textOutputMock.Verify(x => x.WriteString("c"));
            textOutputMock.Verify(x => x.WriteString(string.Empty));
            textOutputMock.Verify(x => x.WriteString("d"));
            textOutputMock.Verify(x => x.WriteString("e"));
            textOutputMock.Verify(x => x.WriteString("f"));
        }

        [Test]
        public void Dispose_should_free_output_gateway()
        {
            var source = _textOutputProxy.Value;

            _dataDestination.Dispose();

            Mock.Get(_textOutput)
                .Verify(x => x.Dispose());
        }
    }
}