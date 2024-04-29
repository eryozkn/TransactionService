namespace TransactionService.Domain
{
    public record Error
    {
        public string Code { get; init; } = null!;
        public string Message { get; set; } = null!;
    }

    public static class ErrorCodes
    {
        public const string InternalServerError = "INTERNAL_SERVER_ERROR";
        public const string TransactionNotFoundError = "TRANSACTION_NOT_FOUND";
    }
}
