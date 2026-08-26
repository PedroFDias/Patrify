using AutoMapper;

namespace Patrify.Account.Profiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<DTO.UserAccountRequest, UserAccount>();
        }
    }
}
