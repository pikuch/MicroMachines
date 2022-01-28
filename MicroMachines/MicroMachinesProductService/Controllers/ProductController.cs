using AutoMapper;
using MicroMachinesCommon.Dtos;
using MicroMachinesProductService.Models;
using MicroMachinesProductService.Services;
using Microsoft.AspNetCore.Mvc;

namespace MicroMachinesProductService.Controllers;

[ApiController]
[Route("products")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;

    public ProductController(
        ILogger<ProductController> logger,
        IMapper mapper,
        IProductRepository productRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _productRepository = productRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductReadDto>>> GetAll()
    {
        var products = await _productRepository.GetAllAsync();
        if (products == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<IEnumerable<ProductReadDto>>(products));
    }

    [HttpGet("{productId}", Name = "GetById")]
    public async Task<ActionResult<ProductReadDto>> GetById(int productId)
    {
        var product = await _productRepository.GetByIdAsync(productId);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<ProductReadDto>(product));
    }

    [HttpPost]
    public async Task<ActionResult> Create(ProductCreateDto product)
    {
        var newProduct = await _productRepository.CreateAsync(_mapper.Map<Product>(product));
        return CreatedAtRoute(nameof(GetById), new { productId = newProduct.Id }, _mapper.Map<ProductReadDto>(newProduct));
    }

    [HttpPut("{productId}")]
    public async Task<ActionResult> Update(int productId, ProductUpdateDto product)
    {
        var foundProduct = await _productRepository.GetByIdAsync(productId);
        if (foundProduct == null)
        {
            return NotFound();
        }
        _mapper.Map(product, foundProduct);

        bool result = await _productRepository.UpdateAsync(productId, foundProduct);
        return (result) ? Ok() : BadRequest();
    }

    [HttpDelete("{productId}")]
    public async Task<ActionResult> Delete(int productId)
    {
        var foundProduct = await _productRepository.GetByIdAsync(productId);
        if (foundProduct == null)
        {
            return NotFound();
        }
        bool result = await _productRepository.DeleteAsync(productId);
        return (result) ? Ok() : BadRequest();
    }
}
