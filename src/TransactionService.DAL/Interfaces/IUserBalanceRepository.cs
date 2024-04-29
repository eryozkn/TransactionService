using TransactionService.DAL.Entities;

namespace TransactionService.DAL.Interfaces
{
    public interface IUserBalanceRepository
    {
        Task<UserBalanceEntity> GetUserBalanceAsync(long userId);
    }
}
