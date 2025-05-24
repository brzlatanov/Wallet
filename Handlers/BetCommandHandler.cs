using Wallet.Data;
using Wallet.Helpers;
using Wallet.Interfaces;
using Wallet.Services;

internal class BetCommandHandler : ICommandHandler
{
    private readonly IBettingService bettingService;
    private readonly IWalletService walletService;

    public BetCommandHandler(IBettingService bettingService, IWalletService walletService)
    {
        this.bettingService = bettingService;
        this.walletService = walletService;
    }

    public bool CanHandle(string command) => command.Equals(Constants.Actions.Bet, StringComparison.OrdinalIgnoreCase);

    public string Handle(decimal amount)
    {
        if (amount < Constants.MinBetAmount || amount > Constants.MaxBetAmount)
        {
            return FormatHelper.FormatMessage(Constants.AmountMustBeWithinRangeError, Constants.MinBetAmount, Constants.MaxBetAmount);
        }

        this.walletService.Withdraw(amount);
        var amountAfterBet = this.bettingService.PlaceBet(amount);

        if (amountAfterBet > 0)
        {
            this.walletService.Deposit(amountAfterBet);
            return FormatHelper.FormatMessage(Constants.BetWonMessage, amountAfterBet, this.walletService.Balance);
        }

        return FormatHelper.FormatMessage(Constants.BetLostMessage, this.walletService.Balance);
    }
}
