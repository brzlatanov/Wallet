using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Interfaces
{
    internal interface IWalletHttpClient
    {
        Task<string> GetWalletBalanceAsync();

        Task<HttpResponseMessage> DepositAsync(decimal amount);

        Task<HttpResponseMessage> WithdrawAsync(decimal amount);
    }
}
