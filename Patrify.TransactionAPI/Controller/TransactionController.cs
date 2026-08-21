using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Patrify.TransactionAPI.DTO;
using Patrify.TransactionAPI.Entities;
using Patrify.TransactionAPI.RabbitMQSender;
using Patrify.TransactionAPI.Repositories;

namespace Patrify.TransactionAPI.Controller
{
    [ApiController]
    [Route("api/transaction")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _repository;
        private readonly IMapper _mapper;
        private IRabbitMQMessageSender _rabbitMQMessageSender;

        public TransactionController(ITransactionRepository repository, IMapper mapper, IRabbitMQMessageSender messageSender)
        {
            _repository = repository;
            _mapper = mapper;
            _rabbitMQMessageSender = messageSender ?? throw new ArgumentNullException(nameof(messageSender));
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] TransactionRequest request)
        {
            var transaction = _mapper.Map<Transaction>(request);
            await _repository.AddAsync(transaction);

            await _rabbitMQMessageSender.SendMessageAsync(request);

            return Ok(transaction);
        }
    }
}
