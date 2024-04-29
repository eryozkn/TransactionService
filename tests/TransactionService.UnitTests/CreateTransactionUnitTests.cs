using Moq;

using TransactionService.Controllers;
using TransactionService.Domain;
using TransactionService.Facade.Interfaces;
using TransactionService.Models.Request;

using Xunit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using System.Threading.Tasks;
using System;
using TransactionService.Models.Response;

namespace TransactionService.UnitTests
{
    public class CreateTransactionUnitTests
    {
        private readonly Mock<ITransactionFacade> _transactionFacadeMock = new();

        private const int TestUserId = 1;
        private const decimal TestAmount = 500;
        private const string TestCurrency = "AED";

        [Fact]
        public async Task CreateTransaction_ShouldReturnSuccessResponse()
        {
            // Setup
            _transactionFacadeMock.Setup(x => x.CreateTransaction(It.IsAny<Transaction>()))
                .ReturnsAsync(
                    () => new Transaction()
                    {
                        UserId = TestUserId,
                        Id = 99,
                        Amount = TestAmount,
                        Currency = "AED",
                        TransactionType = TransactionType.Credit,
                        Reference = Guid.NewGuid(),
                        Status = TransactionStatus.Active,
                        CreatedAt = DateTimeOffset.UtcNow,
                        UpdatedAt = DateTimeOffset.UtcNow
                    });

            TransactionController controllerMock = new(_transactionFacadeMock.Object);

            var requestMock = new TransactionRequest()
            {
                UserId = TestUserId,
                Amount = TestAmount,
                Currency = TestCurrency,
                TransactionType= TransactionType.Credit
            };

            var actionResult = await controllerMock.CreateTransaction(requestMock);


            // Assert
            actionResult.Should().NotBeNull("Response is always supplied");

            actionResult.Should().BeAssignableTo<ObjectResult>()
                     .Which.StatusCode.Should().Be(StatusCodes.Status200OK);

            var resultObject = TestUtils.GetObjectResultContent<TransactionResponse>(actionResult);

            resultObject.UserId.Should().BeOfType(typeof(long)).And.Be(TestUserId);
            resultObject.Amount.Should().BeOfType(typeof(decimal)).And.Be(TestAmount);
        }

        [Fact]
        public async Task CreateTransaction_ShouldReturnInternalServerError()
        {
            // Setup
            _transactionFacadeMock.Setup(x => x.CreateTransaction(It.IsAny<Transaction>()))
                .ReturnsAsync(
                    () => new Error()
                    {
                        Code = ErrorCodes.InternalServerError,
                        Message = "Something went wrong when calling DB"
                    }
                    );

            TransactionController controllerMock = new(_transactionFacadeMock.Object);

            var requestMock = new TransactionRequest()
            {
                UserId = TestUserId,
                Amount = TestAmount,
                Currency = TestCurrency,
                TransactionType = TransactionType.Credit
            };

            var actionResult = await controllerMock.CreateTransaction(requestMock);

            // Assert
            actionResult.Should().NotBeNull("Response is always supplied");

            actionResult.Should().BeAssignableTo<ObjectResult>()
                     .Which.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }
    }
}