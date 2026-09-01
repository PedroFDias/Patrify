namespace Patrify.TransactionAPI.Service.Clients
{
    public interface IAccountClient
    {
        Task<AccountResponse?> GetByCpfAsync(
            string cpf,
            CancellationToken cancellationToken);
    }
}
