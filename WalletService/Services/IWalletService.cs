using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletService.Services
{
    public interface IWalletService
    {
        Task<decimal> GetBalanceAsync();
        Task<decimal> DepositAsync(decimal amount);

        Task<bool> WithdrawAsync(decimal amount);

        //void Withdraw(decimal amount);
    }
}
