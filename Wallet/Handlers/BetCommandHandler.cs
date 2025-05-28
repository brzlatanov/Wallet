using Shared.DTOs;
using System.Text.Json;
using Wallet.Helpers;
using Wallet.Interfaces;
using Wallet.Services;
using Wallet.Shared;

public class BetCommandHandler : ICommandHandler
{
    private readonly IBettingOrchestrationService bettingOrchestrationService;

    public BetCommandHandler(IBettingOrchestrationService bettingService)
    {
        this.bettingOrchestrationService = bettingService;
    }

    public bool CanHandle(string command) => command.Equals(Constants.Actions.Bet, StringComparison.OrdinalIgnoreCase);

    public async Task<string> Handle(decimal amount)
    {
        return await bettingOrchestrationService.PlaceBetAsync(amount);
    }
}
