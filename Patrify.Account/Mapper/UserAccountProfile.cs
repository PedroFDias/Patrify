using AutoMapper;
using Patrify.Account.DTO;

namespace Patrify.Account.Mapper
{
    public class UserAccountProfile: Profile
    {
        public UserAccountProfile()
        {
            CreateMap<UserAccount, UserAccountRequest>();
        }
    }
}
