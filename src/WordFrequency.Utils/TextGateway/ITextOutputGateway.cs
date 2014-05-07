using System;

namespace WordFrequency.Utils.TextGateway
{
    public interface ITextOutputGateway : IDisposable
    {
        void WriteString(string text);
    }
}