using Microsoft.Extensions.DependencyInjection;
using Wallet.Controllers;
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
controller.Run();