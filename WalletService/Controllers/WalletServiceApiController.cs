using Data;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using WalletService.Services;
using Wallet.Shared;
using Wallet.Helpers;

namespace WalletService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalletServiceApiController : ControllerBase
    {
        private readonly ILogger<WalletServiceApiController> logger;
        private readonly IWalletService walletService;

        public WalletServiceApiController(ILogger<WalletServiceApiController> logger,
            IWalletService walletService)
        {
            this.logger = logger;
            this.walletService = walletService;
        }

        [HttpGet("/WalletBalance")]
        public async Task<IActionResult> GetWalletBalance()
        {
            try
            {
                var currentBalance = await this.walletService.GetBalanceAsync();

                return Ok(new BalanceDTO{ Balance = currentBalance });
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, Constants.ErrorRetrievingBalanceMessage);
                return StatusCode(500, Constants.ErrorRetrievingBalanceMessage);
            }

        }

        [HttpPost("/Deposit")]
        public async Task<IActionResult> Deposit([FromBody] decimal amount)
        {
            if(amount <= 0)
            {
                return BadRequest(new ErrorDTO { Message = Constants.AmountMustBePositiveError });
            }

            try
            {
                var newBalance = await this.walletService.DepositAsync(amount);
                return Ok(new BalanceDTO { Balance = newBalance });
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, Constants.DepositFailedErrorMessage);
                return StatusCode(500, new ErrorDTO { Message = FormatHelper.FormatMessage(Constants.UnsuccessfulDepositMessage, amount) });
            } 
        }

        [HttpPost("/Withdraw")]
        public async Task<IActionResult> Withdraw([FromBody] decimal amount)
        {
            try
            {
                var isWithdrawalSuccessful = await this.walletService.WithdrawAsync(amount);
                if (isWithdrawalSuccessful)
                {
                    decimal newBalance;
                    try
                    {
                        newBalance = await this.walletService.GetBalanceAsync();
                        return Ok(new BalanceDTO { Balance = newBalance });
                    }
                    catch (Exception ex)
                    {
                        this.logger.LogError(ex, Constants.WithdrawalSucceededFailedToRetrieveNewBalanceMessage);
                        return StatusCode(500, new ErrorDTO { Message = Constants.WithdrawalSucceededFailedToRetrieveNewBalanceMessage });
                    }
                }
                else
                {
                    return BadRequest(new ErrorDTO { Message = "Withdrawal failed. Insufficient funds or invalid amount." });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, Constants.ErrorWithdrawingBalanceMessage);
                return StatusCode(500, new ErrorDTO { Message = Constants.ErrorWithdrawingBalanceMessage });
            }
        }
    }
}
