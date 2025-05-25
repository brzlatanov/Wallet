
using System.Net.Http;
using System.Text;
using Wallet.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Wallet.Clients
{
    internal class WalletHttpClient : IWalletHttpClient
    {
        private readonly HttpClient _httpClient;

        public WalletHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetWalletBalanceAsync()
        {
            var response = await _httpClient.GetAsync("http://localhost:5010/WalletBalance");
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<HttpResponseMessage> DepositAsync(decimal amount)
        {
            var response = await _httpClient.PostAsync(
                "http://localhost:5010/Deposit", 
                new StringContent($"{amount}", Encoding.UTF8, "application/json"));

            return response;
        }
    }
}