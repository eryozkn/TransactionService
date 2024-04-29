namespace TransactionService.Models.Response
{
    public record UserBalanceResponse
    {
        /// <summary>
        /// The user id on topup app
        /// </summary>
        public long UserId { get; init; }

        /// <summary>
        /// User current balance
        /// </summary>
        public decimal Balance { get; init; }
        /// <summary>
        /// Currency of the balance
        /// </summary>
        public string Currency { get; init; } = null!;
    }
}
