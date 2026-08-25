using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Patrify.Account.DTO;
using Patrify.Account.Services;

namespace Patrify.Account.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountController : ControllerBase
    {
        public IAccountService _service;
        public IMapper _mapper;

        public AccountController(IAccountService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<AccountRequest> AddAccount(AccountRequest accountDto)
        {
            var account = _mapper.Map<Entities.Account>(accountDto);
            await _service.AddAsync(account);
            return accountDto;
        }

        [HttpGet]
        public async Task<IActionResult> Get(AccountRequest accountDto)
        {
            return Ok();
        }
    }
}
