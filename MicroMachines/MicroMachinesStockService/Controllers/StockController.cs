using AutoMapper;
using MicroMachinesCommon.Dtos;
using MicroMachinesStockService.Models;
using MicroMachinesStockService.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MicroMachinesStockService.Controllers;

[ApiController]
[Route("stocks")]
public class StockController : ControllerBase
{
    private readonly ILogger<StockController> _logger;
    private readonly IMapper _mapper;
    private readonly IStockRepository _stockRepository;

    public StockController(
        ILogger<StockController> logger,
        IMapper mapper,
        IStockRepository stockRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _stockRepository = stockRepository;
    }

    [HttpGet]
    [SwaggerOperation("Gets all stocks", "GET /stocks")]
    public async Task<ActionResult<IEnumerable<StockReadDto>>> GetAll()
    {
        var stocks = await _stockRepository.GetAllAsync();
        if (stocks == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<IEnumerable<StockReadDto>>(stocks));
    }

    [HttpGet("{stockId}", Name = "GetById")]
    [SwaggerOperation("Gets stock by id", "GET /stocks/{stockId}")]
    public async Task<ActionResult<StockReadDto>> GetById(int stockId)
    {
        var stock = await _stockRepository.GetByIdAsync(stockId);
        if (stock == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<StockReadDto>(stock));
    }

    [HttpPost]
    [SwaggerOperation("Creates a new stock", "POST /stocks")]
    public async Task<ActionResult> Create(StockCreateDto stock)
    {
        var newStock = await _stockRepository.CreateAsync(_mapper.Map<Stock>(stock));
        return CreatedAtRoute(nameof(GetById), new { stockId = newStock.Id }, _mapper.Map<StockReadDto>(newStock));
    }

    [HttpPut("{stockId}")]
    [SwaggerOperation("Updates a stock", "PUT /stocks/{stockId}")]
    public async Task<ActionResult> Update(int stockId, StockUpdateDto stock)
    {
        var foundStock = await _stockRepository.GetByIdAsync(stockId);
        if (foundStock == null)
        {
            return NotFound();
        }
        _mapper.Map(stock, foundStock);

        bool result = await _stockRepository.UpdateAsync();
        return (result) ? Ok() : BadRequest();
    }

    [HttpDelete("{stockId}")]
    [SwaggerOperation("Deletes a stock if it's empty", "DELETE /stocks/{stockId}")]
    public async Task<ActionResult> Delete(int stockId)
    {
        var foundStock = await _stockRepository.GetByIdAsync(stockId);
        if (foundStock == null)
        {
            return NotFound();
        }
        bool result = await _stockRepository.DeleteAsync(stockId);
        return (result) ? Ok() : BadRequest();
    }

    [HttpPost]
    [Route("{stockId}/products")]
    [SwaggerOperation("Adds products to the stock with given id", "POST /stocks/{stockId}/products")]
    public async Task<ActionResult> AddProducts(int stockId, IEnumerable<ItineraryItemCreateDto> items)
    {
        if (!items.Any())
        {
            return BadRequest();
        }
        bool result = await _stockRepository.AddProductsAsync(stockId, _mapper.Map<IEnumerable<ItineraryItem>>(items));
        return (result) ? Ok() : BadRequest();
    }

    [HttpPut]
    [Route("{stockId}/products")]
    [SwaggerOperation("Removes products from the stock with given id", "PUT /stocks/{stockId}/products")]
    public async Task<ActionResult> RemoveProducts(int stockId, IEnumerable<ItineraryItemCreateDto> items)
    {
        if (!items.Any())
        {
            return BadRequest();
        }
        bool result = await _stockRepository.RemoveProductsAsync(stockId, _mapper.Map<IEnumerable<ItineraryItem>>(items));
        return (result) ? Ok() : BadRequest();
    }
}
