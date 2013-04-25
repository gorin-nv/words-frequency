using System;

namespace WordsAutocomplete.TextGateway
{
    public interface ITextOutputGateway : IDisposable
    {
        void WriteString(string text);
    }
}