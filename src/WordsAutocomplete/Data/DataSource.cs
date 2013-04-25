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
        private int _lineNumber;

        public DataSource(ITextInputGateway textInput)
        {
            _textInput = textInput;
            _lineNumber = -1;
        }

        public IEnumerable<DictionaryItem> GetDictionaryItems()
        {
            var dictionaryLength = GetLength("количество слов в частотном словаре");
            return GetItems(dictionaryLength, ConvertToDictionaryItem, "частостный словарь");
        }

        public IEnumerable<string> GetWordOpenings()
        {
            var wordOpeningsLength = GetLength("количество начал слов");
            return GetItems(wordOpeningsLength, ConvertToWordOpening, "список начал слов");
        }

        private string ReadString()
        {
            _lineNumber += 1;
            return _textInput.ReadString();
        }

        private uint GetLength(string valueDescription)
        {
            uint length;
            var lengthRaw = ReadString();
            if (uint.TryParse(lengthRaw, out length) == false || length == 0)
            {
                throw new Exception(CreateErrorMessage(valueDescription + " должно быть целым положительным числом", lengthRaw));
            }
            return length;
        }

        private IEnumerable<T> GetItems<T>(uint itemsCount, Func<string, T> factory, string itemDescription)
        {
            for (var i = 1u; i <= itemsCount; i++)
            {
                var itemRaw = ReadString();
                if (itemRaw == null)
                    throw new Exception(CreateErrorMessage(
                        string.Format("{0}: должен содержать {1} строк, но содержит {2}", itemDescription, itemsCount, i)));

                var item = factory(itemRaw);
                yield return item;
            }
        }

        private DictionaryItem ConvertToDictionaryItem(string dictionaryItemRaw)
        {
            var parts = dictionaryItemRaw.Split(new[] { ' ' });
            uint count;
            if (parts.Length != 2 ||
                parts[0].All(Char.IsLetter) == false ||
                uint.TryParse(parts[1], out count) == false)
                throw new Exception(CreateErrorMessage("в строке частотного словаря должно быть слово и количество использований слова, разделенные пробелом"));
            return new DictionaryItem(parts[0], count);
        }

        private string ConvertToWordOpening(string wordOpeningRaw)
        {
            if (string.IsNullOrEmpty(wordOpeningRaw) ||
                wordOpeningRaw.All(Char.IsLetter) == false)
                throw new Exception(CreateErrorMessage("начало слова должно состоять только из букв"));
            return wordOpeningRaw;
        }

        private string CreateErrorMessage(string details, string line = null)
        {
            return string.Format("{0}, номер строки: {1}, строка: \"{2}\"", details, _lineNumber, line);
        }
    }
}