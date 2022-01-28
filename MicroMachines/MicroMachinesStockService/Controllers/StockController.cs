using AutoMapper;
using MicroMachinesCommon.Dtos;
using MicroMachinesStockService.Models;
using MicroMachinesStockService.Services;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<ActionResult> Create(StockCreateDto stock)
    {
        var newStock = await _stockRepository.CreateAsync(_mapper.Map<Stock>(stock));
        return CreatedAtRoute(nameof(GetById), new { stockId = newStock.Id }, _mapper.Map<StockReadDto>(newStock));
    }

    [HttpPut("{stockId}")]
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
