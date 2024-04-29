namespace TransactionService.DAL.Entities
{
    public record UserBalanceEntity
    {
        public long UserId { get; set; }
        public decimal Balance { get; set; }
        public string Currency { get; set; } = null!;
    }
}
