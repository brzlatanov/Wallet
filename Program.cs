using Microsoft.Extensions.DependencyInjection;
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
            .BuildServiceProvider();

var controller = serviceProvider.GetRequiredService<GameController>();

// Core loop
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
