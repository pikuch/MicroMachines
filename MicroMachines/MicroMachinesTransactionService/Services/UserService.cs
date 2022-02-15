using MicroMachinesCommon.Dtos;

namespace MicroMachinesTransactionService.Services;

public class UserService : IUserService
{
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _httpClientFactory;

    public UserService(
        IConfiguration configuration,
        IHttpClientFactory httpClientFactory)
    {
        _configuration = configuration;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<bool> AddProducts(int userId, IEnumerable<ItineraryItemReadDto> itineraryItems)
    {
        var httpClient = _httpClientFactory.CreateClient();
        HttpRequestMessage request = new HttpRequestMessage(
            HttpMethod.Post,
            $"{_configuration["UserService"]}/users/{userId}/products");
        request.Content = JsonContent.Create(itineraryItems);
        var response = await httpClient.SendAsync(request);

        return response.IsSuccessStatusCode ? true : false;
    }
}
