using Moq;
using Wallet.Interfaces;
using Wallet.Services;

namespace Wallet.Tests.Unit.Service
{
    public class BettingServiceTests
    {
        private BettingService.Services.BettingService service;
        public BettingServiceTests()
        {
            service = new BettingService.Services.BettingService();
        }

        [Fact]
        public void PlaceBet_Should_Return_NumberBetweenZeroAndAHundred()
        {
            List<decimal> results = new();

            
            for (int i = 0; i < Constants.BetIterations; i++)
            {
                var result = service.PlaceBet(10);
                results.Add(result);
            }

            Assert.True(results.All(r => r >= 0 && r <= 100), "All results should be between 0 and 100.");
        }

        [Fact]
        public void PlaceBet_Should_Return_NumberBetweenZeroAndAHundredInRanges()
        {
            bool hitLoss = false;
            bool hitMidRange = false;
            bool hitHighRange = false;
            List<decimal> results = new();

            for (int i = 0; i < Constants.BetIterations; i++)
            {
                var result = service.PlaceBet(Constants.BetAmount);

                if (result == Constants.ZeroResult)
                {
                    hitLoss = true;
                }
                else if (result >= Constants.MidRangeMin && result <= Constants.MidRangeMax)
                {
                    hitMidRange = true;
                }
                else if (result > Constants.MidRangeMax && result <= Constants.HighRangeMax)
                {
                    hitHighRange = true;
                }

                if (hitLoss && hitMidRange && hitHighRange)
                {
                    break;
                }
            }

            Assert.True(hitLoss, $"Should hit zero at least once.");
            Assert.True(hitMidRange, $"Should hit mid-range at least once ({Constants.MidRangeMin} - {Constants.MidRangeMax}).");
            Assert.True(hitHighRange, $"Should hit high range at least once ({Constants.HighRangeMin} - {Constants.HighRangeMax}).");
        }

        [Fact]
        public void PlaceBet_Should_Return_NumbersInExpectedRanges()
        {
            int lossHitCount = 0;
            int midRangeHitCount = 0;
            int highRangeHitCount = 0;

            List<decimal> results = new();

            for (int i = 0; i < Constants.BetIterations * 1000; i++)
            {
                var result = service.PlaceBet(Constants.BetAmount);

                if (result == Constants.ZeroResult)
                {
                    lossHitCount++;
                }
                else if (result >= Constants.MidRangeMin && result <= Constants.MidRangeMax)
                {
                    midRangeHitCount++;
                }
                else if (result > Constants.MidRangeMax && result <= Constants.HighRangeMax)
                {
                    highRangeHitCount++;
                }
            }

            double lossPercent = 100.0 * lossHitCount / (Constants.BetIterations * 1000);
            double lowWinPercent = 100.0 * midRangeHitCount / (Constants.BetIterations * 1000);
            double highWinPercent = 100.0 * highRangeHitCount / (Constants.BetIterations * 1000);

            Assert.True(lossPercent >= Constants.LossPercentLowerTreshold 
                && lossPercent <= Constants.LossPercentHigherTreshold, 
                $"Loss percentage should be in the treshold of {Constants.LossPercentLowerTreshold} - {Constants.LossPercentHigherTreshold}");

            Assert.True(lowWinPercent >= Constants.LowWinPercentLowerTreshold 
                && lowWinPercent <= Constants.LowWinPercentHigherTreshold,
                $"Low win percentage should be in the treshold of {Constants.LowWinPercentLowerTreshold} - {Constants.LowWinPercentHigherTreshold}");

            Assert.True(highWinPercent >= Constants.HighWinPercentLowerTreshold 
                && highWinPercent <= Constants.HighWinPercentHigherTreshold,
                $"High win percentage should be in the treshold of {Constants.HighWinPercentLowerTreshold} - {Constants.HighWinPercentHigherTreshold}");
        }
    }
}
