using System.ComponentModel;

namespace TransactionService.Domain
{
    public record Transaction
    {
        public long Id { get; set; }
        public Guid Reference { get; init; }

        public long UserId { get; init; }
        public TransactionType TransactionType { get; set; }
        public TransactionStatus Status { get; set; }
        public decimal Amount { get; init; }

        public string Currency { get; init; } = null!;

        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
    }

    public enum TransactionType
    {
        [Description("Debit")]
        Debit = 1,
        [Description("Credit")]
        Credit = 2,
    }

    public enum TransactionStatus
    {
        Deleted = 0,
        Active = 1,
    }
}
