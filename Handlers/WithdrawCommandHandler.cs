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

    public Task<string> Handle(Decimal amount)
    {
        //this.walletService.Withdraw(amount);
        //var newBalance = this.walletService.Balance;
        //return FormatHelper.FormatMessage(Constants.SuccessfulDepositMessage, amount, newBalance);
        return null;
    }
}
