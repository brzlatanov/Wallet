using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Interfaces
{
    internal interface IWalletHttpClient
    {

        public Task<string> GetWalletBalanceAsync();

        public Task<HttpResponseMessage> DepositAsync(decimal amount);
    }
}
