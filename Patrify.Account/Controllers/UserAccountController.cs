using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Patrify.Account.DTO;

namespace Patrify.Account.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class UserAccountController : ControllerBase
    {
        public IAccountService _service;
        public IMapper _mapper;

        public UserAccountController(IAccountService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<UserAccountResponse> AddAccount(UserAccountRequest accountDto)
        {
            var account = _mapper.Map<UserAccount>(accountDto);
            await _service.AddAsync(account);
            return _mapper.Map<UserAccountResponse>(account);
        }
        [HttpGet("{cpf}")]
        public async Task<UserAccountResponse?> Get(string cpf)
        {
            return _mapper.Map<UserAccountResponse?>(await _service.GetAccountByCpf(cpf));
        }

    }
}
