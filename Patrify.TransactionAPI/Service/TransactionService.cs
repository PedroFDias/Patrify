using AutoMapper;
using FluentValidation;
using Patrify.MessageBus.Contracts.Enums;
using Patrify.MessageBus.RabbitMQ.Publish;
using Patrify.TransactionAPI.Service.Clients;
using RabbitMQ.Client;

namespace Patrify.TransactionAPI.Service
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private IRabbitMQPublish _rabbitMQMessageSender;
        private readonly IMapper _mapper;
        private readonly IValidator<DepositRequest> _depositValidator;
        private readonly IValidator<TransferRequest> _transferValidator;
        private readonly IAccountClient _accountClient;

        public TransactionService(ITransactionRepository transactionRepository, IUnitOfWork unitOfWork, IRabbitMQPublish messageSender, IMapper mapper, IValidator<DepositRequest> depositValidator, IValidator<TransferRequest> transferValidator, IAccountClient accountClient)
        {
            _transactionRepository = transactionRepository;
            _unitOfWork = unitOfWork;
            _rabbitMQMessageSender = messageSender ?? throw new ArgumentNullException(nameof(messageSender));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _depositValidator = depositValidator ?? throw new ArgumentNullException(nameof(depositValidator));
            _transferValidator = transferValidator ?? throw new ArgumentNullException(nameof(transferValidator));
            _accountClient = accountClient;
        }
        public async Task<bool> AddDepositAsync(DepositRequest request, CancellationToken cancellationToken = default)
        {
            var validationResult = await _depositValidator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var account = await _accountClient.GetByCpfAsync(request.Cpf, cancellationToken);

            if (account is null)
                return false;

            if (!account.IsActive)
                return false;

            var transaction = _mapper.Map<Transaction>(request);

            transaction.Status = TransactionStatus.Processing;

            await _transactionRepository.AddAsync(transaction, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var message = new DepositCreatedEvent(
                transaction.Id,
                account.Id,
                transaction.Amount,
                transaction.Status
            );

            await _rabbitMQMessageSender.Publish(
                message,
                "Patrify.exchange",
                ExchangeType.Topic,
                "deposit.created"
            );

            return true;
        }
        public async Task<bool> AddTransferAsync(TransferRequest request, CancellationToken cancellationToken = default)
        {
            var validationResult = await _transferValidator.ValidateAsync(request, cancellationToken);

            if(!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var account = await _accountClient.GetByCpfAsync(request.CpfAccountOrigem, cancellationToken);

            if (account is null)
                return false;

            if (!account.IsActive)
                return false;

            var accountDestino = await _accountClient.GetByCpfAsync(request.CpfAccountDestino, cancellationToken);

            if (accountDestino is null)
                return false;

            if (!accountDestino.IsActive)
                return false;

            var transaction = _mapper.Map<Transaction>(request);

            transaction.Status = TransactionStatus.Processing;

            await _transactionRepository.AddAsync(transaction, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var message = new TransferCreatedEvent(
                transaction.Id,
                account.Id,
                accountDestino.Id,
                transaction.Amount,
                transaction.Status
            );

            await _rabbitMQMessageSender.Publish(
                message,
                "Patrify.exchange",
                ExchangeType.Topic,
                "transfer.created"
            );

            return true;
        }
    }
}
