namespace TransactionService.DAL.Entities
{
    public class TransactionEntity
    {
        public long Id { get; set; }
        public Guid Reference { get; set; }
        public long UserId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; } = null!;
        public int Status { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
