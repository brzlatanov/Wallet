using Data;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Interfaces;
using WalletService.Services;
using static System.Net.Mime.MediaTypeNames;

namespace Wallet.Tests.Unit.Service
{
    public class WalletServiceTests
    {
        private IWalletService service;
        private Mock<IDatabaseContext> dbContext;
        public WalletServiceTests()
        {
            dbContext = new Mock<IDatabaseContext>();
            service = new WalletService.Services.WalletService(dbContext.Object); 
        }

        [Fact]
        public async Task GetBalanceAsync_DataCorrupted_ShouldReturnError()
        {
            DbContextShouldThrowExceptionSetup();

            await Assert.ThrowsAsync<InvalidDataException>(() => service.GetBalanceAsync());
        }

        [Fact]
        public async Task GetBalanceAsync_ShouldReturnAmount()
        {
            dbContext.Setup(x => x.GetBalanceAsync())
            .ReturnsAsync(10m);

            Assert.Equal(10m, await service.GetBalanceAsync());
        }

        [Fact]
        public async Task SetBalanceAsync_ShouldThrowExceptionIfGetBalanceThrows()
        {
            DbContextShouldThrowExceptionSetup();

            await Assert.ThrowsAsync<InvalidDataException>(() =>  service.DepositAsync(10m));
        }

        [Fact]
        public async Task SetBalanceAsync_ShouldReturnAmount()
        {
            dbContext.Setup(x => x.GetBalanceAsync())
            .ReturnsAsync(10m);

            Assert.Equal(20m, await service.DepositAsync(10m));
        }

        private void DbContextShouldThrowExceptionSetup()
        {
            dbContext.Setup(x => x.GetBalanceAsync())
            .ThrowsAsync(new InvalidDataException($"Invalid balance value in file: 'mockdb.txt'"));
        }
    }
}
