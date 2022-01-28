using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace MicroMachinesTransactionService.Controllers;

[ApiController]
[Route("[controller]")]
public class TransactionController : ControllerBase
{
    private readonly ILogger<TransactionController> _logger;
    private readonly IMapper _mapper;

    public TransactionController(
        ILogger<TransactionController> logger,
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
