using System;
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

        private List<TimeSpan> _typingDuration;

        public MainWork(Func<string> getTextToType, IUserInput userInput)
        {
            _getTextToType = getTextToType;

            _userInput = userInput;

            _typingDuration = new List<TimeSpan>();
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

            text = text.ToLower();

            Stopwatch stopwatch = new Stopwatch();

            var stopwatchIsStarted = false;

            while (text != userText.ToLower())
            {
                var currentKey = Console.ReadKey(true);

                if (!stopwatchIsStarted)
                {
                    stopwatch.Start();
                    stopwatchIsStarted = true;
                }

                if (currentKey.Key is ConsoleKey.Escape)
                {
                    _userInput.ChangeInputColor(ConsoleColor.White);
                    break;
                }


                if (currentKey.Modifiers is ConsoleModifiers.Control && currentKey.Key is ConsoleKey.Backspace)
                    continue;

                if ((currentKey.Key is ConsoleKey.Backspace))
                {
                    if (userText.Length > 0)
                    {
                        userText = userText.Substring(0, userText.Length - 1);
                    }
                }
                if (currentKey.Key is not ConsoleKey.Backspace)
                {
                    userText += currentKey.KeyChar;
                }

                if (text.StartsWith(userText.ToLower()))
                    _userInput.ChangeInputColor(ConsoleColor.White);
                else
                {
                    _userInput.ChangeInputColor(ConsoleColor.Red);
                    if (currentKey.Key is ConsoleKey.Spacebar)
                    {
                        Console.Write('_');
                        continue;
                    }
                }

                Console.Write(currentKey.KeyChar);
            }
            Console.WriteLine();

            stopwatch.Stop();

            TimeSpan elapsedTime = stopwatch.Elapsed;

            AddDuration(elapsedTime);
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"\t\t\ttime: {elapsedTime.Seconds}:{elapsedTime.Milliseconds}");
            Console.WriteLine();
        }

        private void AddDuration(TimeSpan timeSpan)
        {
            _typingDuration.Add(timeSpan);
        }

        public void ShowStatistics()
        {
            Console.WriteLine("| Try |   Duration (s.)  |");
            Console.WriteLine("|-----|------------------|");

            var minTime = double.MaxValue;

            var escClickDuration = new TimeSpan();
            escClickDuration = escClickDuration.Add(TimeSpan.FromSeconds(10));

            for (int i = 0; i < _typingDuration.Count; i++)
            {
                if (_typingDuration[i] > escClickDuration)
                {
                    Console.WriteLine($"| {i,-3} |     {_typingDuration[i].TotalSeconds,5:F3} s.    |");
                    if (minTime > _typingDuration[i].TotalSeconds)
                        minTime = _typingDuration[i].TotalSeconds;
                }
                else
                {
                    Console.WriteLine($"| {i,-3} |      ------      |");
                }
            }

            Console.WriteLine("|-----|------------------|");
            Console.WriteLine();
            if (minTime is double.MaxValue)
            {
                Console.WriteLine($"Best time: ------ s.");
            }
            else
                Console.WriteLine($"Best time: {minTime} s.");

        }

    }
}
