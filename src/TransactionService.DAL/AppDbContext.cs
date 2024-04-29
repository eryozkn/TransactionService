using Microsoft.EntityFrameworkCore;

using TransactionService.DAL.Entities;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<TransactionEntity> Transactions { get; set; }
    public DbSet<UserBalanceEntity> UserBalances { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TransactionEntity>()
            .Property(t => t.Amount)
            .HasColumnType("decimal(18, 2)");
        
        modelBuilder.Entity<UserBalanceEntity>().ToView("UserBalances");
        modelBuilder.Entity<UserBalanceEntity>().HasNoKey();
    }
}
