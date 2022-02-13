using MicroMachinesCommon.Dtos;

namespace MicroMachinesGateway.Services;

public class UserService : IUserService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public UserService(
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }
    public async Task<IEnumerable<ItineraryItemReadDto>?> GetProductsOfUserAsync(int userId)
    {
        var httpClient = _httpClientFactory.CreateClient();
        HttpRequestMessage request = new HttpRequestMessage(
            HttpMethod.Get,
            $"{_configuration["UserService"]}/users/{userId}/products");
        var response = await httpClient.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsAsync<IEnumerable<ItineraryItemReadDto>>();
        }

        return null;
    }
}
