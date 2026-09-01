namespace Patrify.Account.DTO
{
    public record UserAccountResponse(
        Guid Id,
        string Name,
        string LastName,
        string Email,
        string Cpf,
        decimal Balance,
        bool IsActive
    );
}
