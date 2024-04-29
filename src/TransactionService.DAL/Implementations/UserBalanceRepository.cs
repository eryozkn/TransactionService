using Microsoft.EntityFrameworkCore;

using TransactionService.DAL.Entities;
using TransactionService.DAL.Interfaces;

namespace TransactionService.DAL.Implementations
{
    public class UserBalanceRepository(AppDbContext context) : IUserBalanceRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<UserBalanceEntity> GetUserBalanceAsync(long userId) 
            => await _context.UserBalances.FirstOrDefaultAsync(u => u.UserId == userId) 
               ?? new UserBalanceEntity() { UserId = userId, Balance = 0, Currency = "AED"};
    }
}
