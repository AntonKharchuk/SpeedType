﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Authentication;
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

        private void TrackUserInput(string text)
        {
            string userText = string.Empty;

            text = text.Trim().ToLower();

            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();

            while (text!=userText.Trim().ToLower())
            {
                var currentKey = Console.ReadKey(true);

                if (currentKey.Key is ConsoleKey.Escape) 
                    break;

                if (currentKey.Modifiers is ConsoleModifiers.Control && currentKey.Key is ConsoleKey.Backspace)
                    continue;

                if ((currentKey.Key is ConsoleKey.Backspace))
                {
                    if (userText.Length>0)
                    {
                        userText = userText.Substring(0, userText.Length - 1);
                    }
                }
                if (currentKey.Key is not ConsoleKey.Backspace)
                {
                    userText += currentKey.KeyChar;
                }

                if (text.StartsWith(userText.Trim().ToLower()))
                    _userInput.ChangeInputColor(ConsoleColor.White);
                else
                    _userInput.ChangeInputColor(ConsoleColor.Red);

                Console.Write(currentKey.KeyChar);
            }
            Console.WriteLine();

            stopwatch.Stop();

            TimeSpan elapsedTime = stopwatch.Elapsed;

            Console.WriteLine($"time: {elapsedTime.Seconds}:{elapsedTime.Milliseconds}");
            Console.WriteLine();


        }

    }
}
