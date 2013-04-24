using System.Collections.Generic;
using WordsAutocomplete.TextGateway;

namespace WordsAutocomplete.Data
{
    public class DataDestination : IDataDestination
    {
        private readonly ITextOutputGateway _textOutput;
        private bool _hasPreviousWritting;

        public DataDestination(ITextOutputGateway textOutput)
        {
            _textOutput = textOutput;
            _hasPreviousWritting = false;
        }

        public void WriteWords(IEnumerable<string> words)
        {
            if (words == null)
                return;
            var enumerator = words.GetEnumerator();
            if (enumerator.MoveNext() == false)
                return;

            if (_hasPreviousWritting)
            {
                _textOutput.WriteString(string.Empty);
            }
            else
            {
                _hasPreviousWritting = true;
            }

            do
            {
                _textOutput.WriteString(enumerator.Current);
            } while (enumerator.MoveNext());
        }
    }
}