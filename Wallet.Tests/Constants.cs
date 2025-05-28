using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Tests
{
    internal class Constants
    {
        public const int BetIterations = 1000;
        public const decimal BetAmount = 10m;
        public const decimal ZeroResult = 0m;
        public const decimal MidRangeMin = 10.01m;
        public const decimal MidRangeMax = 20.00m;
        public const decimal HighRangeMin = 20.01m;
        public const decimal HighRangeMax = 100.00m;

        public const int LossPercentLowerTreshold = 49;
        public const int LossPercentHigherTreshold = 51;
        public const int LowWinPercentLowerTreshold = 39;
        public const int LowWinPercentHigherTreshold = 41;
        public const int HighWinPercentLowerTreshold = 9;
        public const int HighWinPercentHigherTreshold = 11;
    }
}
