using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using Wallet.Clients;
using Wallet.Controllers;
using Wallet.Interfaces;
using Wallet.Services;
using Wallet.Shared;
using Wallet.UI;
using Wallet.Validators;
using System.Runtime.CompilerServices;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", 
    optional: true, 
    reloadOnChange: true)
    .Build();

var serviceProvider = new ServiceCollection()
    .AddTransient<IView, ConsoleView>()
    .AddTransient<IValidator, Validator>()
    .AddSingleton<IConfiguration>(configuration)
    .AddTransient<ICommandHandler, BetCommandHandler>()
    .AddTransient<ICommandHandler, DepositCommandHandler>()
    .AddTransient<ICommandHandler, WithdrawCommandHandler>()
    .AddTransient<IBettingOrchestrationService, BettingOrchestrationService>()
    .AddTransient<GameController>()
    .AddHttpClient<IWalletHttpClient, WalletHttpClient>()
    .AddPolicyHandler(HttpPolicyExtensions
        .HandleTransientHttpError()
        .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromMilliseconds(200)))
    .Services
    .BuildServiceProvider();

var controller = serviceProvider.GetRequiredService<GameController>();

while (true)
{
    Console.WriteLine(Constants.SubmitActionPrompt);
    string input = Console.ReadLine();
    if (input.Equals(Constants.ExitCommand, StringComparison.OrdinalIgnoreCase))
    {
        Console.WriteLine(Constants.GoodbyeMessage);
        break;
    }

    controller.ProcessInput(input);
}
