namespace TransactionService.DAL.Entities
{
    public class UserBalanceEntity
    {
        public long UserId { get; set; }
        public decimal Balance { get; set; }
        public string Currency { get; set; } = null!;
    }
}
