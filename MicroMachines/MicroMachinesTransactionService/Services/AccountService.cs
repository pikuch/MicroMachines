using MicroMachinesCommon.Dtos;

namespace MicroMachinesTransactionService.Services;

public class AccountService : IAccountService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public AccountService(
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }

    public async Task<bool> ChargeAccountAsync(int accountId, decimal orderValue)
    {
        var httpClient = _httpClientFactory.CreateClient();
        HttpRequestMessage request = new HttpRequestMessage(
            HttpMethod.Put,
            $"{_configuration["AccountService"]}/accounts/{accountId}/charge/{orderValue}");
        var response = await httpClient.SendAsync(request);

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> ExecuteTransactionAsync(TransactionUpdateDto transaction)
    {
        var httpClient = _httpClientFactory.CreateClient();
        HttpRequestMessage request = new HttpRequestMessage(
            HttpMethod.Put,
            $"{_configuration["AccountService"]}/accounts/executetransaction");
        request.Content = JsonContent.Create(transaction);
        var response = await httpClient.SendAsync(request);

        return response.IsSuccessStatusCode;
    }

    public async Task<IEnumerable<AccountReadDto>?> GetUserAccountsAsync(int userId)
    {
        var httpClient = _httpClientFactory.CreateClient();
        HttpRequestMessage request = new HttpRequestMessage(
            HttpMethod.Get,
            $"{_configuration["AccountService"]}/accounts/user/{userId}");
        var response = await httpClient.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<IEnumerable<AccountReadDto>>();
        }

        return null;
    }
}
