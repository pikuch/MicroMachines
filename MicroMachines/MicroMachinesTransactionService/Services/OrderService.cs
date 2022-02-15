namespace MicroMachinesTransactionService.Services;

public class OrderService : IOrderService
{
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _httpClientFactory;

    public OrderService(
        IConfiguration configuration,
        IHttpClientFactory httpClientFactory)
    {
        _configuration = configuration;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<bool> ConfirmOrder(int orderId, int transactionId)
    {
        var httpClient = _httpClientFactory.CreateClient();
        HttpRequestMessage request = new HttpRequestMessage(
            HttpMethod.Put,
            $"{_configuration["OrderService"]}/orders/{orderId}/confirm/{transactionId}");
        var response = await httpClient.SendAsync(request);

        return response.IsSuccessStatusCode ? true : false;
    }
}
