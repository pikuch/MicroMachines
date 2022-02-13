﻿using MicroMachinesCommon.Dtos;

namespace MicroMachinesGateway.Services;

public class OrderService : IOrderService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public OrderService(
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }
    public async Task<IEnumerable<OrderReadDto>?> GetOrdersOfUserAsync(int userId)
    {
        var httpClient = _httpClientFactory.CreateClient();
        HttpRequestMessage request = new HttpRequestMessage(
            HttpMethod.Get,
            $"{_configuration["OrderService"]}/orders/user/{userId}");
        var response = await httpClient.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsAsync<IEnumerable<OrderReadDto>>();
        }

        return null;
    }
}
