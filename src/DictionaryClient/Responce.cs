namespace DictionaryClient
{
    internal class Responce
    {
        public static Responce Create(string text)
        {
            return new Responce {Text = text};
        }

        public static Responce CreateError(string message)
        {
            return new Responce {Error = message};
        }

        public string Text { get; private set; }
        public string Error { get; private set; }

        public bool HasError
        {
            get { return !string.IsNullOrEmpty(Error); }
        }
    }
}