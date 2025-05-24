using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Interfaces
{
    internal interface IWalletService
    {
        decimal Balance
        {
            get;
        }
        void Deposit(decimal amount);

        void Withdraw(decimal amount);
    }
}
