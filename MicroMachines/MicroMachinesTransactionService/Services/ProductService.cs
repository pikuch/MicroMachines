using MicroMachinesCommon.Dtos;

namespace MicroMachinesTransactionService.Services;

public class ProductService : IProductService
{
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _httpClientFactory;

    public ProductService(
        IConfiguration configuration,
        IHttpClientFactory httpClientFactory)
    {
        _configuration = configuration;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<decimal?> GetItineraryValueAsync(List<ItineraryItemReadDto> itineraryItems)
    {
        var httpClient = _httpClientFactory.CreateClient();
        HttpRequestMessage request = new HttpRequestMessage(
            HttpMethod.Put,
            $"{_configuration["ProductService"]}/products/value");
        request.Content = JsonContent.Create(itineraryItems);
        var response = await httpClient.SendAsync(request);
        
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<decimal>();
        }

        return null;
    }
}
