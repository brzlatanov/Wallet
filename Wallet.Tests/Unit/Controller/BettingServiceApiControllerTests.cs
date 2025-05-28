using BettingService.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Shared.DTOs;
using Wallet.Interfaces;
using Wallet.Shared;

namespace Wallet.Tests.Unit.Controller
{
    public class BettingServiceApiControllerTests
    {
        private readonly Mock<ILogger<BettingServiceApiController>> loggerMock = new();
        private readonly Mock<IBettingService> bettingServiceMock = new();
        private readonly BettingServiceApiController controller;

        public BettingServiceApiControllerTests()
        {
            controller = new BettingServiceApiController(loggerMock.Object, bettingServiceMock.Object);
        }

        [Fact]
        public async Task BetCall_ShouldReturn500IfServiceThrowsException()
        {
            this.bettingServiceMock.Setup(s => s.PlaceBet(It.IsAny<decimal>())).Throws(new Exception());

            var result = this.controller.Bet(5m);

            var status = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, status.StatusCode);
            Assert.Equal("An error occurred while placing bet.", status.Value);
        }

        [Fact]
        public async Task BetCall_ShouldReturnOkWithBalance()
        {
            this.bettingServiceMock.Setup(s => s.PlaceBet(5m)).Returns(9m);

            var result = this.controller.Bet(5m);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var dto = Assert.IsType<BalanceDTO>(okResult.Value);
            Assert.Equal(9m, dto.Balance);
        }

        [Fact]
        public void BetCall_ShouldReturnBadRequest_ForNegativeAmount()
        {
            decimal invalidAmount = -10m;
            var result = controller.Bet(invalidAmount);
            var status = Assert.IsType<ObjectResult>(result);

            Assert.Equal(500, status.StatusCode);
            Assert.Equal(Shared.Constants.AmountMustBePositiveError, status.Value);
        }

        [Fact]
        public void BetCall_ShouldReturnBadRequest_ForZeroAmount()
        {
            decimal invalidAmount = 0m;
            var result = controller.Bet(invalidAmount);
            var status = Assert.IsType<ObjectResult>(result);

            Assert.Equal(500, status.StatusCode);
            Assert.Equal(Shared.Constants.AmountMustBePositiveError, status.Value);
        }

        [Fact]
        public void BetCall_ShouldCallService_WithCorrectAmount()
        {
            decimal betAmount = 5m;
            bettingServiceMock.Setup(s => s.PlaceBet(betAmount)).Returns(10m);

            var result = controller.Bet(betAmount);

            bettingServiceMock.Verify(s => s.PlaceBet(betAmount), Times.Once);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var dto = Assert.IsType<BalanceDTO>(okResult.Value);
            Assert.Equal(10m, dto.Balance);
        }

        [Fact]
        public void BetCall_ShouldReturnInternalServerError_WhenServiceReturnsNegativeBalance()
        {
            decimal betAmount = -5m;
            bettingServiceMock.Setup(s => s.PlaceBet(betAmount))
                .Throws(new InvalidDataException(Shared.Constants.AmountMustBePositiveError));

            var result = controller.Bet(betAmount);

            var status = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, status.StatusCode);
            Assert.Equal(Shared.Constants.AmountMustBePositiveError, status.Value);
        }
    }
}
