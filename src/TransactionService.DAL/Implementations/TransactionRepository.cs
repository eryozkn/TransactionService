using Microsoft.EntityFrameworkCore;

using TransactionService.DAL.Entities;
using TransactionService.DAL.Interfaces;

namespace TransactionService.DAL.Implementations
{
    public class TransactionRepository(AppDbContext context) : ITransactionRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<TransactionEntity> CreateTransaction(TransactionEntity transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return transaction;
        }

        public Task<IEnumerable<TransactionEntity>> GetUserTransactions(long userId)
        {
            return (Task<IEnumerable<TransactionEntity>>)_context.Transactions.AsNoTracking().ToList()
                .Where(u => u.UserId == userId);
        }
    }
}
