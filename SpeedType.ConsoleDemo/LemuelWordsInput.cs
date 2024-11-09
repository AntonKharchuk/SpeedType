using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

using Newtonsoft.Json;
namespace SpeedType.ConsoleDemo
{
    internal class LemuelWordsInput:UserInput, ILemuelInput
    {

        private List<string> _listOfWords;
        private readonly string _filename;

        public LemuelWordsInput()
        {
            _filename = "LemuelWords.txt";
            _listOfWords = new List<string>();
        }

        public void LoadWords()
        {
            // Проверяем, существует ли файл
            if (File.Exists(_filename))
            {
                // Чтение содержимого файла
                string jsonContent = File.ReadAllText(_filename);

                // Десериализация JSON в список строк
                List<string> wordsFromFile = JsonConvert.DeserializeObject<List<string>>(jsonContent);

                if (wordsFromFile != null)
                {
                    // Добавляем слова из файла в _listOfWords
                    _listOfWords.AddRange(wordsFromFile);
                }
            }
            else
            {
                // Если файл не существует, создаем его
                File.Create(_filename).Dispose();
            }
        }

        public IEnumerable<string> GetWords()
        {
            LoadWords();
            ProcessUserInput();
            return _listOfWords;
        }

        public void ProcessUserInput()
        {
            Console.WriteLine("Введiть нiчого, мiнус або слова:");

            string userInput = "";
            string line;
            while ((line = Console.ReadLine()) != null && line != "" )
            {
                if (line == "-")
                {
                    userInput = line;
                    break;
                }
                userInput += line + Environment.NewLine;
            }

            if (string.IsNullOrWhiteSpace(userInput))
            {
                // TODO: Действие 1 для пустой строки
                Console.WriteLine("Нiчого");
            }
            else if (userInput == "-")
            {
                // Удаление всех слов из файла
                _listOfWords.Clear();
                SaveWords();
                Console.WriteLine("Всi слова видаленi");
            }
            else
            {
                // TODO: Действие 2 для других значений
                Console.WriteLine("Слова додано");
                _listOfWords.AddRange(ParseEnglishWords(userInput));
                SaveWords();
            }
        }
        private List<string> ParseEnglishWords(string text)
        {
            List<string> englishWords = new List<string>();

            // Используем регулярное выражение для поиска английских слов в начале строки
            string pattern = @"^\d+\.\s+([A-Za-z\s()]+)";
            var lines = text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                var match = Regex.Match(line, pattern);
                if (match.Success)
                {
                    string englishWord = match.Groups[1].Value.Trim();
                    englishWords.Add(englishWord);
                }
            }

            return englishWords;
        }
        public void SaveWords()
        {
            // Сериализация списка в JSON и запись в файл
            string jsonContent = JsonConvert.SerializeObject(_listOfWords, Formatting.Indented);
            File.WriteAllText(_filename, jsonContent);
        }
    }
}

