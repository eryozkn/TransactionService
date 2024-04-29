using FluentValidation;

using TransactionService.Domain;

namespace TransactionService.Models.Request
{
    public record TransactionRequest
    {
        public long UserId { get; init; }

        public decimal Amount { get; init; }

        public string Currency { get; init; } = null!;
        public TransactionType TransactionType { get; init; }
    }

    public class TransactionRequestValidator : AbstractValidator<TransactionRequest>
    {
        public TransactionRequestValidator()
        {
            RuleFor(x => x.UserId).NotNull().GreaterThan(0);
            RuleFor(x => x.Amount).GreaterThan(0);
            RuleFor(x => x.TransactionType).IsInEnum();
            RuleFor(x => x.Currency).NotEmpty().When(x => x.Currency != null);
        }
    }
}