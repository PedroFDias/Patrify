namespace Patrify.TransactionAPI.DTO
{
    public record AccountResponse
    (
        Guid Id,
        string Name,
        string LastName,
        string Email,
        string Cpf,
        bool IsActive
    );
}
