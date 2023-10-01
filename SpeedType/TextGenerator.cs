using System.Net.Http.Headers;

namespace SpeedType
{
    public class TextGenerator
    {
        private Random random;

        private string[] _listOfWords;

        public TextGenerator()
        {
            random = new Random();

            using (var sr = new StreamReader("D:\\Code\\C#\\Learning\\SpeedType\\SpeedType\\CommonWords.txt"))
            {
                _listOfWords = sr.ReadToEnd().Split(new[]{'\n','\r'}, StringSplitOptions.RemoveEmptyEntries);
            }
        }
        public string GetWord()
        {
            int index = random.Next(1000);

            if (index < _listOfWords.Length)
                return _listOfWords[index];
            return _listOfWords[100];
        }
        public string GetText()
        {
            string result = string.Empty;

            int index;

            for (int i = 0; i < 10; i++)
            {
                index = random.Next(1000);
                result += (_listOfWords[index]+" ");
            }
            return result;
        }
    }
}