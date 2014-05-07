using System;

namespace WordFrequency.Utils.TextGateway
{
    public interface ITextInputGateway : IDisposable
    {
        string ReadString();
    }
}