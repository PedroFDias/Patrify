using AutoMapper;
using Patrify.Account.DTO;

namespace Patrify.Account.Profiles
{
    public class UserAccountProfile: Profile
    {
        public UserAccountProfile()
        {
            CreateMap<UserAccount, UserAccountRequest>().ReverseMap();
            CreateMap<UserAccount, UserAccountResponse>();
        }
    }
}
