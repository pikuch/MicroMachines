using MicroMachinesCommon.Dtos;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace MicroMachinesOrderService.Services;

public class PaymentQueue : IPaymentQueue
{
    private readonly IConnection _connection;
    private readonly string _paymentQueueName;
    private readonly string _paymentExchangeName;

    public PaymentQueue(IConfiguration configuration)
    {
        var factory = new ConnectionFactory();
        factory.HostName = configuration["Rabbit"];
        _connection = factory.CreateConnection();
        _paymentQueueName = configuration["PaymentQueue"];
        _paymentExchangeName = configuration["PaymentExchange"];
    }
    public async Task<bool> Enqueue(OrderReadDto order)
    {
        using var channel = _connection.CreateModel();
        channel.QueueDeclare(_paymentQueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
        channel.ExchangeDeclare(_paymentExchangeName, ExchangeType.Direct);
        channel.QueueBind(_paymentQueueName, _paymentExchangeName, "");
        string message = JsonConvert.SerializeObject(order);
        var body = Encoding.UTF8.GetBytes(message);
        channel.BasicPublish(_paymentExchangeName, "", null, body);
        return true;
    }
}
