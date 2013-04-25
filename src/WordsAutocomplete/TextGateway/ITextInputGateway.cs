using System;

namespace WordsAutocomplete.TextGateway
{
    public interface ITextInputGateway : IDisposable
    {
        string ReadString();
    }
}