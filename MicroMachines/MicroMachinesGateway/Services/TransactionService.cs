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
