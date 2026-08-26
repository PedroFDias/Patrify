using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Patrify.MessageBus.Contracts.Events;
using Patrify.MessageBus.RabbitMQ.Publish;
using Patrify.TransactionAPI.DTO;
using Patrify.TransactionAPI.Entities;
using Patrify.TransactionAPI.Service;
using RabbitMQ.Client;

namespace Patrify.TransactionAPI.Controller
{
    [ApiController]
    [Route("api/transaction")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;
        private IRabbitMQPublish _rabbitMQMessageSender;

        public TransactionController(ITransactionService transactionService, IMapper mapper, IRabbitMQPublish messageSender)
        {
            _transactionService = transactionService ?? throw new ArgumentNullException(nameof(transactionService));
            _mapper = mapper;
            _rabbitMQMessageSender = messageSender ?? throw new ArgumentNullException(nameof(messageSender));
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] TransactionRequest request)
        {
            var transaction = _mapper.Map<Transaction>(request);
            await _transactionService.AddAsync(transaction);

            var message = new TransactionCreatedEvent(
                transaction.Id,
                request.AccountId,
                transaction.Amount,
                (MessageBus.Contracts.Enums.TransactionType)request.Type
            );

            await _rabbitMQMessageSender.Publish(
                message,
                "Patrify.exchange",
                ExchangeType.Topic,
                "transaction.created"
            );

            return Ok(transaction);
        }
    }
}
