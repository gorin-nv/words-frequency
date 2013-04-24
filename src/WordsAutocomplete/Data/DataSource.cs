using System;
using System.Collections.Generic;
using System.Linq;
using WordsAutocomplete.TextGateway;
using WordsFrequency;

namespace WordsAutocomplete.Data
{
    public class DataSource : IDataSource
    {
        private readonly ITextInputGateway _textInput;

        public DataSource(ITextInputGateway textInput)
        {
            _textInput = textInput;
        }

        public IEnumerable<DictionaryItem> GetDictionaryItems()
        {
            var dictionaryLength = GetDictionaryLength();
            return GetDictionary(dictionaryLength);
        }

        public IEnumerable<string> GetWordOpenings()
        {
            throw new NotImplementedException();
        }

        private uint GetDictionaryLength()
        {
            uint dictionaryLength;
            var dictionaryLengthRaw = _textInput.ReadString();
            if (uint.TryParse(dictionaryLengthRaw, out dictionaryLength) == false || dictionaryLength == 0)
            {
                throw new Exception(CreateErrorMessage("Количество слов в частотном словаре должно быть положительным числом", 0, dictionaryLengthRaw));
            }
            return dictionaryLength;
        }

        private IEnumerable<DictionaryItem> GetDictionary(uint dictionaryLength)
        {
            for (var i = 1u; i <= dictionaryLength; i++)
            {
                var dictionaryItemRaw = _textInput.ReadString();
                if (dictionaryItemRaw == null)
                    throw new Exception(CreateErrorMessage(
                        string.Format("Частостный словарь должен содержать {0} строк, но содержит {1}", dictionaryLength, i),
                        i + 1, null));

                var item = ConvertToDictionaryItem(dictionaryItemRaw);
                if(item == null)
                    throw new Exception(CreateErrorMessage(
                        "В строке частотного словаря должно быть слово и количество использований слова, разделенные пробелом",
                        i + 1, null));

                yield return item;
            }
        }

        private string CreateErrorMessage(string details, uint lineNumber, string line)
        {
            return string.Format("{0}, номер строки: {1}, строка: \"{2}\"", details, lineNumber, line);
        }

        private DictionaryItem ConvertToDictionaryItem(string dictionaryItemRaw)
        {
            var parts = dictionaryItemRaw.Split(new[] { ' ' });
            if (parts.Length != 2)
                return null;
            if (parts[0].All(Char.IsLetter) == false)
                return null;
            uint count;
            if (uint.TryParse(parts[1], out count) == false)
                return null;
            return new DictionaryItem(parts[0], count);
        }
    }
}