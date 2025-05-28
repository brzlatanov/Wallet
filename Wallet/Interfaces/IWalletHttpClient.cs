using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("Wallet.Tests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace Wallet.Interfaces
{
    internal interface IWalletHttpClient
    {
        Task<string> GetWalletBalanceAsync();

        Task<HttpResponseMessage> DepositAsync(decimal amount);

        Task<HttpResponseMessage> WithdrawAsync(decimal amount);

        Task<HttpResponseMessage> PlaceBetAsync(decimal amount);
    }
}
