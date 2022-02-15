using MicroMachinesCommon.Dtos;
using MicroMachinesCommon.Enums;
using MicroMachinesTransactionService.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace MicroMachinesTransactionService.Services;

public class PaymentQueueReceiver : BackgroundService
{
    private readonly IConfiguration _configuration;
    private readonly ITransactionRepository _transactionRepository;
    private IConnection _connection = null!;
    private IModel? _channel;
    private readonly string _paymentQueueName;
    private readonly IProductService _productService;
    private readonly IAccountService _accountService;
    private readonly IStockService _stockService;
    private readonly IUserService _userService;
    private readonly IOrderService _orderService;
    private ConnectionFactory? _factory;

    public PaymentQueueReceiver(
        IConfiguration configuration,
        ITransactionRepository transactionRepository,
        IProductService productService,
        IAccountService accountService,
        IStockService stockService,
        IUserService userService,
        IOrderService orderService)
    {
        _configuration = configuration;
        _transactionRepository = transactionRepository;
        _paymentQueueName = configuration["PaymentQueue"];
        _productService = productService;
        _accountService = accountService;
        _stockService = stockService;
        _userService = userService;
        _orderService = orderService;
    }

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        _factory = new ConnectionFactory
        {
            HostName = _configuration["Rabbit"],
            DispatchConsumersAsync = true
        };
        _connection = _factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: _paymentQueueName,
                                durable: true,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);

        return base.StartAsync(cancellationToken);
    }
    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await base.StopAsync(cancellationToken);
        _connection.Close();
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var paymentQueueConsumer = new AsyncEventingBasicConsumer(_channel);

        paymentQueueConsumer.Received += async (model, eventArgs) =>
        {
            var body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var order = JsonConvert.DeserializeObject<OrderReadDto>(message);

            var orderValue = await _productService.GetItineraryValueAsync(order.Itinerary);
            if (orderValue == null)
            {
                return;
            }
            var userAccounts = await _accountService.GetUserAccountsAsync(order.UserId);
            if (userAccounts == null)
            {
                return;
            }
            var sufficientAccount = userAccounts.Where(x => x.IsClosed == false && x.Balance >= orderValue).FirstOrDefault();

            if (sufficientAccount == null)
            {
                return;
            }

            await _accountService.ChargeAccountAsync(sufficientAccount.Id, orderValue.Value);

            Transaction transaction = new Transaction()
            {
                AccountFromId = sufficientAccount.Id,
                AccountToId = 0,
                TimeStamp = DateTime.UtcNow,
                Amount = orderValue.Value,
                Status = TransactionStatus.Confirmed
            };

            await _transactionRepository.CreateAsync(transaction);
            await _stockService.RemoveProducts(order.Itinerary);
            await _userService.AddProducts(order.UserId, order.Itinerary);
            await _orderService.ConfirmOrder(order.Id, transaction.Id);
        };
        _channel.BasicConsume(_paymentQueueName, true, paymentQueueConsumer);
        await Task.CompletedTask;
    }
}
