using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Patrify.Account.DTO;
using Patrify.Account.Services;

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
        public async Task<UserAccountRequest> AddAccount(UserAccountRequest accountDto)
        {
            var account = _mapper.Map<Entities.UserAccount>(accountDto);
            await _service.AddAsync(account);
            return accountDto;
        }

        [HttpGet]
        public async Task<IActionResult> Get(UserAccountRequest accountDto)
        {
            return Ok();
        }
    }
}
