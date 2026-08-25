using AutoMapper;
using Patrify.Account.DTO;

namespace Patrify.Account.Mapper
{
    public class AccountProfile: Profile
    {
        public AccountProfile()
        {
            CreateMap<Entities.Account, AccountRequest>();
        }
    }
}
