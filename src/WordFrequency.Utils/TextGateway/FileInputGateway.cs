using System;
using System.IO;

namespace WordFrequency.Utils.TextGateway
{
    public class FileInputGateway : ITextInputGateway
    {
        private readonly StreamReader _reader;

        public FileInputGateway(string path)
        {
            if (File.Exists(path) == false)
                throw new Exception(string.Format("файл {0} не найден", path));
            _reader = new StreamReader(path);
        }

        public string ReadString()
        {
            return _reader.ReadLine();
        }

        public void Dispose()
        {
            _reader.Dispose();
        }
    }
}