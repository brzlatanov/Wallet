using Shared.DTOs;
using System.Text.Json;
using Wallet.Helpers;
using Wallet.Interfaces;
using Wallet.Shared;

internal class WithdrawCommandHandler : ICommandHandler
{
    private readonly IWalletHttpClient walletHttpClient;

    public WithdrawCommandHandler(IWalletHttpClient walletHttpClient)
    {
        this.walletHttpClient = walletHttpClient;
    }

    public bool CanHandle(string command) => command.Equals(Constants.Actions.Withdraw, StringComparison.OrdinalIgnoreCase);

    public async Task<string> Handle(Decimal amount)
    {
        var withdrawResponse = await this.walletHttpClient.WithdrawAsync(amount);

        if(!withdrawResponse.IsSuccessStatusCode)
        {
            var errorDto = JsonSerializer.Deserialize<ErrorDTO>(
    await withdrawResponse.Content.ReadAsStringAsync());

            return errorDto.Message;
        }

        var responseDto = JsonSerializer.Deserialize<BalanceDTO>(
        await withdrawResponse.Content.ReadAsStringAsync());

        return FormatHelper.FormatMessage(Constants.SuccessfulWithdrawalMessage, amount, responseDto.Balance);
    }
}
