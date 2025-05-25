using Shared.DTOs;
using System.Text.Json;
using Wallet.Helpers;
using Wallet.Interfaces;
using Wallet.Shared;

internal class BetCommandHandler : ICommandHandler
{
    private readonly IWalletHttpClient walletHttpClient;

    public BetCommandHandler(IWalletHttpClient walletHttpClient)
    {
        this.walletHttpClient = walletHttpClient;
    }

    public bool CanHandle(string command) => command.Equals(Constants.Actions.Bet, StringComparison.OrdinalIgnoreCase);

    public async Task<string> Handle(decimal amount)
    {
        // mechanism to check if we can withdraw the amount instead of directly withdrawing it? 
        if (amount < Constants.MinBetAmount || amount > Constants.MaxBetAmount)
        {
            return FormatHelper.FormatMessage(Constants.AmountMustBeWithinRangeError, Constants.MinBetAmount, Constants.MaxBetAmount);
        }

        var withdrawResponse = await this.walletHttpClient.WithdrawAsync(amount);

        if (!withdrawResponse.IsSuccessStatusCode)
        {
            return FormatHelper.FormatMessage(Constants.UnsuccessfulBetMessage, amount);
        }

        var amountAfterBetResponse = await this.walletHttpClient.PlaceBetAsync(amount);

        if (!amountAfterBetResponse.IsSuccessStatusCode)
        {
            await this.walletHttpClient.DepositAsync(amount);
            return FormatHelper.FormatMessage(Constants.UnsuccessfulBetMessage, amount);
        }

        var responseDto = JsonSerializer.Deserialize<BalanceDTO>(
            await amountAfterBetResponse.Content.ReadAsStringAsync());

        string newBalance = await this.walletHttpClient.GetWalletBalanceAsync();
        if (responseDto?.Balance > 0)
        {
            var depositResponse = await this.walletHttpClient.DepositAsync(responseDto.Balance);
            newBalance = await this.walletHttpClient.GetWalletBalanceAsync();
            return FormatHelper.FormatMessage(Constants.BetWonMessage, responseDto.Balance, newBalance);
        }  

        return FormatHelper.FormatMessage(Constants.BetLostMessage, newBalance);
    }
}
