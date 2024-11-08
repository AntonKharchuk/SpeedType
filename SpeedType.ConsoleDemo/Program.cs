// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using SpeedType;
using SpeedType.ConsoleDemo;

var host = Host.CreateDefaultBuilder(args)
           .ConfigureServices((hostContext, services) =>
           {
               services.AddSingleton<ILemuelInput, LemuelWordsInput>();
               services.AddSingleton<MainWork>();
           })
           .Build();

var userInput = host.Services.GetRequiredService<ILemuelInput>();

var words = userInput.GetWords();

var generator = new TextGenerator();
generator.AddCastomWords(words);

var mainWork = new MainWork(generator.GetText, userInput);

for (int i = 0; i < 30; i++)
{
    mainWork.Run();
}

mainWork.ShowStatistics();

Console.ReadLine();


