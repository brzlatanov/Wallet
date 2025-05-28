using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using Wallet.Helpers;
using Wallet.Interfaces;
using Wallet.Shared;

namespace Wallet.Services
{
    internal class BettingOrchestrationService : IBettingOrchestrationService
    {
        private readonly IWalletHttpClient walletHttpClient;

        public BettingOrchestrationService(IWalletHttpClient walletHttpClient)
        {
            this.walletHttpClient = walletHttpClient;
        }
        public async Task<string> PlaceBetAsync(decimal amount)
        {
            if (amount < Constants.MinBetAmount || amount > Constants.MaxBetAmount)
            {
                return FormatHelper.FormatMessage(Constants.AmountMustBeWithinRangeError, Constants.MinBetAmount, Constants.MaxBetAmount);
            }

            // if withdrawal fails, we must stop execution
            HttpResponseMessage withdrawResponse;
            try
            {
                withdrawResponse = await walletHttpClient.WithdrawAsync(amount);
            }
            catch
            {
                // endpoint is unresponsive, we can't withdraw
                return FormatHelper.FormatMessage(Constants.UnsuccessfulBetMessage, amount);
            }

            // endpoint is responsive, but it doesn't return a success status code
            if (!withdrawResponse.IsSuccessStatusCode)
            {
                return FormatHelper.FormatMessage(Constants.UnsuccessfulBetMessage, amount);
            }

            // if a bet can't be placed, we have a withdrawal and we must return it
            HttpResponseMessage betResponse;
            try
            {
                betResponse = await this.walletHttpClient.PlaceBetAsync(amount);
            }
            catch
            {
                var refundResult = await TryRefundAsync(amount);
                return refundResult;
            }

            if (!betResponse.IsSuccessStatusCode)
            {
                var refundResult = await TryRefundAsync(amount);
                return refundResult;
            }

            var betResponseDto = JsonSerializer.Deserialize<BalanceDTO>(
                await betResponse.Content.ReadAsStringAsync());

            if (betResponseDto == null)
            {
                var refundResult = await TryRefundAsync(amount);
                return refundResult;
            }

            BalanceDTO? newBalanceDto = JsonSerializer.Deserialize<BalanceDTO>(
                await this.walletHttpClient.GetWalletBalanceAsync());

            if (newBalanceDto == null)
            {
                var refundResult = await TryRefundAsync(amount);
                return refundResult;
            }

            if (betResponseDto.Balance > 0)
            {
                bool depositSuccess;

                try
                {
                    depositSuccess = await TryDepositAsync(betResponseDto.Balance);

                }
                catch
                {
                    return Constants.BetFailedDepositError;
                }

                if (!depositSuccess)
                {
                    return Constants.BetFailedDepositError;
                }

                newBalanceDto = JsonSerializer.Deserialize<BalanceDTO>(
                    await this.walletHttpClient.GetWalletBalanceAsync());

                if (newBalanceDto == null)
                {
                    var refundResult = await TryRefundAsync(amount);
                    return refundResult;
                }

                return FormatHelper.FormatMessage(Constants.BetWonMessage, betResponseDto.Balance, newBalanceDto.Balance);
            }

            return FormatHelper.FormatMessage(Constants.BetLostMessage, newBalanceDto.Balance);
        }

        private async Task<string?> TryRefundAsync(decimal amount)
        {
            HttpResponseMessage refundResponse;
            try
            {
                refundResponse = await walletHttpClient.DepositAsync(amount);
            }
            catch
            {
                return Constants.BetFailedDepositError;
            }

            if (!refundResponse.IsSuccessStatusCode)
            {
                return Constants.BetFailedDepositError;
            }

            return FormatHelper.FormatMessage(Constants.UnsuccessfulBetMessage, amount);
        }

        private async Task<bool> TryDepositAsync(decimal amount)
        {
            try
            {
                var depositResponse = await walletHttpClient.DepositAsync(amount);
                return depositResponse.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}
