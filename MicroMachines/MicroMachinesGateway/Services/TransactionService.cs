using MicroMachinesCommon.Dtos;

namespace MicroMachinesGateway.Services;

public class TransactionService : ITransactionService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public TransactionService(
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }

    public async Task<bool> ConfirmAsync(int transactionId)
    {
        var httpClient = _httpClientFactory.CreateClient();
        HttpRequestMessage request = new HttpRequestMessage(
            HttpMethod.Put,
            $"{_configuration["TransactionService"]}/transactions/{transactionId}/confirm");
        var response = await httpClient.SendAsync(request);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> CreateAsync(TransactionCreateDto transaction)
    {
        var httpClient = _httpClientFactory.CreateClient();
        HttpRequestMessage request = new HttpRequestMessage(
            HttpMethod.Post,
            $"{_configuration["TransactionService"]}/transactions");
        request.Content = JsonContent.Create(transaction);
        var response = await httpClient.SendAsync(request);
        return response.IsSuccessStatusCode;
    }

    public async Task<IEnumerable<TransactionReadDto>?> GetTransactionsOfUserAsync(int userId)
    {
        var httpClient = _httpClientFactory.CreateClient();
        HttpRequestMessage request = new HttpRequestMessage(
            HttpMethod.Get,
            $"{_configuration["TransactionService"]}/transactions/user/{userId}");
        var response = await httpClient.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsAsync<IEnumerable<TransactionReadDto>>();
        }

        return null;
    }
}
