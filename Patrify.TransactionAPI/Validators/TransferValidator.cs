using FluentValidation;

namespace Patrify.TransactionAPI.Validators
{
    public class TransferValidator: AbstractValidator<TransferRequest>
    {
        public TransferValidator()
        {
            RuleFor(x => x.Amount)
                .GreaterThan(0)
                .WithMessage("The amount must be greater than zero.");

            RuleFor(x => x.AccountId)
                .NotEmpty()
                .WithMessage("The source account ID must not be empty.");

            RuleFor(x => x.targetAccountId)
                .NotEmpty()
                .WithMessage("The target account ID must not be empty.");

            RuleFor(x => x.targetAccountId)
                .NotEqual(x => x.AccountId)
                .WithMessage("The target account must be different from the source account.");

            RuleFor(x => x.Type)
                .IsInEnum()
                .WithMessage("The transaction type is invalid.");
        }
    }
}
