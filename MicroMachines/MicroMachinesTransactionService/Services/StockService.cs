using MicroMachinesCommon.Dtos;

namespace MicroMachinesTransactionService.Services;

public class StockService : IStockService
{
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _httpClientFactory;

    public StockService(
        IConfiguration configuration,
        IHttpClientFactory httpClientFactory)
    {
        _configuration = configuration;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<bool> RemoveProducts(IEnumerable<ItineraryItemReadDto> itineraryItems)
    {
        var httpClient = _httpClientFactory.CreateClient();
        HttpRequestMessage request = new HttpRequestMessage(
            HttpMethod.Put,
            $"{_configuration["StockService"]}/stocks/1/products");
        request.Content = JsonContent.Create(itineraryItems);
        var response = await httpClient.SendAsync(request);

        return response.IsSuccessStatusCode ? true : false;
    }
}
