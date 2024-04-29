using Microsoft.Extensions.Logging;

using Monad;

using TransactionService.DAL.Entities;
using TransactionService.DAL.Interfaces;
using TransactionService.Domain;
using TransactionService.Facade.Interfaces;

namespace TransactionService.Facade.Implementations
{
    public class UserBalanceFacade(IUserBalanceRepository repository, ILogger<UserBalanceFacade> logger) : IUserBalanceFacade
    {
        private readonly ILogger<UserBalanceFacade> _logger = logger;
        private readonly IUserBalanceRepository _repository = repository;

        public async ValueTask<Either<UserBalance, Error>> GetUserBalance(long userId)
        {
            try
            {
                var userBalanceEntity = await _repository.GetUserBalanceAsync(userId);
                return () => MapToUserBalance(userBalanceEntity);
            }
            catch (Exception ex) 
            {
                _logger.LogError($"Error in getting user current balance. Detail: {ex.Message}");
                return () => new Error() { Code = ErrorCodes.InternalServerError, Message = "Error in get balance" };
            }
        }

        private UserBalance MapToUserBalance(UserBalanceEntity userBalanceEntity)
        {
            return new UserBalance()
            {
                UserId = userBalanceEntity.UserId,
                Balance = userBalanceEntity.Balance,
                Currency = userBalanceEntity.Currency
            };
        }
    }
}
