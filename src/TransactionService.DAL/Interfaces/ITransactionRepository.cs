using TransactionService.DAL.Entities;

namespace TransactionService.DAL.Interfaces
{
    public interface ITransactionRepository
    {
        Task<TransactionEntity> CreateTransaction(TransactionEntity transaction);
        Task<IEnumerable<TransactionEntity>> GetUserTransactions(long userId);
    }
}
