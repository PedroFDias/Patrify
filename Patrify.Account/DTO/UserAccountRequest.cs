namespace Patrify.Account.DTO
{
    public record UserAccountRequest(
        string Name,
        string LastName,
        string Email,
        string Cpf
    );
}
