namespace Patrify.Account.Services
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
    }
}
