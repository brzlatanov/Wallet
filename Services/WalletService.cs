using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Interfaces;

namespace Wallet.Services
{
    internal class WalletService : IWalletService
    {
        public decimal balance = 0;

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Deposit amount must be positive.");
            }
            balance += amount;
        }
    }
}
