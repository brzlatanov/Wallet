using System.Runtime.CompilerServices;
using Wallet.Interfaces;
using Wallet.Shared;

[assembly: InternalsVisibleTo("Wallet.Tests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace BettingService.Services
{
    internal class BettingService : IBettingService
    {
        private static readonly Random random = new Random();
        public decimal PlaceBet(decimal amount)
        {
            if (amount <= 0)
            {
               throw new InvalidDataException(Constants.AmountMustBePositiveError);
            }

            var percentage = random.Next(0, 100);
            Console.WriteLine(percentage);

            if (percentage <= 49)
            {
                return 0;
            }
            else if (percentage <= 89)
            {
                var multiplier = CalculateMultiplier(101, 201);
                amount *= multiplier;
            }
            else
            {
                var multiplier = CalculateMultiplier(200, 1001);
                amount *= multiplier;
            }

            return amount;
        }

        private static decimal CalculateMultiplier(int from, int to)
        {
            var multiplier = (decimal)random.Next(from, to) / 100m;
            return multiplier;
        }
    }
}

