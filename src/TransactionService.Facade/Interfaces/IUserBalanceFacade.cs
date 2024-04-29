using Monad;

using TransactionService.Domain;

namespace TransactionService.Facade.Interfaces
{
    public interface IUserBalanceFacade
    {
        ValueTask<Either<UserBalance, Error>> GetUserBalance(long userId);
    }
}