using Microsoft.Extensions.DependencyInjection;
using Wallet.Controllers;
using Wallet.Interfaces;
using Wallet.UI;

// [x] Initialize controller
// [x] Controller gets view with dependency injection 

var serviceProvider = new ServiceCollection()
            .AddSingleton<IView, ConsoleView>()
            .AddSingleton<GameController>() 
            .BuildServiceProvider();

var controller = serviceProvider.GetRequiredService<GameController>();
controller.Run();