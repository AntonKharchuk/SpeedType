﻿// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using SpeedType;
using SpeedType.ConsoleDemo;

var host = Host.CreateDefaultBuilder(args)
           .ConfigureServices((hostContext, services) =>
           {
               services.AddSingleton<IUserInput, UserInput>();
               services.AddSingleton<MainWork>();
           })
           .Build();

Console.WriteLine("Let's type!\n");

var userInput = host.Services.GetRequiredService<IUserInput>();

var generator = new TextGenerator();
generator.AddComputerTerms();
generator.Add1000CummonWords();

var mainWork = new MainWork(generator.GetText, userInput);

for (int i = 0; i < 30; i++)
{
    mainWork.Run();
}

mainWork.ShowStatistics();

Console.ReadLine();


