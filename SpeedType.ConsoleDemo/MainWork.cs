using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedType.ConsoleDemo
{
    internal class MainWork
    {
        private Func<string> _getTextToType;

        private IUserInput _userInput;

        public MainWork(Func<string> getTextToType, IUserInput userInput)
        {
            _getTextToType = getTextToType;

            _userInput = userInput;
        }

        public async void Run()
        {
            var textToType = _getTextToType.Invoke();

            Console.WriteLine(textToType);

            TrackUserInput(textToType);
        }

        private async void TrackUserInput(string text)
        {
            string userText = string.Empty;

            text = text.Trim().ToLower();

            while (text!=userText.Trim().ToLower())
            {
                var currentKey = Console.ReadKey(true);

                if (currentKey.Key is ConsoleKey.Escape) break;

                userText += currentKey.KeyChar;
            }

            Console.WriteLine(userText);
        } 

    }
}
