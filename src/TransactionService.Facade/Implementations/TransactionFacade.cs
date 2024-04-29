using Microsoft.Extensions.Logging;

using Monad;

using TransactionService.DAL.Entities;
using TransactionService.DAL.Interfaces;
using TransactionService.Domain;
using TransactionService.Facade.Interfaces;

namespace TransactionService.Facade.Implementations
{
    public class TransactionFacade(ILogger<TransactionFacade> logger, ITransactionRepository repository) : ITransactionFacade
    {
        private readonly ILogger<TransactionFacade> _logger = logger;
        private readonly ITransactionRepository _repository = repository;

        public async ValueTask<Either<Transaction, Error>> CreateTransaction(Transaction transaction)
        {
            //map to entity a mapper library can be used such as Automapper, Mapperly
            var transactionEntity = MapToTransactionEntity(transaction);

            try
            {
                var createdTransaction = await _repository.CreateTransaction(transactionEntity);
                return () => MapToTransaction(createdTransaction);
            }
            catch (Exception ex) 
            {
                _logger.LogError($"Error in creating transaction. Detail: {ex.Message}");
                return () => new Error() { Code = ErrorCodes.InternalServerError , Message = "Error in create transaction"};
            }
        }

        private static TransactionEntity MapToTransactionEntity(Transaction transaction)
        {
            return new TransactionEntity()
            {
                Reference = Guid.NewGuid(),
                UserId = transaction.UserId,
                Status = (int)TransactionStatus.Active,
                Amount = (transaction.TransactionType == TransactionType.Debit) ? -transaction.Amount : transaction.Amount, // Debit (-), Credit (+)
                Currency = transaction.Currency,
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            };
        }
        private static Transaction MapToTransaction(TransactionEntity transactionEntity)
        {
            return new Transaction()
            {
                Reference = transactionEntity.Reference,
                UserId = transactionEntity.UserId,
                Status = (TransactionStatus)transactionEntity.Status,
                Amount = transactionEntity.Amount,
                Currency = transactionEntity.Currency,
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow,
            };
        }
    }
}
