using Moq;
using Shared.DTOs;
using System.Net;
using System.Text;
using System.Text.Json;
using Wallet.Interfaces;
using Wallet.Services;
using Wallet.Shared;

namespace Wallet.Tests.Unit.Service
{
    public class BettingOrchestrationServiceTests 
    {
        private Mock<IWalletHttpClient> walletHttpClientMock;
        private BettingOrchestrationService service;
        public BettingOrchestrationServiceTests()
        {
            walletHttpClientMock = new Mock<IWalletHttpClient>();
            service = new BettingOrchestrationService(walletHttpClientMock.Object);
        }

        [Fact]
        public async Task PlaceBetAsync_AmountOutOfBounds_Should_ReturnRangeError()
        {
            var result = await service.PlaceBetAsync(0.5m);
            Assert.Contains(string.Format(Shared.Constants.AmountMustBeWithinRangeError, "$1.00", "$10.00"), result);

            result = await service.PlaceBetAsync(10.5m);
            Assert.Contains(string.Format(Shared.Constants.AmountMustBeWithinRangeError, "$1.00", "$10.00"), result);
        }

        [Fact]
        public async Task PlaceBetAsync_WithdrawFails_ShouldReturnUnsuccessfulBetMessage()
        {
            walletHttpClientMock.Setup(x => x.WithdrawAsync(It.IsAny<decimal>()))
                .ThrowsAsync(new HttpRequestException());

            var result = await service.PlaceBetAsync(5m);

            Assert.Contains(string.Format(Shared.Constants.UnsuccessfulBetMessage, "$5.00"), result);
        }

        [Fact]
        public async Task PlaceBetAsync_WithdrawNotSuccessStatus_ShouldReturnUnsuccessfulBetMessage()
        {
            walletHttpClientMock.Setup(x => x.WithdrawAsync(It.IsAny<decimal>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.BadRequest));

            var result = await service.PlaceBetAsync(5m);

            Assert.Contains(string.Format(Shared.Constants.UnsuccessfulBetMessage, "$5.00"), result);
        }

        [Fact]
        public async Task PlaceBetAsync_BetFails_RefundSuccess_ShouldReturnUnsuccessfulBetMessage()
        {
            walletHttpClientMock.Setup(x => x.WithdrawAsync(It.IsAny<decimal>()))
              .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK));
            walletHttpClientMock.Setup(x => x.PlaceBetAsync(It.IsAny<decimal>()))
              .ThrowsAsync(new HttpRequestException());
            walletHttpClientMock.Setup(x => x.DepositAsync(It.IsAny<decimal>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK));

            var result = await service.PlaceBetAsync(5m);

            Assert.Contains(string.Format(Shared.Constants.UnsuccessfulBetMessage, "$5.00"), result);
        }

        [Fact]
        public async Task PlaceBetAsync_BetFails_RefundFails_ShouldReturnBetFailedDepositError()
        {
            walletHttpClientMock.Setup(x => x.WithdrawAsync(It.IsAny<decimal>()))
               .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK));
            walletHttpClientMock.Setup(x => x.PlaceBetAsync(It.IsAny<decimal>()))
               .ThrowsAsync(new HttpRequestException());
            walletHttpClientMock.Setup(x => x.DepositAsync(It.IsAny<decimal>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.BadRequest));

            var result = await service.PlaceBetAsync(5m);

            Assert.Equal(Shared.Constants.BetFailedDepositError, result);
        }

        [Fact]
        public async Task PlaceBetAsync_BetWon_DepositSuccess_ShouldReturnBetWonMessage()
        {
            walletHttpClientMock.Setup(x => x.WithdrawAsync(It.IsAny<decimal>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK));
            var betWinDto = new BalanceDTO { Balance = 10m };
            walletHttpClientMock.Setup(x => x.PlaceBetAsync(It.IsAny<decimal>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JsonSerializer.Serialize(betWinDto), Encoding.UTF8, "application/json")
                });
            walletHttpClientMock.Setup(x => x.GetWalletBalanceAsync())
                .ReturnsAsync(JsonSerializer.Serialize(new BalanceDTO { Balance = 10m }));
            walletHttpClientMock.Setup(x => x.DepositAsync(It.IsAny<decimal>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK));

            var result = await service.PlaceBetAsync(5m);

            Assert.Contains(string.Format(Shared.Constants.BetWonMessage.Split('!')[0], "$10.00"), result);
        }

        [Fact]
        public async Task PlaceBetAsync_BetLost_ShouldReturnBetLostMessage()
        {
            walletHttpClientMock.Setup(x => x.WithdrawAsync(It.IsAny<decimal>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK));
            var betLostDto = new BalanceDTO { Balance = 0m };
           walletHttpClientMock.Setup(x => x.PlaceBetAsync(It.IsAny<decimal>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JsonSerializer.Serialize(betLostDto), Encoding.UTF8, "application/json")
                });
            walletHttpClientMock.Setup(x => x.GetWalletBalanceAsync())
                .ReturnsAsync(JsonSerializer.Serialize(new BalanceDTO { Balance = 0m }));

            var result = await service.PlaceBetAsync(5m);

            Assert.Contains(Shared.Constants.BetLostMessage.Split('!')[0], result);
        }
    }
}
