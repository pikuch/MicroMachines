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
    private readonly IConnection _connection;
    private readonly string _paymentQueueName;

    public PaymentQueueReceiver(IConfiguration configuration)
    {
        var factory = new ConnectionFactory();
        factory.HostName = configuration["Rabbit"];
        _connection = factory.CreateConnection();
        _paymentQueueName = configuration["PaymentQueue"];
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var channel = _connection.CreateModel();
        channel.QueueDeclare(_paymentQueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
        var paymentQueueConsumer = new EventingBasicConsumer(channel);

        paymentQueueConsumer.Received += async (model, eventArgs) =>
        {
            var body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var order = JsonConvert.DeserializeObject<OrderReadDto>(message);

            //var orderValue = await _productService.GetItineraryValueAsync(order.Itinerary);
            //var userAccounts = await _accountService.GetUserAccountsAsync(order.UserId);
            //var sufficientAccount = userAccounts.Where(x => x.IsClosed == false && x.Balance >= orderValue).FirstOrDefault();

            //if (sufficientAccount == null)
            //{
            //    return;
            //}

            //await _accountService.ChargeAccountAsync(sufficientAccount.Id, orderValue);
            //Transaction transaction = new Transaction()
            //{
            //    AccountFromId = sufficientAccount.Id,
            //    AccountToId = 0,
            //    TimeStamp = DateTime.UtcNow,
            //    Amount = orderValue,
            //    Status = TransactionStatus.Confirmed
            //};

            //await _transactionRepository.CreateAsync(Transaction transaction);
            //await _stockService.RemoveProducts(order.Itinerary);
            //await _userService.AddProducts(order.Itinerary);
            //await _orderService.UpdateOrder(order);
        };
        channel.BasicConsume(_paymentQueueName, true, paymentQueueConsumer);
        return Task.CompletedTask;
    }
}
