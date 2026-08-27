using AutoMapper;

namespace Patrify.TransactionAPI.Mappings
{
    public class TransactionProfile: Profile
    {
        public TransactionProfile() 
        { 
            CreateMap<DepositRequest, Transaction>().ReverseMap();
            CreateMap<TransferRequest, Transaction>().ReverseMap();
        }
    }
}
