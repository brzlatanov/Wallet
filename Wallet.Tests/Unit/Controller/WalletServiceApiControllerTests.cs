using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Shared.DTOs;
using WalletService.Controllers;
using WalletService.Services;
using Wallet.Shared;

namespace Wallet.Tests.Unit.Controller
{
    public class WalletServiceApiControllerTests
    {
        private readonly Mock<ILogger<WalletServiceApiController>> loggerMock = new();
        private readonly Mock<IWalletService> walletServiceMock = new();
        private readonly WalletServiceApiController controller;

        public WalletServiceApiControllerTests()
        {
            this.controller = new WalletServiceApiController(loggerMock.Object, walletServiceMock.Object);
        }

        [Fact]
        public async Task GetWalletBalance_ShouldReturnOkWithBalance()
        {
            this.walletServiceMock.Setup(s => s.GetBalanceAsync()).ReturnsAsync(50m);

            var result = await controller.GetWalletBalance();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var dto = Assert.IsType<BalanceDTO>(okResult.Value);
            Assert.Equal(50m, dto.Balance);
        }

        [Fact]
        public async Task GetWalletBalance_WhenGetBalanceFails_ShouldReturn500()
        {
            this.walletServiceMock.Setup(s => s.GetBalanceAsync()).ThrowsAsync(new IOException());

            var result = await controller.GetWalletBalance();

            var status = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, status.StatusCode);
        }

        [Fact]
        public async Task Deposit_WithInvalidAmount_ShouldReturnBadRequest()
        {
            var result = await controller.Deposit(0);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            var error = Assert.IsType<ErrorDTO>(badRequest.Value);
            Assert.Equal(Shared.Constants.AmountMustBePositiveError, error.Message);
        }

        [Fact]
        public async Task Deposit_WithValidAmount_ShouldReturnOkWithNewBalance()
        {
            this.walletServiceMock.Setup(s => s.DepositAsync(10m)).ReturnsAsync(60m);

            var result = await controller.Deposit(10m);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var dto = Assert.IsType<BalanceDTO>(okResult.Value);
            Assert.Equal(60m, dto.Balance);
        }

        [Fact]
        public async Task Deposit_WhenException_ShouldReturn500()
        {
            this.walletServiceMock.Setup(s => s.DepositAsync(It.IsAny<decimal>())).ThrowsAsync(new Exception());

            var result = await controller.Deposit(10m);

            var status = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, status.StatusCode);

            var error = Assert.IsType<ErrorDTO>(status.Value);
            Assert.Equal("Your deposit of $10.00 was not successful. Please try again.", error.Message);
        }

        [Fact]
        public async Task Withdraw_Success_ShouldReturnOkWithNewBalance()
        {
            this.walletServiceMock.Setup(s => s.WithdrawAsync(10m)).ReturnsAsync(true);
            this.walletServiceMock.Setup(s => s.GetBalanceAsync()).ReturnsAsync(40m);

            var result = await controller.Withdraw(10m);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var dto = Assert.IsType<BalanceDTO>(okResult.Value);
            Assert.Equal(40m, dto.Balance);
        }

        [Fact]
        public async Task Withdraw_InsufficientFunds_ShouldReturnBadRequest()
        {
            this.walletServiceMock.Setup(s => s.WithdrawAsync(100m)).ReturnsAsync(false);

            var result = await controller.Withdraw(100m);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            var error = Assert.IsType<ErrorDTO>(badRequest.Value);
            Assert.Equal("Withdrawal failed. Insufficient funds or invalid amount.", error.Message);
        }

        [Fact]
        public async Task Withdraw_WhenException_ShouldReturn500()
        {
            this.walletServiceMock.Setup(s => s.WithdrawAsync(It.IsAny<decimal>())).ThrowsAsync(new Exception());

            var result = await controller.Withdraw(10m);

            var status = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, status.StatusCode);
        }

        [Fact]
        public async Task Withdraw_WhenGetBalanceFails_ShouldReturn500()
        {
            this.walletServiceMock.Setup(s => s.WithdrawAsync(It.IsAny<decimal>())).ReturnsAsync(true);
            this.walletServiceMock.Setup(s => s.GetBalanceAsync()).ThrowsAsync(new IOException());

            var result = await controller.Withdraw(10m);

            var status = Assert.IsType<ObjectResult>(result);
            var error = Assert.IsType<ErrorDTO>(status.Value);
            Assert.Equal(500, status.StatusCode);
            Assert.Equal("Withdrawal succeeded, but failed to retrieve the new balance.", error.Message);
        }
    }
}
