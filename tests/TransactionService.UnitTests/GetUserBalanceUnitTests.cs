using Moq;

using TransactionService.Controllers;
using TransactionService.Domain;
using TransactionService.Facade.Interfaces;

using Xunit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using System.Threading.Tasks;
using TransactionService.Models.Response;

namespace TransactionService.UnitTests
{
    public class GetUserBalanceUnitTests
    {
        private readonly Mock<IUserBalanceFacade> _userBalanceFacadeMock = new();

        private const long TestUserId = 1;
        private const decimal TestBalance = 1200;
        private const string TestCurrency = "AED";

        [Fact]
        public async Task GetUserBalance_ShouldReturnSuccessResponse()
        {
            // Setup
            _userBalanceFacadeMock.Setup(x => x.GetUserBalance(It.IsAny<long>()))
                .ReturnsAsync(
                    () => new UserBalance()
                    {
                        UserId = TestUserId,
                        Balance = TestBalance,
                        Currency = TestCurrency
                    });

            UserBalanceController controllerMock = new(_userBalanceFacadeMock.Object);

            var actionResult = await controllerMock.GetUserBalance(TestUserId);


            // Assert
            actionResult.Should().NotBeNull("Response is always supplied");

            actionResult.Should().BeAssignableTo<ObjectResult>()
                     .Which.StatusCode.Should().Be(StatusCodes.Status200OK);

            var resultObject = TestUtils.GetObjectResultContent<UserBalanceResponse>(actionResult);

            resultObject.UserId.Should().BeOfType(typeof(long)).And.Be(TestUserId);
            resultObject.Balance.Should().BeOfType(typeof(decimal)).And.Be(TestBalance);
            resultObject.Currency.Should().BeOfType(typeof(string)).And.Be(TestCurrency);
        }

        [Fact]
        public async Task CreateTransaction_ShouldReturnInternalServerError()
        {
            // Setup
            _userBalanceFacadeMock.Setup(x => x.GetUserBalance(It.IsAny<long>()))
                .ReturnsAsync(
                    () => new Error()
                    {
                        Code = ErrorCodes.InternalServerError,
                        Message = "Something went wrong when getting user balance"
                    }
                    );

            UserBalanceController controllerMock = new(_userBalanceFacadeMock.Object);

            var actionResult = await controllerMock.GetUserBalance(TestUserId);

            // Assert
            actionResult.Should().NotBeNull("Response is always supplied");

            actionResult.Should().BeAssignableTo<ObjectResult>()
                     .Which.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }
    }
}
