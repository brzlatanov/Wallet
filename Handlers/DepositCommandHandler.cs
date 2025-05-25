using Wallet.Data;
using Wallet.Helpers;
using Wallet.Interfaces;

internal class DepositCommandHandler : ICommandHandler
{
    private readonly IWalletService walletService;
    private readonly IWalletHttpClient walletHttpClient;

    public DepositCommandHandler(IWalletService walletService, 
        IWalletHttpClient walletHttpClient)
    {
        this.walletService = walletService;
        this.walletHttpClient = walletHttpClient;
    }

    public bool CanHandle(string command) => command.Equals(Constants.Actions.Deposit, StringComparison.OrdinalIgnoreCase);

    public async Task<string> Handle(Decimal amount)
    {
        var response = await this.walletHttpClient.DepositAsync(amount);

        if(!response.IsSuccessStatusCode)
        {
            return Constants.UnsuccessfulDepositMessage;
        }

        var newBalance = await this.walletHttpClient.GetWalletBalanceAsync();
        return FormatHelper.FormatMessage(Constants.SuccessfulDepositMessage, amount, newBalance);
    }
}