using Microsoft.Extensions.DependencyInjection;
using Wallet.Controllers;
using Wallet.Data;
using Wallet.Interfaces;
using Wallet.UI;
using Wallet.Validators;

// [x] Initialize controller
// [x] Controller gets view with dependency injection 

var serviceProvider = new ServiceCollection()
            .AddSingleton<IView, ConsoleView>()
            .AddSingleton<IValidator, StringValidator>()
            .AddSingleton<GameController>() 
            .BuildServiceProvider();

var controller = serviceProvider.GetRequiredService<GameController>();

while (true)
{
    Console.WriteLine(Constants.SubmitActionPrompt);
    string input = Console.ReadLine();
    if (input.Equals(Constants.ExitCommand, StringComparison.OrdinalIgnoreCase))
        break;
    controller.ProcessInput(input);
}
