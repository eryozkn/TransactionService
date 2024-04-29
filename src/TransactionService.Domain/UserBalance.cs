namespace TransactionService.Domain
{
    public record UserBalance
    {
        public long UserId { get; init; }

        public decimal Balance { get; init; }

        public string Currency { get; init; } = null!;
    }
}
