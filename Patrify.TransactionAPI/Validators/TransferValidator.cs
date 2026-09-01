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

            RuleFor(x => x.CpfAccountOrigem)
                .NotEmpty()
                .WithMessage("The source Cpf must not be empty.");

            RuleFor(x => x.CpfAccountDestino)
                .NotEmpty()
                .WithMessage("The target CpfDestino must not be empty.");

            RuleFor(x => x.CpfAccountDestino)
                .NotEqual(x => x.CpfAccountOrigem)
                .WithMessage("The target CpfDestino must be different from the source Cpf.");

            RuleFor(x => x.Type)
                .IsInEnum()
                .WithMessage("The transaction type is invalid.");
        }
    }
}
