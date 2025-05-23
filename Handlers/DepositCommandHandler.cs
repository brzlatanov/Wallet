using Wallet.Data;
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
        var newBalance = walletService.Deposit(amount);
        return String.Format(Constants.SuccessfulDepositMessage, amount, newBalance);
    }
}