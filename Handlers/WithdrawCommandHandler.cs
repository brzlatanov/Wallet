using Wallet.Data;
using Wallet.Helpers;
using Wallet.Interfaces;

internal class WithdrawCommandHandler : ICommandHandler
{
    private readonly IWalletService walletService;

    public WithdrawCommandHandler(IWalletService walletService)
    {
        this.walletService = walletService;
    }

    public bool CanHandle(string command) => command.Equals(Constants.Actions.Withdraw, StringComparison.OrdinalIgnoreCase);

    public string Handle(Decimal amount)
    {
        var newBalance = walletService.Withdraw(amount);
        return FormatHelper.FormatMessage(Constants.SuccessfulDepositMessage, amount, newBalance);
    }
}
