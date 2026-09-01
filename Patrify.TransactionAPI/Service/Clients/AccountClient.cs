namespace Patrify.TransactionAPI.Service.Clients
{
    public class AccountClient : IAccountClient
    {
        private readonly HttpClient _httpClient;
        public AccountClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<AccountResponse?> GetByCpfAsync(string cpf, CancellationToken cancellationToken)
        {
            return await _httpClient.GetFromJsonAsync<AccountResponse?>(
                $"api/accounts/{cpf}", cancellationToken
            );
        }
    }
}
