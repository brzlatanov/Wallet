using Data;
using Shared.DTOs;
using Wallet.Helpers;
using Wallet.Shared;

namespace WalletService.Services
{
    public class WalletService : IWalletService
    {
        private readonly IDatabaseContext dbContext;
        public WalletService(IDatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<decimal> GetBalanceAsync()
        {
            return await this.dbContext.GetBalanceAsync();
        }

        public async Task<decimal> DepositAsync(decimal amount)
        {
            var currentBalance = await this.dbContext.GetBalanceAsync();
            var newBalance = currentBalance + amount;
            await this.dbContext.SetBalanceAsync(newBalance);
            return newBalance;
        }

        public async Task<bool> WithdrawAsync(decimal amount)
        {
            var currentBalance = await this.dbContext.GetBalanceAsync();

            if (currentBalance >= amount)
            {
                currentBalance -= amount;
                await this.dbContext.SetBalanceAsync(currentBalance);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
