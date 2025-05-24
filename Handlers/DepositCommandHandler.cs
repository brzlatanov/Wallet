using Wallet.Data;
using Wallet.Helpers;
using Wallet.Interfaces;

internal class DepositCommandHandler : ICommandHandler
{
    private readonly IWalletService walletService;

    public DepositCommandHandler(IWalletService walletService)
    {
        this.walletService = walletService;
    }

    public bool CanHandle(string command) => command.Equals(Constants.Actions.Deposit, StringComparison.OrdinalIgnoreCase);

    public string Handle(Decimal amount)
    {
        this.walletService.Deposit(amount);
        var newBalance = this.walletService.Balance;
        return FormatHelper.FormatMessage(Constants.SuccessfulDepositMessage, amount, newBalance);
    }
}