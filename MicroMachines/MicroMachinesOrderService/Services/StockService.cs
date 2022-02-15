using MicroMachinesCommon.Dtos;

namespace MicroMachinesOrderService.Services;

public class StockService : IStockService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public StockService(
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }

    public async Task<bool> verifyStockAsync(List<ItineraryItemReadDto> itineraryItems)
    {
        var httpClient = _httpClientFactory.CreateClient();
        HttpRequestMessage request = new HttpRequestMessage(
            HttpMethod.Get,
            $"{_configuration["StockService"]}/stocks/1/products");
        request.Content = JsonContent.Create(itineraryItems);
        var response = await httpClient.SendAsync(request);

        return (response.IsSuccessStatusCode) ? true : false;
    }
}
