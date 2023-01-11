namespace Framework.Runtime.Core.Widgets
{
    public struct TextModel
    {
        public static implicit operator TextModel(string text)
        {
            return new TextModel
            {
                Format = text
            };
        }
        
        public static implicit operator string(TextModel text)
        {
            return text.Format;
        }

        public string Format;
        public object[] Keys;
    }
}
