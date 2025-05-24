
using Wallet.Interfaces;
using Wallet.Data;
using Wallet.Helpers;

namespace Wallet.Services
{
    internal class WalletService : IWalletService
    {
        public decimal balance = 0;

        public decimal Balance
        {
            get => balance; 
        }

        public void Deposit(decimal amount)
        {
            balance += amount;
        }

        public void Withdraw(decimal amount)
        {
            if (balance >= amount)
            {
                balance -= amount;
            }
            else
            {
                throw new InvalidOperationException(FormatHelper.FormatMessage(Constants.InsufficientFundsMessage, balance));
            }
        }
    }
}
