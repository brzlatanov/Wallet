using Wallet.Interfaces;

namespace Wallet.Services
{
    internal class BettingService : IBettingService
    {
        private static readonly Random random = new Random();
        public decimal PlaceBet(decimal amount)
        {
            var percentage = random.Next(0, 100);
            Console.WriteLine(percentage);

            if(percentage <= 49)
            {
                return 0;
            }
            else if(percentage <= 89)
            {
                Console.WriteLine("Winning path 40%");
                var multiplier = CalculateMultiplier(101, 201);
                Console.WriteLine(multiplier);
                amount *= multiplier;
            }
            else 
            {
                Console.WriteLine("Winning path 90%");
                var multiplier = CalculateMultiplier(200, 1001);
                Console.WriteLine(multiplier);
                amount *= multiplier;
            }

            return amount;
        }

        public static decimal CalculateMultiplier(decimal from, decimal to)
        {
            var multiplier = (decimal)random.Next(101, 201) / 100;
            return multiplier;
        }
    }
}
