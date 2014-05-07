using System;
using System.Collections.Generic;
using WordFrequency.Utils.TextGateway;

namespace WordFrequency.Utils.Data
{
    public class DataDestination : IDataDestination
    {
        private readonly Lazy<ITextOutputGateway> _textOutputProxy;
        private bool _hasPreviousWritting;

        public DataDestination(Lazy<ITextOutputGateway> textOutputProxy)
        {
            _textOutputProxy = textOutputProxy;
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
                _textOutputProxy.Value.WriteString(string.Empty);
            }
            else
            {
                _hasPreviousWritting = true;
            }

            do
            {
                _textOutputProxy.Value.WriteString(enumerator.Current);
            } while (enumerator.MoveNext());
        }

        public void Dispose()
        {
            if (_textOutputProxy.IsValueCreated)
            {
                _textOutputProxy.Value.Dispose();
            }
        }
    }
}