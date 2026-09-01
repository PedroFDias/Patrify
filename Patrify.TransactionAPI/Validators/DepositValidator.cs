using FluentValidation;

namespace Patrify.TransactionAPI.Validators
{
    public class DepositValidator : AbstractValidator<DepositRequest>
    {
        public DepositValidator()
        {
            RuleFor(x => x.Cpf)
                .NotEmpty()
                .WithMessage("Cpf is required.");

            RuleFor(x => x.Amount)
                .GreaterThan(0)
                .WithMessage("Amount must be greater than zero.");

            RuleFor(x => x.Description)
                .MaximumLength(255)
                .WithMessage("Description must not exceed 255 characters.");

            RuleFor(x => x.Type)
                .IsInEnum()
                .WithMessage("Type must be a valid transaction type.");

        }
    }
}
