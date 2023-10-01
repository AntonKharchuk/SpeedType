namespace SpeedType
{
    public class TextGenerator
    {
        private string _defaultWord;
        private string _defaultText;

        public TextGenerator()
        {
            _defaultWord = "Mama";
            _defaultText = "I love mama";
        }
        public string GetWord()
        {
            return _defaultWord;
        }
        public string GetText()
        {
            return _defaultText;
        }
    }
}