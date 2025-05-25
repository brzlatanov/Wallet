using Microsoft.Extensions.DependencyInjection;
using Wallet.Clients;
using Wallet.Controllers;
using Wallet.Data;
using Wallet.Interfaces;
using Wallet.Services;
using Wallet.UI;
using Wallet.Validators;

var serviceProvider = new ServiceCollection()
    .AddTransient<IView, ConsoleView>()
    .AddTransient<IValidator, Validator>()
    .AddSingleton<IWalletService, WalletService>()
    .AddSingleton<IBettingService, BettingService>()
    .AddTransient<ICommandHandler, BetCommandHandler>()
    .AddTransient<ICommandHandler, DepositCommandHandler>()
    .AddTransient<ICommandHandler, WithdrawCommandHandler>()
    .AddTransient<GameController>()
    .AddHttpClient<IWalletHttpClient, WalletHttpClient>()
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
