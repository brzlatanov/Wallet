using System.Text;
using Wallet.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Wallet.Clients
{
    internal class WalletHttpClient : IWalletHttpClient
    {
        private readonly HttpClient httpClient;
        private readonly IConfiguration config;

        public WalletHttpClient(HttpClient httpClient, IConfiguration config)
        {
            this.httpClient = httpClient;
            this.config = config;
        }

        public async Task<string> GetWalletBalanceAsync()
        {
            string endpoint = config.GetSection("ServiceUrls")["WalletService"];
            var response = await httpClient.GetAsync($"{endpoint}/WalletBalance");
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<HttpResponseMessage> DepositAsync(decimal amount)
        {
            string endpoint = config.GetSection("ServiceUrls")["WalletService"];
            var response = await httpClient.PostAsync(
                $"{endpoint}/Deposit", 
                new StringContent($"{amount}", Encoding.UTF8, "application/json"));

            return response;
        }

        public async Task<HttpResponseMessage> WithdrawAsync(decimal amount)
        {
            string endpoint = config.GetSection("ServiceUrls")["WalletService"];
            var response = await httpClient.PostAsync(
                $"{endpoint}/Withdraw",
                new StringContent($"{amount}", Encoding.UTF8, "application/json"));

            return response;
        }

        public async Task<HttpResponseMessage> PlaceBetAsync(decimal amount)
        {
            string endpoint = config.GetSection("ServiceUrls")["BettingService"];
            var response = await httpClient.PostAsync(
                $"{endpoint}/Bet",
                new StringContent($"{amount}", Encoding.UTF8, "application/json"));

            return response;
        }
    }
}