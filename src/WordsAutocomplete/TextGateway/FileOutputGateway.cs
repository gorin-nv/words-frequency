using System.IO;

namespace WordsAutocomplete.TextGateway
{
    internal class FileOutputGateway : ITextOutputGateway
    {
        private readonly StreamWriter _writer;

        public FileOutputGateway(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            _writer = new StreamWriter(path);
        }

        public void WriteString(string text)
        {
            _writer.WriteLine(text);
        }

        public void Dispose()
        {
            _writer.Close();
        }
    }
}