using Monad;

using TransactionService.Domain;

namespace TransactionService.Facade.Interfaces
{
    public interface ITransactionFacade
    {
        ValueTask<Either<Transaction, Error>> CreateTransaction(Transaction transaction);
    }
}
