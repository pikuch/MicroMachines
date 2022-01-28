using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace MicroMachinesOrderService.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly ILogger<OrderController> _logger;
    private readonly IMapper _mapper;

    public OrderController(
        ILogger<OrderController> logger,
        IMapper mapper)
    {
        _logger = logger;
        _mapper = mapper;
    }

    //[HttpGet(Name = "GetWeatherForecast")]
    //public IEnumerable<WeatherForecast> Get()
    //{
    //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
    //    {
    //        Date = DateTime.Now.AddDays(index),
    //        TemperatureC = Random.Shared.Next(-20, 55),
    //        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
    //    })
    //    .ToArray();
    //}
}
