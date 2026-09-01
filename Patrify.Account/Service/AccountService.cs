using Microsoft.AspNetCore.Mvc;

namespace Patrify.Account.Service
{
    public class AccountService : IAccountService
    {
        public IAccountRepository _repository;
        public IUnitOfWork _unitOfWork;
        public AccountService(IAccountRepository accountRepository, IUnitOfWork unitOfWork)
        {
            _repository = accountRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(UserAccount account)
        {
            await _repository.AddAsync(account);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<UserAccount?> GetAccountByCpf(string cpf)
        {
            return await _repository.GetByExpressionAsync(a => a.Cpf == cpf);
        }
    }
}
