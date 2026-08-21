using AutoMapper;
using Patrify.TransactionAPI.DTO;
using Patrify.TransactionAPI.Entities;

namespace Patrify.TransactionAPI.Mappings
{
    public class TransactionProfile: Profile
    {
        public TransactionProfile() 
        { 
            CreateMap<TransactionRequest, Transaction>();
        }
    }
}
