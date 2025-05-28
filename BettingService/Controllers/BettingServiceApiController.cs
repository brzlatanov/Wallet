using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Wallet.Interfaces;
using Wallet.Shared;

namespace BettingService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BettingServiceApiController : ControllerBase
    {
        private readonly ILogger<BettingServiceApiController> logger;
        private readonly IBettingService bettingService;

        public BettingServiceApiController(ILogger<BettingServiceApiController> logger,
             IBettingService bettingService)
        {
            this.logger = logger;
            this.bettingService = bettingService;
        }

        [HttpPost("/Bet")]
        public IActionResult Bet([FromBody] decimal amount)
        {
            if (amount <= 0)
            {
                return StatusCode(500, Constants.AmountMustBePositiveError);
            }

            Console.WriteLine($"Bet endpoint called with amount {amount}");
            try
            {
                var amountAfterBet = this.bettingService.PlaceBet(amount);

                return Ok(new BalanceDTO { Balance = amountAfterBet });
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, Constants.ErrorPlacingBetMessage);
                return StatusCode(500, Constants.ErrorPlacingBetMessage);
            }

        }
    }
}
