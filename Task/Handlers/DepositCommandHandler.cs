using Wallet.Shared;
using Wallet.Helpers;
using Wallet.Interfaces;
using System.Text.Json;
using Shared.DTOs;

internal class DepositCommandHandler : ICommandHandler
{
    private readonly IWalletHttpClient walletHttpClient;

    public DepositCommandHandler(IWalletHttpClient walletHttpClient)
    {
        this.walletHttpClient = walletHttpClient;
    }

    public bool CanHandle(string command) => command.Equals(Constants.Actions.Deposit, StringComparison.OrdinalIgnoreCase);

    public async Task<string> Handle(Decimal amount)
    {
        var depositResponse = await this.walletHttpClient.DepositAsync(amount);

        if(!depositResponse.IsSuccessStatusCode)
        {
            return FormatHelper.FormatMessage(Constants.UnsuccessfulDepositMessage, amount);
        }

        var responseDto = JsonSerializer.Deserialize<BalanceDTO>(
            await depositResponse.Content.ReadAsStringAsync());

        return FormatHelper.FormatMessage(Constants.SuccessfulDepositMessage, amount, responseDto.Balance);
    }
}