
using Wallet.Interfaces;
using Wallet.Data;

namespace Wallet.Services
{
    internal class WalletService : IWalletService
    {
        public decimal balance = 0;

        public decimal Deposit(decimal amount)
        {
            balance += amount;
            return balance;
        }

        public decimal Withdraw(decimal amount)
        {
            if (balance >= amount)
            {
                balance -= amount;
                return balance;
            }
            else
            {
                throw new InvalidOperationException(Constants.InsufficientFundsMessage);
            }
        }
    }
}
