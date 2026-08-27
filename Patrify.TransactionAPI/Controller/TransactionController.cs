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

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService ?? throw new ArgumentNullException(nameof(transactionService));
        }

        [HttpPost("deposit")]
        public async Task<IActionResult> CreateDeposit([FromBody] DepositRequest request)
        {
            await _transactionService.AddDepositAsync(request);

            return Ok(request);
        }

        [HttpPost("transfer")]
        public async Task<IActionResult> CreateTransfer([FromBody] TransferRequest request)
        {
            await _transactionService.AddTransferAsync(request);

            return Ok(request);
        }
    }
}
